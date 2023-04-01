

#region using statements

using DataJuggler.Net7;
using DataJuggler.UltimateHelper;
using DataJuggler.Excelerate;

#endregion

namespace DataJuggler.SQLSnapshot
{

    #region class SQLExcelBridge
    /// <summary>
    /// This class is used to export SQL Server data to Excel
    /// </summary>
    public class SQLExcelBridge
    {
        
        #region Methods
            
            #region ExportSnapshot(string connectionString, string path, bool appendPartialGuid = true, string fontName = "Verdana", double fontSize = 11, List<string> ignoreTables = null, List<string> ignoreFields = null)
            /// <summary>
            /// method Export Snapshot
            /// </summary>
            public static SQLExportResult ExportSnapshot(string connectionString, string path, bool appendPartialGuid = true, string fontName = "Verdana", double fontSize = 11, List<string> ignoreTables = null, List<string> ignoreFields = null)
            {
                // initial value
                SQLExportResult result = new SQLExportResult();

                // local
                bool skipTable = false;
                
                // Create a new instance of a 'SQLDatabaseConnector' object.
                SQLDatabaseConnector connector = new SQLDatabaseConnector();
                
                // Set the connectionstring
                connector.ConnectionString = connectionString;
                
                // Open the connection
                connector.Open();
                
                // create a database
                Database database = new Database();
                
                // load the database
                database = connector.LoadDatabaseSchema(database);
                
                // Set the tables
                List<DataTable> tables = database.Tables;
                
                // Load the tables
                tables = connector.LoadDataTablesData(tables, ignoreTables);
                
                // If the tables collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(tables))
                {
                    // if the value for appendPartialGuid is true
                    if (appendPartialGuid)
                    {
                        // Add a partial guid to the fileName so it is unique in a folder
                        path = FileHelper.CreateFileNameWithPartialGuid(path, 12);
                    }

                    // Create the worksheetInfo
                    FileInfo worksheetInfo = new FileInfo(path);

                    // Create a new collection of 'LoadWorksheetInfo' objects.
                    List<LoadWorksheetInfo> worksheets = new List<LoadWorksheetInfo>();

                    // Iterate the collection of DataTable objects
                    foreach (DataTable table in tables)
                    {
                        // reset
                        skipTable = false;

                        // If the ignoreTables collection exists and has one or more items
                        if (ListHelper.HasOneOrMoreItems(ignoreTables))
                        {
                            // Iterate the collection of string objects
                            foreach (string tableName in ignoreTables)
                            {
                                // if this tableName matches
                                if (TextHelper.IsEqual(tableName, table.Name))
                                {
                                    // this table will be skipped
                                    skipTable = true;
                                }
                            }
                        }

                        // if the value for skipTable is false
                        if (!skipTable)
                        {
                            // Create a new instance of a 'LoadWorksheetInfo' object.
                            LoadWorksheetInfo loadWorksheetInfo = new LoadWorksheetInfo();

                            // Create the sheetName
                            loadWorksheetInfo.SheetName = table.Name;

                            // Set the rows
                            loadWorksheetInfo.Rows = table.Rows;

                            // Set the fields so the fieldnames are exported
                            loadWorksheetInfo.Fields = table.Fields;

                            // Add this worksheet
                            worksheets.Add(loadWorksheetInfo);

                            // Add the rows for this table
                            result.RowsCount += table.Rows.Count;
                        }
                    }

                    // Set the result
                    result.Tables = tables;

                    // Create a workbook
                    ExcelHelper.CreateWorkbook(worksheetInfo, worksheets, fontName, fontSize);
                }
                
                // return value
                return result;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
