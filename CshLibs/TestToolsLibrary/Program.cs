using System;
using System.IO;
using ToolsLibrary;

namespace TestToolsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            if (OperatingSystemInfo.IsWindows())
            {
                Console.WriteLine("Platform: Windows");
            }
            else if (OperatingSystemInfo.IsMacOS())
            {
                Console.WriteLine("Platform: MacOS");
            }
            else if (OperatingSystemInfo.IsLinux())
            {
                Console.WriteLine("Platform: Linux");
            }
            


            string folder = "";
            if (FolderSettings.GetFolderApp(ref folder))
            {
                Console.WriteLine("GetFolderApp: " + folder);
            }


            if (FolderSettings.GetFolderProgramData(ref folder))
            {
                Console.WriteLine("GetFolderProgramData: " + folder);
            }

            if (FolderSettings.GetFolderAppdataRoaming(ref folder))
            {
                Console.WriteLine("GetFolderAppdataRoaming: " + folder);
            }

            if (FolderSettings.GetFolderAppdataLocal(ref folder))
            {
                Console.WriteLine("GetFolderAppdataLocal: " + folder);
            }

            if (FolderSettings.GetFolderDocuments(ref folder))
            {
                Console.WriteLine("GetFolderDocuments: " + folder);
            }
            string myDocs = folder;

            if (FolderSettings.GetFolderDesktop(ref folder))
            {
                Console.WriteLine("GetFolderDesktop: " + folder);
            }

            if (FileFolder.IsDirectoryWritable(myDocs, false))
            {
                Console.WriteLine("Folder " + myDocs + " is writable");
            }
            else
            {
                Console.WriteLine("Folder " + myDocs + " is NOT writable");
            }

            string myDocs2 = myDocs + "ABC";
            if (FileFolder.IsDirectoryWritable(myDocs2, false))
            {
                Console.WriteLine("Folder " + myDocs2 + " is writable");
            }
            else
            {
                Console.WriteLine("Folder " + myDocs2 + " is NOT writable");
            }


            // Save application settings to AppData/Roaming 
            AppSettings appSettings = new AppSettings();
            Console.WriteLine("Preparing to store application settings");
            if (FolderSettings.GetFolderAppdataRoaming(ref folder))
            {
                Console.WriteLine("Got path to AppData\\Roaming = " + folder);
            }
            else
            {
                Console.WriteLine("Problem getting AppdataRoaming path");
                return;
            }
            string serFile = Path.Combine(folder, "AppSettings.xml");
            Console.WriteLine("Saves settings to " + serFile);
            ObjToXml.WriteXML<AppSettings>(appSettings, serFile);
            AppSettings appSettings2 = new AppSettings();
            ObjToXml.ReadXML<AppSettings>(ref appSettings2, serFile);
            Console.WriteLine("After reading settings we get: ");
            Console.WriteLine(appSettings2.Heltal);
            Console.WriteLine(appSettings2.KommatalFloat);
            Console.WriteLine(appSettings2.KommatalDouble);
            Console.WriteLine(appSettings2.KommatalDecimal);
            Console.WriteLine(appSettings2.Tegn);
            Console.WriteLine(appSettings2.Streng);


            // Assembly info: 
            About anAbout = new About();

            Assy.GetAssy<Program>(ref anAbout);
            Console.WriteLine("--------------------");
            Console.WriteLine("Assembly info: ");
            Console.WriteLine("Title = " + anAbout.Title);
            Console.WriteLine("Company = " + anAbout.Company);
            Console.WriteLine("Version = " + anAbout.Version);
            Console.WriteLine("FileVersion = " + anAbout.FileVersion);

            Assy.GetAssy<About>(ref anAbout);
            Console.WriteLine("--------------------");
            Console.WriteLine("Assembly info: ");
            Console.WriteLine("Title = " + anAbout.Title);
            Console.WriteLine("Company = " + anAbout.Company);
            Console.WriteLine("Version = " + anAbout.Version);
            Console.WriteLine("FileVersion = " + anAbout.FileVersion);

            Console.ReadKey();


        }
    }

    public class AppSettings
    {
        public int Heltal { get; set; }
        public float KommatalFloat { get; set; }
        public double KommatalDouble { get; set; }
        public decimal KommatalDecimal { get; set; }
        public char Tegn { get; set; }
        public string Streng { get; set; }

        public AppSettings()
        {
            Heltal = 2;
            KommatalFloat = 3.1425f;
            KommatalDouble = 2.78;
            KommatalDecimal = 0.10m;
            Tegn = 'p';
            Streng = @"Hello, \world!";
        }

    }
}
