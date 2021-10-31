# CshLibs

Library with (currently) five classes for providing methods for quickly setting up cross platform (.NET (core)) OS specific settings. 

## OperatingSystemInfo 
Provides methods IsWindows(), IsMacOS(), and IsLinux(). 

## FolderSettings
Provides GetFolderApp(), GetFolderProgramData(), GetFolderAppdataLocal(), GetFolderAppdataRoaming(), GetFolderDesktop(), and  GetFolderDocuments(). 

## FileFolder 
Provides IsDirectoryWritable(). 

## ObjToXml 
Provides WriteXML(), ReadXML(). 

## Assy 
Provides GetAssy() which reads Title, Company, Version and FileVersion from assembly / fileversioninfo. 

# Usage 
See examples in TestToolsLibrary in Program.cs. 
