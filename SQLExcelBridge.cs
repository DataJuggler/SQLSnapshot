

#region using statements

using DataJuggler.Net7;
using DataJuggler.UltimateHelper;
using DataJuggler.Excelerate;
using DataJuggler.Net7.Delegates;

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
            
            #region ExportSnapshot(string connectionString, string path, List<string> ignoreTables = null, ProgressStatusCallback callback = null, bool appendPartialGuid = true, string fontName = "Verdana", double fontSize = 11)
            /// <summary>
            /// This method Exports a Snapshot of a SQL Server database including all data rows.
            /// <param name="connectionString">A connectionstring with read permission so the schema and data rows can be loaded</param>
            /// <param name="path">The path to save the Excel file. This path must end in .xlsx.</param>
            /// <param name="ignoreTables">This optional parameter is a list of all table names to excluded.</param>
            /// <param name="callback">This optional parameter is used to get progress callbacks during export operations.</param>
            /// <param name="appendPartialGuid">This optional parameter defaults to true. If true, the path parameter will be appended with 12 characters of a partial guid to ensure uniqueness in a folder.</param>
            /// <param name="fontName">This optional parameter defaults to Verdana. Change this value to write to Excel in a different font.</param>
            /// <param name="fontSize">This optional parameter default to font size 11. Change this value to write to Excel in a different font size.</param>
            /// </summary>
            public static SQLExportResult ExportSnapshot(string connectionString, string path, List<string> ignoreTables = null, ProgressStatusCallback callback = null, bool appendPartialGuid = true, string fontName = "Verdana", double fontSize = 11)
            {
                // initial value
                SQLExportResult result = new SQLExportResult();

                // locals
                bool skipTable = false;
                
                // Create a new instance of a 'SQLDatabaseConnector' object.
                SQLDatabaseConnector connector = new SQLDatabaseConnector();
                
                // Set the connectionstring
                connector.ConnectionString = connectionString;
                
                // Open the connection
                connector.Open();
                
                // create a database
                Database database = new Database();

                // If the callback object exists
                if (NullHelper.Exists(callback))
                {
                    // Callback to the caller to indicate status
                    callback(0, 0, "Reading database schema, please wait.", 0, 0, "");
                }
                
                // load the database
                database = connector.LoadDatabaseSchema(database);
                
                // Set the tables
                List<DataTable> tables = database.Tables;
                
                // Load the tables
                tables = connector.LoadDataTablesData(tables, ignoreTables, callback);
                
                // If the tables collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(tables))
                {
                     // If the callback object exists
                    if (NullHelper.Exists(callback))
                    {
                        // Callback to the caller to indicate status
                        callback(tables.Count * 2, 0, "Reading database schema complete. Loading Data please wait.", 0, 0, "");
                    }

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
                    ExcelHelper.CreateWorkbook(worksheetInfo, worksheets, callback);
                }
                
                // return value
                return result;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
