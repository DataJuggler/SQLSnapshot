

#region using statements

using DataJuggler.Net7;

#endregion

namespace SQLSnapshot
{

    #region class SQLExportResult
    /// <summary>
    /// This class is used to return the results of ExportSnapshot of a SQL Server database 
    /// </summary>
    public class SQLExportResult
    {
        
        #region Private Variables
        private List<DataTable> tables;
        private int rowsCount;
        #endregion

        #region Properties

            #region RowsCount
            /// <summary>
            /// This property gets or sets the value for 'RowsCount'.
            /// </summary>
            public int RowsCount
            {
                get { return rowsCount; }
                set { rowsCount = value; }
            }
            #endregion
            
            #region Tables
            /// <summary>
            /// This property gets or sets the value for 'Tables'.
            /// </summary>
            public List<DataTable> Tables
            {
                get { return tables; }
                set { tables = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
