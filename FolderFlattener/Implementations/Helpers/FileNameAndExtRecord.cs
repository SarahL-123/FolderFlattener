using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Implementations.Helpers
{
    /// <summary>
    /// Contains the full details of where each file is
    /// </summary>
    internal record FileNameAndExtRecord(
        string PathToFolder,
        string FileNameNoExtension,
        string Extension,
        int NumFilesInFile=1,
        bool isCompressedFile = false);
}
