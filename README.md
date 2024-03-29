# SQLSnapshot

DataJuggler.SQLSnapshot allows you to export a snapshot of a SQL Server database including 
all data rows to Excel with a few lines of code.

Optional: Pass in a list of table names you wish to exclude from the export.

There is now a desktop (WinForms) example that demonstrates this project
https://github.com/DataJuggler/SQLSnapshotDesktop

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
   
# 11.17.2023: Updated to .NET 8.

This project has been updated to the latest .NET version, .NET 8.

The file name for the Excel file will be saved and combined with a partial guid, so it will be unique
in a folder.

This project combines two Nuget packages of mine:
1. DataJuggler.Net8 - Which reads the database schema
2. DataJuggler.Excelerate - Writes to Excel

Known Issues:

None at this time.

Fixed issues

1. (Fixed in 1.0.8, Excelerate version 7.2.9) Exclude Tables bug has been fixed.
2. (Fixed in 1.0.9 Excelerate version 7.2.10) Fixed format date columns as dates.

Future updates and features may include:

1. Ability to only write changes since last snapshot
2. Export database schema for tables and fields
3. Consolidate data to update a Test or Dev server with production data
4. I am looking at creating a callback delegate so client apps can create a progress bar or display current status or operation.

Completed Features
1. Pass in a list of tables to exclude (completed in version 7.2.9)

The reason I created this project is SQL Backups are great for data protection, however this requires 
restoring the entire database to lookup values. There are also times I need to discover when data 
changed to help determine when a new bug was introduced.

If you have any problems, please create an issue and I welcome any feedback as to if you think this
project is useful or ways it can be improved. I am considering building a Windows Service to create a commercial product
out of this with more robust features.

# News

8.13.2023: DataJuggler.Excelerate was updated because DataJuggler.UltimateHelper was updated.

7.24.2023:

ExcelerateWinApp has been updated with improvements for saving worksheets.
