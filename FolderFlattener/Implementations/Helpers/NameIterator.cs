using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderFlattener.Implementations.Helpers
{
    internal class NameIterator
    {
        private readonly int numDigits;
        private int current = 0;

        public NameIterator(int numDigits)
        {
            this.numDigits = numDigits;
        }

        public string GetFilenameAdvanceNext()
        {
            string fileName = current.ToString();
            if (fileName.Length < numDigits)
            {
                fileName = fileName.PadLeft(numDigits, '0');
            }

            current++;

            return fileName;


        }


    }
}
