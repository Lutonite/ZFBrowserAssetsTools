using System;
using System.IO;
using System.Linq;

namespace ZFBrowserAssetsPacker
{
    class Program
    {
        private static string BROWSER_ASSETS_PATH = "browser_assets";
        private static string SITES_FOLDER = "sites";
        
        static void Main(string[] args)
        {
            Console.WriteLine("ZFB BrowserAssets file packer - by Lutonite");
            
            string[] files = Directory
                .GetFiles(SITES_FOLDER, "*.*", SearchOption.AllDirectories)
                .Select(
                    file => file
                        .Replace(SITES_FOLDER, "")
                        .Replace("\\", "/")
                ).ToArray();

            Console.WriteLine("Detected files: [{0}]", string.Join(", ", files));
            
            if (File.Exists(BROWSER_ASSETS_PATH))
            {
                File.Delete(BROWSER_ASSETS_PATH);
            }

            using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(BROWSER_ASSETS_PATH)))
            {
                binaryWriter.Write("zfbRes_v1"); // FILE FORMAT HEADER
                binaryWriter.Write(files.Length);
            
                FILEDATA[] fileArray = new FILEDATA[files.Length];
                for (int i = 0; i < files.Length; i++)
                {
                    fileArray[i].name = files[i];
                    fileArray[i].data = File.ReadAllBytes(SITES_FOLDER + files[i]);
                    fileArray[i].length = fileArray[i].data.Length;

                    binaryWriter.Write(fileArray[i].name);
                
                    fileArray[i].offsetIntMarker = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(Int64.MaxValue);
                
                    binaryWriter.Write(fileArray[i].length);
                }
            
                for (int j = 0; j < fileArray.Length; j++)
                {
                    fileArray[j].offset = binaryWriter.BaseStream.Position;
                    binaryWriter.Write(fileArray[j].data);
                
                    long position = binaryWriter.BaseStream.Position;
                    binaryWriter.BaseStream.Position = fileArray[j].offsetIntMarker;
                    binaryWriter.Write(fileArray[j].offset);
                    binaryWriter.BaseStream.Position = position;
                }   
            }

            Console.WriteLine("Packing complete.");
        }
    }
}
