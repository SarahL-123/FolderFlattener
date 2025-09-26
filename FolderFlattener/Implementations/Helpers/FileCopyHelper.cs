using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
