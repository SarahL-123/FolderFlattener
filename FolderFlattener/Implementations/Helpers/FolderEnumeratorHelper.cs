using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Implementations.Helpers
{
    internal static class FolderEnumeratorHelper
    {
        /// <summary>
        /// Returns the file names and the extensions (extensions include '.') for every file in the folder
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static IReadOnlyList<(string FileNameNoExtension, string ExtensionWithDot)> GetFileNamesAndExtensionsInFolder(
            string folderName)
        {
            var toReturn = Directory
                .GetFiles(folderName)
                .Select(filePath => Path.GetFileName(filePath))
                .Select(fileName =>
                {
                    string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
                    string ext = Path.GetExtension(fileName);
                    return (fileNameOnly, ext);
                })
                .ToList();

            return toReturn;

        }

        /// <summary>
        /// Returns all the sub folders in a folder.
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static IReadOnlyList<string> GetDirectoriesInFolder(string folderPath)
        {
            return Directory.GetDirectories(folderPath).ToList();
        }
    }
}
