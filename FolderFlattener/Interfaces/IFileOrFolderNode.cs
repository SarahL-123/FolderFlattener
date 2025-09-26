using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Interfaces
{
    /// <summary>
    /// Represents a single file possibly in a zipped archive.<br/>
    /// When the output function is called (with a target path), it should take the original file
    /// and 
    /// 
    /// </summary>
    public interface IFileOrFolderNode
    {
        /// <summary>
        /// Outputs this and all of its child nodes, using the filename generator to get the filenames
        /// </summary>
        public void OutputRecursively(ISequentialFilenameGenerator filenameGenerator);

        public IReadOnlyList<IFileOrFolderNode> Children { get; }

    }
}
