using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderFlattener.Interfaces;

namespace FolderFlattener.Implementations.Nodes
{
    internal class CompressedArchiveNode : IFileOrFolderNode
    {
        public CompressedArchiveNode(string filename)
        {
            
        }

        public IReadOnlyList<IFileOrFolderNode> Children => children;
        private List<IFileOrFolderNode> children = new List<IFileOrFolderNode>(0);

        public void OutputRecursively(ISequentialFilenameGenerator filenameGenerator)
        {
            // don't do anything in here, it should be handled by the parent?
        }
    }
}
