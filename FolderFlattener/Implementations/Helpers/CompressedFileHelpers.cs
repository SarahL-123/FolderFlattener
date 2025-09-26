using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;

namespace FolderFlattener.Implementations.Helpers
{
    internal static class CompressedFileHelpers
    {
        public static bool IsCompressedFile(string fileExtension)
        {
            fileExtension = fileExtension.ToLower();
            if (fileExtension == ".rar") return true;

            return false;
        }

        public static int GetNumItemsInCompressedFile(
            string pathToFolderNotIncludingFile,
            string fileNameOnly,
            string fileExtensionWithDotOnly)
        {
            // for now only support rar so just hard code it
            using var archive = RarArchive.Open(Path.Combine(pathToFolderNotIncludingFile, fileNameOnly + fileExtensionWithDotOnly));
            return archive.Entries.Where(entry => !entry.IsDirectory).Count();
        }

        public static void WriteCompressedFileContents(
            FileNameAndExtRecord fileNameAndExtRecord,
            NameIterator nameIterator,
            string outputFolderPath)
        {
            using var archive = RarArchive.Open(Path.Combine(
                fileNameAndExtRecord.PathToFolder,
                fileNameAndExtRecord.FileNameNoExtension + fileNameAndExtRecord.Extension));

            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                string fileExt = System.IO.Path.GetExtension(entry.Key!);
                if (!fileExt.Contains('.'))
                {
                    fileExt = "." + fileExt;
                }

                string outputFileNameNoExt = nameIterator.GetFilenameAdvanceNext();

                string outputFinalPath = System.IO.Path.Combine(
                    outputFolderPath,
                    outputFileNameNoExt + fileExt);

                entry.WriteToFile(
                    destinationFileName: outputFinalPath);

            }

        }
    }
}
