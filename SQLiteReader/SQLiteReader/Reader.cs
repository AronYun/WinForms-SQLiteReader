using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace SQLiteReader
{
    public partial class Reader : Form
    {
        private float W = 854; // 視窗預設寬度
        private float H = 480; // 視窗預設長度

        public Reader()
        {
            InitializeComponent();
        }

        /// <summary>
        ///［視窗］載入中事件
        /// </summary>
        private void Reader_Load(object sender, EventArgs e)
        {
            W = this.Width;
            H = this.Height;
            Zoom.SetTag(this);
        }

        /// <summary>
        ///［視窗］顯示後事件
        /// </summary>
        private void Reader_Shown(object sender, EventArgs e)
        {
            this.tableListBox.Items.Clear();
            this.pathTextBox.Text = string.Empty;
            this.scriptTextBox.Text = string.Empty;
            this.tableDataGridView_DataSource(null);
            this.messageTextBox.Text = string.Empty;
        }

        /// <summary>
        ///［視窗］縮放事件
        /// </summary>
        private void Reader_Resize(object sender, EventArgs e)
        {
            float scaleX = (this.Width) / W;
            float scaleY = (this.Height) / H;
            Zoom.SetControls(scaleX, scaleY, this);
        }

        /// <summary>
        ///［建立資料庫］按鈕事件
        /// </summary>
        private void createButton_Click(object sender, EventArgs args)
        {
            try
            {
                string input = Interaction.InputBox("請輸入資料庫路徑", "建立資料庫", string.Empty);

                if (string.IsNullOrEmpty(input))
                {
                    return;
                }

                string dataBaseSource = string.IsNullOrEmpty(input) ? string.Empty : Path.GetFullPath(input.Trim());

                if (File.Exists(dataBaseSource))
                {
                    
                    throw new Exception("資料庫已存在，無法再次建立");
                }

                SQLite sqlite = new SQLite(dataBaseSource);
                sqlite.createDataBase();

                MessageBox.Show("建立成功");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///［執行］按鈕事件
        /// </summary>
        private void executeButton_Click(object sender, EventArgs args)
        {
            try
            {
                string scriptTextBoxSelection = this.scriptTextBox.SelectionLength > 0 ? this.scriptTextBox.SelectedText : this.scriptTextBox.Text;
                string sql = string.IsNullOrEmpty(scriptTextBoxSelection) ? string.Empty : scriptTextBoxSelection.Trim().Replace(Environment.NewLine, " ");
                this.scriptTextBox.Focus();

                if (string.IsNullOrEmpty(sql))
                {
                    return;
                }

                string dataBaseSource = string.IsNullOrEmpty(this.pathTextBox.Text) ? string.Empty : Path.GetFullPath(this.pathTextBox.Text.Trim());
                this.pathTextBox.Text = dataBaseSource;

                if (string.IsNullOrEmpty(dataBaseSource))
                {
                    throw new Exception("請連線至資料庫");
                }

                SQLite sqlite = new SQLite(dataBaseSource);

                int spaceIndex = sql.IndexOf(" ");
                string type = spaceIndex >= 0 ? sql.Substring(0, spaceIndex).Trim().ToUpper() : sql;

                DataTable table = null;
                string message = string.Empty;
                TabPage tabPage = this.resultTabPage;

                if (string.Equals(type, "SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    table = sqlite.select(sql);
                    message = $"查詢 {table.Rows.Count} 筆資料，執行SQLite資料庫語法：{sql}";
                }
                else
                {
                    DialogResult result = MessageBox.Show($"是否要執行異動指令？", "確認訊息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        int count = sqlite.execute(sql);
                        message = $"異動 {count} 筆資料，執行SQLite資料庫語法：{sql}";
                        tabPage = this.messageTabPage;
                    }
                }

                this.tableDataGridView_DataSource(table);
                this.messageTextBox.Text = message;
                this.resultTabControl.SelectedTab = tabPage;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///［結果表格］資料來源設定
        /// </summary>
        /// <param name="dataSource">資料來源(null:清除)</param>
        private void tableDataGridView_DataSource(object dataSource = null)
        {
            this.tableDataGridView.Columns.Clear();
            this.tableDataGridView.DataSource = dataSource;
        }

        /// <summary>
        ///［連線］按鈕事件
        /// </summary>
        private void connectButton_Click(object sender, EventArgs args)
        {
            try
            {
                OpenFileDialog browser = new OpenFileDialog();
                browser.Title = "開啟檔案";
                browser.Filter = "所有檔案|*.*";
                DialogResult reslut = browser.ShowDialog();
                if (reslut == DialogResult.OK)
                {
                    string path = browser.FileName;

                    SQLite sqlite = new SQLite(path);
                    object[] tables = sqlite.getTables();

                    this.Reader_Shown(sender, args);

                    this.pathTextBox.Text = path;
                    this.tableListBox.Items.AddRange(tables); 
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///［資料庫路徑］文本改變事件
        /// </summary>
        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            string path = string.IsNullOrEmpty(this.pathTextBox.Text) ? string.Empty : this.pathTextBox.Text.Trim();
            bool enabled = !path.Equals(string.Empty);

            this.unconnectButton.Enabled = enabled;
            this.scriptTextBox.Enabled = enabled;
            this.executeButton.Enabled = enabled;
        }

        /// <summary>
        ///［中斷連線］按鈕事件
        /// </summary>
        private void unconnectButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"是否要中斷連線？", "確認訊息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Reader_Shown(sender, e);
            }   
        }

        /// <summary>
        ///［Table列表］項目雙擊事件
        /// </summary>
        private void tableListBox_DoubleClick(object sender, EventArgs e)
        {
            if (this.tableListBox.SelectedItem != null)
            {
                string tableName = this.tableListBox.SelectedItem.ToString();
                this.scriptTextBox.Text = $"SELECT * FROM {tableName}";
            }
        }

        /// <summary>
        ///［Table列表］滑鼠放開事件
        /// </summary>
        private void tableListBox_MouseUp(object sender, MouseEventArgs e)
        {
            int index = this.tableListBox.IndexFromPoint(e.Location);
            if (index == ListBox.NoMatches)
            {
                this.tableListBox.ClearSelected();
            }
        }

        /// <summary>
        ///［匯入產製］按鈕事件
        /// </summary>
        private void importProduceButton_Click(object sender, EventArgs args)
        {
            try
            {
                OpenFileDialog browser = new OpenFileDialog();
                browser.Title = "開啟檔案";
                browser.Filter = "所有檔案|*.*|Log 檔案|*.log|Txt 檔案|*.txt";
                DialogResult reslut = browser.ShowDialog();
                if (reslut == DialogResult.OK)
                {
                    string filePath = browser.FileName;
                    ImportProduce.run(filePath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.ToString());
            }
        }
    }
}