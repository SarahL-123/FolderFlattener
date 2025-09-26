using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Interfaces
{
    public interface ISequentialFilenameGenerator
    {
        /// <summary>
        /// Gets the name of the next file (only the filename part, not extension or path)
        /// </summary>
        /// <returns></returns>
        public string GetNextFilename();


        public void AdvanceToNextChapter();
    }
}
