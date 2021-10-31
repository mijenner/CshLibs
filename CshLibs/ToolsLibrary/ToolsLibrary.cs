using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace ToolsLibrary
{
    public static class OperatingSystemInfo
    {
        // Usage:
        //   if(OperatingSystem.IsWindows()) ... 
        //   if(OperatingSystem.IsMacOS()) ... 
        public static bool IsWindows() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }


    public static class FolderSettings
    {

        public static bool GetFolderApp(ref string path)
        {
            try
            {
                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }
        }


        public static bool GetFolderProgramData(ref string path)
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }

        }


        public static bool GetFolderAppdataLocal(ref string path)
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }

        }

        public static bool GetFolderAppdataRoaming(ref string path)
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }

        }

        public static bool GetFolderDesktop(ref string path)
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }

        }

        public static bool GetFolderDocuments(ref string path)
        {
            try
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                // throw;
            }

        }

    }

    public static class FileFolder
    {

        public static bool IsDirectoryWritable(string dirPath, bool throwIfFails = false)
        {
            try
            {
                using (FileStream fs = File.Create(Path.Combine(dirPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
                { }
                return true;
            }
            catch
            {
                if (throwIfFails)
                    throw;
                else
                    return false;
            }
        }
    }

    public static class ObjToXml
    {
        public static void WriteXML<T>(T anObject, string fileNameWithPath)
        {

            // Note: Object, must have a constructor without parameters for
            // below to work AND only properties (public) are saved! 
            XmlSerializer writer = new XmlSerializer(typeof(T));
            FileStream file = File.Create(fileNameWithPath);
            writer.Serialize(file, anObject);
            file.Close();
        }

        public static void ReadXML<T>(ref T anObject, string fileNameWithPath)
        {
            XmlSerializer reader = new XmlSerializer(typeof(T));
            StreamReader file = new StreamReader(fileNameWithPath);
            anObject = (T)reader.Deserialize(file);
            file.Close();

        }
    }

    public class About
    {
        private string title;
        private string company;
        private string version;
        private string fileVersion;

        public About() 
        {
            this.title = ""; 
            this.company = ""; 
            this.version = ""; 
            this.fileVersion = ""; 
        }

        public string Title { 
            get {
                return title; 
            }
            set{
                title = value; 
            } }
        public string Company { 
            get{
                return company; 
            }
            set {
                company = value; 
            } }
        public string Version { 
            get{
                return version; 
            } 
            set{
                version = value; 
            } }
        public string FileVersion { 
            get{
                return fileVersion; 
            } 
            set{
                fileVersion = value; 
            } }


    }

    public static class Assy
    {

        public static bool GetAssy<T>(ref About anAbout)
        {
            Assembly anAssy = typeof(T).Assembly;

            object[] attribs;
            attribs = anAssy.GetCustomAttributes(typeof(AssemblyProductAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.Title = ((AssemblyProductAttribute)attribs[0]).Product;
            }
            else
            {
                anAbout.Title = "";
            }

            attribs = anAssy.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.Company = ((AssemblyCompanyAttribute)attribs[0]).Company;
            }
            else
            {
                anAbout.Company = "";
            }

            attribs = anAssy.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.Version = ((AssemblyInformationalVersionAttribute)attribs[0]).InformationalVersion;
            }
            else
            {
                anAbout.Version = "";
            }

            attribs = anAssy.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.FileVersion = ((AssemblyFileVersionAttribute)attribs[0]).Version;
            }
            else
            {
                anAbout.FileVersion = "";
            }

            return true;
        }
    }
}
