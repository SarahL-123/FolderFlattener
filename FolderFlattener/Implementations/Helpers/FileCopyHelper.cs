using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Readers;
using SharpCompress.Writers;

namespace FolderFlattener.Implementations.Helpers
{
    internal static class FileCopyHelper
    {
        /// <summary>
        /// Copies a file to the new location and renames it
        /// </summary>
        /// <param name="originalFile"></param>
        /// <param name="newFileName">Only the file name, no extension no path</param>
        /// <param name="outputDirectory"></param>
        public static void CopyRename(
            FileNameAndExtRecord originalFile,
            string newFileName,
            string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            string finalInputPath = Path.Combine(
                originalFile.PathToFolder,
                originalFile.FileNameNoExtension + originalFile.Extension);

            string finalOutputPath = System.IO.Path.Combine(
                outputDirectory, 
                newFileName + originalFile.Extension);

            File.Copy(
                sourceFileName: finalInputPath,
                destFileName: finalOutputPath);

        }

        //public void TestABC()
        //{
        //    // works for rar!
        //    //using var archive = RarArchive.Open("test.rar");
        //    //foreach (var entry in archive.Entries)
        //    //{
        //    //    entry.WriteToFile()
        //    //}

        //    using var archive = SharpCompress.Archives.SevenZip.SevenZipArchive.Open("test.rar");
        //    foreach (var entry in archive.Entries)
        //    {
        //        entry.WriteToFile()
        //    }


        //}
    }
}
