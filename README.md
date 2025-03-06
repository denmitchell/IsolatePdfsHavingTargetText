This .NET 9 Console Application will search a folder for PDF files containing search text 
and copy files that have the search text into an "output" subfolder.  Unless the program 
is modified, the program's executable should reside in the same folder as the source documents 
that are searched.

To generate a standalone executable, launch a terminal window in the project directory and execute:
> dotnet publish -r win-x64 -p:PublishSingleFile=true --self-contained true
