using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SQLiteReader
{
    class ImportProduce
    {
        public static void run(string filePath)
        {
            filePath = filePath.Trim();
            string[] separator = new string[] { "/#|*/" };

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Trim().Trim('\u001A').Replace("\r", string.Empty).Replace("\n", string.Empty);
                    string[] lineSplit = line.Split(separator, StringSplitOptions.None);
                    string dateTime = lineSplit.Length > 0 ? lineSplit[0] : string.Empty;
                    string machineNo = lineSplit.Length > 1 ? lineSplit[1] : string.Empty;
                    string databaseName = lineSplit.Length > 2 ? lineSplit[2] : string.Empty;
                    string sql = lineSplit.Length > 3 ? lineSplit[3] : string.Empty;

                    try
                    {
                        SQLite sqlite = new SQLite(databaseName);
                        createTable(sqlite);
                        sqlite.execute(sql);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"Line:[{line}]");
                        Console.WriteLine($"DB:[{databaseName}]");
                        Console.WriteLine($"SQL:[{sql}]");
                        Console.WriteLine($"Exception:[{e.ToString()}]");
                        Console.WriteLine("==================================================");
                    }
                }
            }
        }

        private static void createTable(SQLite sqlite)
        {
            string TBSO11 = $@"CREATE TABLE IF NOT EXISTS TBSO11 (
                                COMPID           TEXT DEFAULT '',
                                ORDERNO          TEXT DEFAULT '',
                                ORDERDATE        TEXT DEFAULT '',
                                ORDERTIME        TEXT DEFAULT '',
                                ISSHOP           TEXT DEFAULT '',
                                SHOPNO           TEXT DEFAULT '',
                                SHIFTNO          TEXT DEFAULT '',
                                STATION          TEXT DEFAULT '',
                                PAO              TEXT DEFAULT '',
                                CUSTNO           TEXT DEFAULT '',
                                PAYMETHOD        TEXT DEFAULT '',
                                MEMO             TEXT DEFAULT '',
                                TOTALAMT         REAL DEFAULT 0,
                                CASH             REAL DEFAULT 0,
                                DIFFAMT          REAL DEFAULT 0,
                                INVOICENO        TEXT DEFAULT '',
                                INVOICEDATE      TEXT DEFAULT '',
                                INVOICETYPE      TEXT DEFAULT '',
                                INVOICEAMT       REAL DEFAULT 0,
                                TAXAMT           REAL DEFAULT 0,
                                BEFTAXAMT        REAL DEFAULT 0,
                                GETITEMNO        TEXT DEFAULT '',
                                SAVEAREA         TEXT DEFAULT '',
                                VCHRNO           TEXT DEFAULT '',
                                VCHRDATE         TEXT DEFAULT '',
                                CHECKNO          TEXT DEFAULT '',
                                REVENUENO        TEXT DEFAULT '',
                                STATUS           TEXT DEFAULT '',
                                CREADATE         TEXT DEFAULT '',
                                CREATIME         TEXT DEFAULT '',
                                CREAEMPNO        TEXT DEFAULT '',
                                UPDDATE          TEXT DEFAULT '',
                                UPDTIME          TEXT DEFAULT '',
                                UPDEMPNO         TEXT DEFAULT '',
                                CONFDATE         TEXT DEFAULT '',
                                CONFTIME         TEXT DEFAULT '',
                                CONFEMPNO        TEXT DEFAULT '',
                                CANCELDATE       TEXT DEFAULT '',
                                CANCELTIME       TEXT DEFAULT '',
                                CANCELEMPNO      TEXT DEFAULT '',
                                GROUPNO          TEXT DEFAULT '',
                                MEMBERNO         TEXT DEFAULT '',
                                REMITAMT         REAL DEFAULT 0,
                                CREDITAMT        REAL DEFAULT 0,
                                GOODAMT          REAL DEFAULT 0,
                                GOODTAXAMT       REAL DEFAULT 0,
                                ISOVER           TEXT DEFAULT '',
                                OVERAMT          REAL DEFAULT 0,
                                PAYMETHODS       TEXT DEFAULT '',
                                DISCOUNTAMT      REAL DEFAULT 0,
                                TOTALADDAMT      REAL DEFAULT 0,
                                TOTALADDPOINTAMT REAL DEFAULT 0,
                                CARRIERID1       TEXT DEFAULT '',
                                NPOBAN           TEXT DEFAULT '',
                                IPASSAMT         REAL DEFAULT 0,
                                IPASSTAXAMT      REAL DEFAULT 0,
                                BONUSPOINT       REAL DEFAULT 0,
                                LINEPAYAMT       REAL DEFAULT 0,
                                ISUPLOAD         TEXT DEFAULT '',
                                UPLOADDATETIME   TEXT DEFAULT '',
                                UPLOADEMPNO      TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, ORDERNO)
                            )";

            string TBSO12 = $@"CREATE TABLE IF NOT EXISTS TBSO12 (
                                COMPID         TEXT DEFAULT '',
                                ORDERNO        TEXT DEFAULT '',
                                SRLNO          TEXT DEFAULT '',
                                MTRLNO         TEXT DEFAULT '',
                                MTRLNAME       TEXT DEFAULT '',
                                MEMO           TEXT DEFAULT '',
                                AMOUNT         REAL DEFAULT 0,
                                UNITCOSTPRICE  REAL DEFAULT 0,
                                UNITADDPRICE   REAL DEFAULT 0,
                                UNITADDPOINT   REAL DEFAULT 0,
                                UNITSALEPRICE  REAL DEFAULT 0,
                                COSTAMT        REAL DEFAULT 0,
                                ADDAMT         REAL DEFAULT 0,
                                ADDPOINTAMT    REAL DEFAULT 0,
                                SALEAMT        REAL DEFAULT 0,
                                INVAMT         REAL DEFAULT 0,
                                INVENTORYTYPE  TEXT DEFAULT '',
                                MINO           TEXT DEFAULT '',
                                DISCOUNT       REAL DEFAULT 0,
                                BEFTAXAMT      REAL DEFAULT 0,
                                TAXAMT         REAL DEFAULT 0,
                                ISUPLOAD       TEXT DEFAULT '',
                                UPLOADDATETIME TEXT DEFAULT '',
                                UPLOADEMPNO    TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, ORDERNO, SRLNO)
                            )";

            string TBSO21 = $@"CREATE TABLE IF NOT EXISTS TBSO21 (
                                COMPID         TEXT DEFAULT '',
                                STOREID        TEXT DEFAULT '',
                                MTRLNO         TEXT DEFAULT '',
                                TXNDATE        TEXT DEFAULT '',
                                TXNTIME        TEXT DEFAULT '',
                                TXNCODE        TEXT DEFAULT '',
                                TXNQTY         REAL DEFAULT 0,
                                TXNAMT         REAL DEFAULT 0,
                                TXNFROM        TEXT DEFAULT '',
                                TXNTO          TEXT DEFAULT '',
                                ORDERNO        TEXT DEFAULT '',
                                ISUPLOAD       TEXT DEFAULT '',
                                UPLOADDATETIME TEXT DEFAULT '',
                                UPLOADEMPNO    TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, STOREID, ORDERNO, MTRLNO, TXNCODE)
                            )";

            string TBAI04 = $@"CREATE TABLE IF NOT EXISTS TBAI04 (
                                COMPID             TEXT DEFAULT '',
                                INVOICENUMBER      TEXT DEFAULT '',
                                INVOICEDATE        TEXT DEFAULT '',
                                INVOICETIME        TEXT DEFAULT '',
                                SELLERID           TEXT DEFAULT '',
                                SELLERNAME         TEXT DEFAULT '',
                                BUYERID            TEXT DEFAULT '',
                                BUYERNAME          TEXT DEFAULT '',
                                CHECKNUMBER        TEXT DEFAULT '',
                                BUYERREMARK        TEXT DEFAULT '',
                                MAINREMARK         TEXT DEFAULT '',
                                CLEARANCEMARK      TEXT DEFAULT '',
                                CATEGORY           TEXT DEFAULT '',
                                RELATENUMBER       TEXT DEFAULT '',
                                INVOICETYPE        TEXT DEFAULT '',
                                GROUPMARK          TEXT DEFAULT '',
                                DONATEMARK         TEXT DEFAULT '',
                                CARRIERTYPE        TEXT DEFAULT '',
                                CARRIERID1         TEXT DEFAULT '',
                                CARRIERID2         TEXT DEFAULT '',
                                PRINTMARK          TEXT DEFAULT '',         
                                PRINTTIMES         INTEGER DEFAULT 0,
                                NPOBAN             TEXT DEFAULT '',
                                RANDOMNUMBER       TEXT DEFAULT '',
                                ARNO               TEXT DEFAULT '',
                                CHECKNO            TEXT DEFAULT '',
                                TAXYM              TEXT DEFAULT '',
                                TAXSRLNO           TEXT DEFAULT '',
                                VCHRNO             TEXT DEFAULT '',
                                VCHRDATE           TEXT DEFAULT '',
                                SHIPPINGNO         TEXT DEFAULT '',
                                PREINVOICEDATE     TEXT DEFAULT '',
                                COUNTTOTAL         REAL DEFAULT 0,
                                WEIGHTTOTAL        REAL DEFAULT 0,
                                INVOICEKIND        TEXT DEFAULT '',
                                DELETEMARKDATE     TEXT DEFAULT '',
                                DELETEVCHRNO       TEXT DEFAULT '',
                                DELETEEMPNO        TEXT DEFAULT '',
                                CREATEEMPNO        TEXT DEFAULT '',
                                CREATEDATE         TEXT DEFAULT '',
                                STATION            TEXT DEFAULT '',
                                SYSID              TEXT DEFAULT '',
                                MAILADDRESS        TEXT DEFAULT '',
                                SALESAMOUNT        REAL DEFAULT 0,
                                FREETAXSALESAMOUNT REAL DEFAULT 0,
                                ZEROTAXSALESAMOUNT REAL DEFAULT 0,
                                TAXTYPE            TEXT DEFAULT '',
                                TAXRATE            REAL DEFAULT 0,
                                TAXAMOUNT          REAL DEFAULT 0,
                                TOTALAMOUNT        REAL DEFAULT 0,
                                ALLOWANCESAMT      REAL DEFAULT 0,
                                CURRENCY           TEXT DEFAULT '',
                                STATUSCODE         TEXT DEFAULT '',
                                MARKTAXSTATUS      TEXT DEFAULT '',
                                ISUPLOAD           TEXT DEFAULT '',
                                UPLOADDATETIME     TEXT DEFAULT '',
                                UPLOADEMPNO        TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, INVOICENUMBER, INVOICEDATE, ARNO)
                            )";

            string TBAI05 = $@"CREATE TABLE IF NOT EXISTS TBAI05 (
                                COMPID            TEXT DEFAULT '',
                                ARNO              TEXT DEFAULT '',
                                DESCRIPTION       TEXT DEFAULT '',
                                QUANTITY          REAL DEFAULT 0,
                                UNIT              TEXT DEFAULT '',
                                UNITPRICE         REAL DEFAULT 0,
                                AMOUNT            REAL DEFAULT 0,
                                SEQUENCENUMBER    TEXT DEFAULT '',
                                REMARK            TEXT DEFAULT '',
                                RELATENUMBER      TEXT DEFAULT '',
                                ITEMNO            TEXT DEFAULT '',
                                WEIGHT            REAL DEFAULT 0,
                                TAX               REAL DEFAULT 0,
                                ORGSEQUENCENUMBER TEXT DEFAULT '',
                                DISCOUNTAMOUNT    REAL DEFAULT 0,
                                TXUNITPRICE       REAL DEFAULT 0,
                                ZEROINVAMOUNT     REAL DEFAULT 0,
                                ISUPLOAD          TEXT DEFAULT '',
                                UPLOADDATETIME    TEXT DEFAULT '',
                                UPLOADEMPNO       TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, ARNO, SEQUENCENUMBER)
                            )";

            string TBSO13 = $@"CREATE TABLE IF NOT EXISTS TBSO13 (
                                COMPID           TEXT DEFAULT '',  
                                ORDERNO          TEXT DEFAULT '',  
                                RETURNSRLNO      INTEGER DEFAULT 0,    
                                RETURNDATE       TEXT DEFAULT '',  
                                RETURNTIME       TEXT DEFAULT '',  
                                ISSHOP           TEXT DEFAULT '',  
                                SHOPNO           TEXT DEFAULT '',  
                                SHIFTNO          TEXT DEFAULT '',  
                                STATION          TEXT DEFAULT '',  
                                PAO              TEXT DEFAULT '',  
                                CUSTNO           TEXT DEFAULT '',  
                                RETURNREASON     TEXT DEFAULT '',  
                                MEMO             TEXT DEFAULT '',  
                                TOTALAMT         REAL DEFAULT 0, 
                                INVOICENO        TEXT DEFAULT '',  
                                INVOICEDATE      TEXT DEFAULT '',  
                                INVOICESRLNO     TEXT DEFAULT '',  
                                INVOICESRLDATE   TEXT DEFAULT '',  
                                INVOICETYPE      TEXT DEFAULT '',  
                                INVOICEAMT       REAL DEFAULT 0, 
                                TAXAMT           REAL DEFAULT 0, 
                                BEFTAXAMT        REAL DEFAULT 0, 
                                GETITEMNO        TEXT DEFAULT '',  
                                SAVEAREA         TEXT DEFAULT '',  
                                VCHRNO           TEXT DEFAULT '',  
                                VCHRDATE         TEXT DEFAULT '',  
                                CHECKNO          TEXT DEFAULT '',  
                                REVENUENO        TEXT DEFAULT '',  
                                NEWORDERNO       TEXT DEFAULT '',  
                                STATUS           TEXT DEFAULT '',  
                                CREADATE         TEXT DEFAULT '',  
                                CREATIME         TEXT DEFAULT '',  
                                CREAEMPNO        TEXT DEFAULT '',  
                                UPDDATE          TEXT DEFAULT '',  
                                UPDTIME          TEXT DEFAULT '',  
                                UPDEMPNO         TEXT DEFAULT '',  
                                CONFDATE         TEXT DEFAULT '',  
                                CONFTIME         TEXT DEFAULT '',  
                                CONFEMPNO        TEXT DEFAULT '',  
                                CANCELDATE       TEXT DEFAULT '',  
                                CANCELTIME       TEXT DEFAULT '',  
                                CANCELEMPNO      TEXT DEFAULT '',  
                                ORDERSRLNO       TEXT DEFAULT '',  
                                PAYMETHOD        TEXT DEFAULT '',  
                                GROUPNO          TEXT DEFAULT '',  
                                MEMBERNO         TEXT DEFAULT '',  
                                CASH             REAL DEFAULT 0, 
                                REMITAMT         REAL DEFAULT 0, 
                                CREDITAMT        REAL DEFAULT 0, 
                                GOODAMT          REAL DEFAULT 0, 
                                PAYMETHODS       TEXT DEFAULT '',  
                                DISCOUNTAMT      REAL DEFAULT 0, 
                                TOTALADDAMT      REAL DEFAULT 0, 
                                TOTALADDPOINTAMT REAL DEFAULT 0, 
                                CARRIERID1       TEXT DEFAULT '',  
                                NPOBAN           TEXT DEFAULT '',  
                                IPASSAMT         REAL DEFAULT 0, 
                                IPASSTAXAMT      REAL DEFAULT 0, 
                                LINEPAYAMT       REAL DEFAULT 0, 
                                ISEXPORT         TEXT DEFAULT '',  
                                ISUPLOAD         TEXT DEFAULT '',  
                                UPLOADDATETIME   TEXT DEFAULT '',  
                                UPLOADEMPNO      TEXT DEFAULT '',  
                                PRIMARY KEY (COMPID, ORDERNO, RETURNSRLNO)
                            )";

            string TBSO14 = $@"CREATE TABLE IF NOT EXISTS TBSO14 (
                                COMPID         TEXT DEFAULT '',  
                                ORDERNO        TEXT DEFAULT '',  
                                RETURNSRLNO    INTEGER DEFAULT 0,
                                SRLNO          TEXT DEFAULT '',  
                                MTRLNO         TEXT DEFAULT '',  
                                MTRLNAME       TEXT DEFAULT '',  
                                MEMO           TEXT DEFAULT '',  
                                AMOUNT         REAL DEFAULT 0,  
                                UNITCOSTPRICE  REAL DEFAULT 0,  
                                UNITADDPRICE   REAL DEFAULT 0,  
                                UNITADDPOINT   REAL DEFAULT 0,  
                                UNITSALEPRICE  REAL DEFAULT 0,  
                                RETURNAMOUNT   REAL DEFAULT 0,  
                                COSTAMT        REAL DEFAULT 0,  
                                ADDAMT         REAL DEFAULT 0,  
                                ADDPOINTAMT    REAL DEFAULT 0,  
                                SALEAMT        REAL DEFAULT 0,  
                                INVAMT         REAL DEFAULT 0,  
                                ACCRETURNAMOUT REAL DEFAULT 0,  
                                ODDAMOUT       REAL DEFAULT 0,  
                                INVENTORYTYPE  TEXT DEFAULT '',  
                                MINO           TEXT DEFAULT '',  
                                ORDERSRLNO     TEXT DEFAULT '',  
                                DISCOUNT       REAL DEFAULT 0,  
                                BEFTAXAMT      REAL DEFAULT 0,  
                                TAXAMT         REAL DEFAULT 0,
                                ISUPLOAD       TEXT DEFAULT '',
                                UPLOADDATETIME TEXT DEFAULT '',
                                UPLOADEMPNO    TEXT DEFAULT '',
                                PRIMARY KEY (COMPID, ORDERNO, RETURNSRLNO, SRLNO)
                            )";

            sqlite.execute(TBSO11);
            sqlite.execute(TBSO12);
            sqlite.execute(TBSO21);
            sqlite.execute(TBAI04);
            sqlite.execute(TBAI05);
            sqlite.execute(TBSO13);
            sqlite.execute(TBSO14);
        }
    }
}