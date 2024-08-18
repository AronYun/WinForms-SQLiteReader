using System;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SQLiteReader
{
    class SQLite
    {
        protected string source { get; set; }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="source">來源資料庫</param>
        /// <param name="connection">資料庫連接</param>
        public SQLite(string source)
        {
            this.source = source;
        }

        /// <summary>
        /// 建立資料庫
        /// </summary>
        public void createDataBase()
        {
            if (!Directory.Exists(Path.GetDirectoryName(this.source)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(this.source));
            }

            if (!File.Exists(this.source))
            {
                SQLiteConnection.CreateFile(this.source);
                
            }
        }

        /// <summary>
        /// 執行查詢語法 (回傳DataTable)
        /// </summary>
        /// <param name="connection">資料庫連接(可傳空值null)</param>
        /// <param name="sql">SQL語法</param>
        /// <param name="parameter">參數</param>
        /// <returns>DataTable</returns>
        public DataTable select(string sql, SQLiteConnection connection = null, SQLiteParameter[] parameter = null)
        {
            sql = sql.Trim();
            string prefix = sql.Length >= 6 ? sql.Substring(0, 6) : sql;
            if (!string.Equals(prefix, "SELECT", StringComparison.OrdinalIgnoreCase))
            {
                throw new SQLiteException("SQL logic error prefix is not 'SELECT': syntax error");
            }

            DataTable table = new DataTable();

            if (connection == null)
            {
                using (connection = new SQLiteConnection($"data source={this.source}"))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        if (parameter != null && parameter.Length > 0)
                        {
                            command.Parameters.AddRange(parameter);
                        }
                        using (SQLiteDataAdapter adpter = new SQLiteDataAdapter(command))
                        {
                            adpter.Fill(table);
                        }
                    }
                }
            }
            else
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameter != null && parameter.Length > 0)
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    using (SQLiteDataAdapter adpter = new SQLiteDataAdapter(command))
                    {
                        adpter.Fill(table);
                    }
                }
            }
            return table;
        }

        /// /// <summary>
        /// 執行查詢語法 (回傳List)     
        /// </summary>
        /// <param name="connection">資料庫連接(可傳空值null)</param>
        /// <param name="sql">SQL語法</param>
        /// <param name="parameter">參數</param>
        /// <returns>List</returns>
        public List<T> select<T>(string sql, SQLiteConnection connection = null, SQLiteParameter[] parameter = null) where T : new()
        {
            DataTable table = this.select(sql, connection, parameter);
            List<T> list = this.convertToModels<T>(table);
            return list;
        }

        /// <summary>
        /// 執行語法
        /// </summary>
        /// <param name="connection">資料庫連接(可傳空值null)</param>
        /// <param name="sql">SQL語法</param>
        /// <param name="parameter">參數</param>
        public int execute(string sql, SQLiteConnection connection = null, SQLiteParameter[] parameter = null)
        {
            int count = 0;

            sql = sql.Trim();

            string dataSource = connection == null ? this.source : connection.FileName;

            if (connection == null)
            {
                using (connection = new SQLiteConnection($"data source={this.source}"))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                        {
                            if (parameter != null && parameter.Length > 0)
                            {
                                command.Parameters.AddRange(parameter);
                            }
                            try
                            {
                                count = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            catch (Exception e)
                            {
                                transaction.Rollback();
                                throw e;
                            }
                        }
                    }
                }
            }
            else
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameter != null && parameter.Length > 0)
                    {
                        command.Parameters.AddRange(parameter);
                    }
                    count = command.ExecuteNonQuery();
                }
            }
            return count;
        }

        /// <summary>
        /// DataTable 轉換 Model (Class Object)
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>Class Object</returns>
        private List<T> convertToModels<T>(DataTable table) where T : new()
        {
            List<T> models = new List<T>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    T model = new T();
                    foreach (DataColumn column in table.Columns)
                    {
                        PropertyInfo propertyInfo = typeof(T).GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.IgnoreCase);
                        if (propertyInfo != null && row[column] != DBNull.Value && propertyInfo.CanWrite)
                        {
                            if (propertyInfo.PropertyType == typeof(decimal?))
                            {
                                propertyInfo.SetValue(model, Convert.ChangeType(row[column], typeof(decimal)), null);
                            }
                            else if (propertyInfo.PropertyType == typeof(int?))
                            {
                                propertyInfo.SetValue(model, Convert.ChangeType(row[column], typeof(int)), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(model, Convert.ChangeType(row[column], propertyInfo.PropertyType), null);
                            }
                        }
                    }
                    models.Add(model);
                }
            }
            return models;
        }

        /// <summary>
        /// 將 Model 轉為 INSERT 單筆的 SQL 語法
        /// </summary>
        /// <param name="model">Class Object</param>
        /// <returns>string</returns>
        private Tuple<string, SQLiteParameter[]> insertOneSQL(string tableName, object model)
        {
            string sql = string.Empty;

            List<string> columnName = new List<string>();
            List<string> columnParam = new List<string>();
            List<SQLiteParameter> parameter = new List<SQLiteParameter>();
            for (int i = 0; i < model.GetType().GetProperties().Length; i++)
            {
                PropertyInfo property = model.GetType().GetProperties()[i];
                string name = property.Name;
                string param = $"@param{i}";
                object value = property.GetValue(model);
                columnName.Add(name);
                columnParam.Add(param);
                parameter.Add(new SQLiteParameter(param, value));
            }

            sql = $@"INSERT INTO {tableName} ({string.Join(",", columnName)}) VALUES ({string.Join(",", columnParam)})";

            return new Tuple<string, SQLiteParameter[]>(sql, parameter.ToArray());
        }

        /// <summary>
        /// 將 Model 轉為 INSERT 多筆的 SQL 語法
        /// </summary>
        /// <param name="modelList">Class Object List</param>
        /// <returns>string</returns>
        private Tuple<string, SQLiteParameter[]> insertListSQL(string tableName, List<object> modelList)
        {
            string sql = string.Empty;

            List<string> columnName = new List<string>();
            List<string> columnParam = new List<string>();
            List<SQLiteParameter> parameter = new List<SQLiteParameter>();
            int count = 0;
            for (int i = 0; i < modelList.Count; i++)
            {
                object model = modelList[i];
                List<string> columnOneParam = new List<string>();

                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    string name = property.Name;
                    string param = $"@param{count++}";
                    object value = property.GetValue(model);

                    if (i == 0)
                    {
                        columnName.Add(name);
                    }

                    columnOneParam.Add(param);
                    parameter.Add(new SQLiteParameter(param, value));
                }

                columnParam.Add($"({string.Join(",", columnOneParam)})");
            }
            sql = $@"INSERT INTO {tableName} ({string.Join(",", columnName)}) VALUES {string.Join(",", columnParam)}";

            return new Tuple<string, SQLiteParameter[]>(sql, parameter.ToArray());
        }

        /// <summary>
        /// 組合 SQL 與 parameter
        /// </summary>
        /// <param name="sql">SQL語法</param>
        /// <param name="parameter">參數陣列</param>
        /// <returns>string</returns>
        private string combineSQL(string sql, SQLiteParameter[] parameter)
        {
            string resultSQL = sql;

            foreach (SQLiteParameter param in parameter)
            {
                string parameterName = $@"(?<=\W){param.ParameterName}(?=\W|$)";
                string value = param.Value is string ? $"'{param.Value}'" : param.Value.ToString();
                resultSQL = Regex.Replace(resultSQL, parameterName, value);
            }

            return resultSQL;
        }

        /// <summary>
        /// 取得資料庫所有的資料表名稱
        /// </summary>
        /// <returns>object[]</returns>
        public object[] getTables()
        {
            string columnName = "name";
            string sql = $"SELECT {columnName} FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%'";
            DataTable table = this.select(sql);
            object[] tables = new object[table.Rows.Count];
            for(int i = 0; i < table.Rows.Count; i++)
            {
                tables[i] = table.Rows[i][columnName];
            }
            return tables;
        }
    }
}