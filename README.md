# SQLSnapshot
DataJuggler.SQLSnapshot allows you to export a snapshot of a SQL Server database including 
all data rows to Excel with a few lines of code (could be written as one if we were charged by the line).

Video<br>
https://youtu.be/dOA_8EJ_xWA

<img src=https://github.com/DataJuggler/SharedRepo/blob/master/Shared/Images/SQLSnapshot.png width=540 height=360>

Pass in a connectionstring and a path to save.

    using DataJuggler.SQLSnapshot;

    // Set a connectionstring - make sure to include Encrypt=False as shown below
    string connectionString = @"Data Source=ServerName\SQLExpress;Initial Catalog=DataJuggler;Integrated Security=True;Encrypt=False;";

    // Set the export path
    string exportPath = @"c:\Temp\DataJugglerExport.xlsx";

    // export the result (one line of code. Is this useful, let me know by starring this project please).
    SQLExportResult result = SQLExcelBridge.ExportSnapshot(connectionString, exportPath);
    

The file name for the Excel file will be saved and combined with a partial guid, so it will be unique
in a folder.

This project combines two Nuget packages of mine:
1. DataJuggler.Net7 - Which reads the database schema
2. DataJuggler.Excelerate - Writes to Excel

Update: Exclude Tables bug has been fixed.

Known Issues:
1. I attempted to format date columns, but my first attempt didn't work. Dates show up as numbers until you format the Excel column.

Future updates and features may include:

1. Ability to only write changes since last snapshot
2. Export database schema for tables and fields
3. Consolidate data to update a Test or Dev server with production data
4. Pass in a list of tables and / or fields to exclude (completed, but after testing the first attempt at this didn't work.)

The reason I created this project is SQL Backups are great for data protection, however this requires 
restoring the entire database to lookup values. There are also times I need to discover when data 
changed to help determine when a new bug was introduced.

If you have any problems, please create an issue and I welcome any feedback as to if you think this
project is useful or ways it can be improved. I am considering building a Windows Service to create a commercial product
out of this with more robust features.
