using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using FolderFlattener.Interfaces;
using SharpCompress.Archives.Rar;

namespace FolderFlattener.Implementations.Nodes
{
    internal class RarNode : IFileOrFolderNode
    {
        private readonly string pathToRarFile;

        public RarNode(string pathToRarFile)
        {
            this.pathToRarFile = pathToRarFile;

            using (var archive = RarArchive.Open(pathToRarFile))
            {
                
            }

        }

        IReadOnlyList<IFileOrFolderNode> IFileOrFolderNode.Children => throw new NotImplementedException();

        public void OutputRecursively(ISequentialFilenameGenerator filenameGenerator)
        {
            throw new NotImplementedException();
        }
    }
}
