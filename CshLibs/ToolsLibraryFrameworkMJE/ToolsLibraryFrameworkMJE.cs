using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ToolsLibraryFrameworkMJE
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

    public struct About
    {
        public string Title;
        public string Company;
        public string Version;
        public string FileVersion; 
    }

    public static class Assy
    {

        public static bool GetAssy<T>(out About anAbout)
        {
            Assembly anAssy = typeof(T).Assembly;

            object[] attribs; 
            attribs = anAssy.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.Title = ((AssemblyTitleAttribute)attribs[0]).Title;
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

            attribs = anAssy.GetCustomAttributes(typeof(AssemblyVersionAttribute), true);
            if (attribs.Length > 0)
            {
                anAbout.Version = ((AssemblyVersionAttribute)attribs[0]).Version;
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
