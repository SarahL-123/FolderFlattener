using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Interfaces
{
    /// <summary>
    /// Shows how to flatten the file structure.<br/>
    /// Lifespan: create a new one every time user wants to flatten files
    /// </summary>
    public interface IFlatteningStrategy
    {
        /// <summary>
        /// Takes some folder paths (in order) and stores all the contents.
        /// </summary>
        /// <param name="folderPaths">The file path(s)</param>
        public void DetectFiles(IEnumerable<string> folderPaths);

        /// <summary>
        /// Only call this after <see cref="DetectFiles(string[])"/><br/>
        /// Outputs the files to a specific folder.
        /// </summary>
        /// <param name="outputFolderPath"></param>
        public void Output(string outputFolderPath);
    }
}
