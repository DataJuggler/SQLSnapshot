# SQLSnapshot
DataJuggler.SQLSnapshot allows you to export a snapshot of a SQL Server database including 
all data rows to Excel with one line of code. 

<img src=https://github.com/DataJuggler/SharedRepo/blob/master/Shared/Images/Camera.png width=256 height=256>

Pass in a connectionstring and a path to save.

    using DataJuggler.SQLSnapshot;

    // Set a connectionstring
    string connectionString = ConnectionTextBox.Text;

    // Set the export path
    string exportPath = @"c:\Temp\DataJugglerExport.xlsx";

    // export the result
    SQLExportResult result = SQLExcelBridge.ExportSnapshot(connectionString, exportPath);

The file name for the Excel file will be saved and combined with a partial guid, so it will be unique
in a folder.

Future updates and features may include:

1. Ability to only write changes since last snapshot
2. Export database schema
3. Consolidate data to update a Test or Dev server with production data 

The reason I created this project is SQL Backups are great for data protection, however this requires 
restoring the entire database to lookup values. There are also times I need to discover when data 
changed to help determine when a new bug was introduced.

If you have any problems, please create an issue and welcome any feedback as to if you think this
project is valuable. I am considering building a Windows Service to create a commercial product
out of this with more robust features.
