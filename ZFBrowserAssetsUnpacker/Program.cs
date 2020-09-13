using System;
using System.IO;

namespace ZFBrowserAssetsUnpacker
{
    class Program
    {
        private static string BROWSER_ASSETS_PATH = "browser_assets";
        private static string SITES_FOLDER = "sites";
        private static string BA_FILE_HEADER = "zfbRes_v1";
        
        static void Main(string[] args)
        {
            Console.WriteLine("ZFB BrowserAssets file unpacker - by Lutonite");
            
            using (BinaryReader binaryReader = new BinaryReader(File.Open(BROWSER_ASSETS_PATH, FileMode.Open)))
            {
                string zfbHeader = binaryReader.ReadString();
                if (!zfbHeader.Equals(BA_FILE_HEADER))
                {
                    throw new FormatException("Unsupported browser_assets file format");
                }
                
                int fileCount = binaryReader.ReadInt32();
                FILEDATA[] fileArray = new FILEDATA[fileCount];
                
                // Data Extraction
                for (int i = 0; i < fileCount; i++)
                {
                    fileArray[i].name = binaryReader.ReadString();
                    fileArray[i].offset = binaryReader.ReadInt64();
                    fileArray[i].length = binaryReader.ReadInt32();
                   
                    byte[] dataArray = new byte[fileArray[i].length];
                    long position = binaryReader.BaseStream.Position;
                    binaryReader.BaseStream.Position = (int) fileArray[i].offset;
                    binaryReader.Read(dataArray, 0, fileArray[i].length);
                    fileArray[i].data = dataArray;
                    binaryReader.BaseStream.Position = position;
                }
                
                // Data output to files
                if (Directory.Exists(SITES_FOLDER))
                {
                    Directory.Delete(SITES_FOLDER);
                    Directory.CreateDirectory(SITES_FOLDER);
                }
                
                for (int j = 0; j < fileCount; j++)
                {
                    Directory.CreateDirectory(SITES_FOLDER + fileArray[j].name);
                    Console.WriteLine("Writing file [{0}]", fileArray[j].name);

                    FileStream file = File.Create(SITES_FOLDER + fileArray[j].name);
                    using (BinaryWriter binaryWriter = new BinaryWriter(file))
                    {
                        binaryWriter.Write(fileArray[j].data);
                    }
                }
            }
            
            Console.WriteLine("Unpacking complete.");
        }
    }
}
