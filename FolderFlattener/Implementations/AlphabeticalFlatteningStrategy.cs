using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using FolderFlattener.Implementations.Helpers;
using FolderFlattener.Interfaces;

namespace FolderFlattener.Implementations
{
    internal class AlphabeticalFlatteningStrategy : IFlatteningStrategy
    {

        /* How this will work internally
         * - The simple way is to recursively loop through all the folders and just mark each one 
         *   as a leaf if it has files in it
         * - If yes, store it in the leafs
         * 
         */


        /// <summary>
        /// Contains all the items in order<br/>
        /// If many child items then use better data structure, for now it doesn't matter
        /// </summary>
        private IList<FileNameAndExtRecord> folderItems;

        public AlphabeticalFlatteningStrategy()
        {
            folderItems = new List<FileNameAndExtRecord>();
        }

        //**********************************

        public void DetectFiles(IEnumerable<string> folderPaths)
        {
            folderItems.Clear();


            //recursively loop through the folders,
            // If it has files, store it in the leaf folder
            int leafNumber = 1; // start from 1 for humans to read
            foreach (var folderPath in folderPaths)
            {
                leafNumber = BuildFolderItemsRecursively(folderPath, leafNumber);
            }

        }


        /// <summary>
        /// Adds items to <see cref="folderItems"/> recursively.Returns the new leaf
        /// number to use.
        /// </summary>
        /// <param name="folderFilePath"></param>
        /// <param name="leafNumber"></param>
        /// <returns></returns>
        private int BuildFolderItemsRecursively(
            string folderFilePath,
            int leafNumber)
        {
            // Get files, and store them
            var fileNamesAndExts = 
                Helpers.FolderEnumeratorHelper.GetFileNamesAndExtensionsInFolder(folderFilePath)
                .OrderBy(itm => itm.FileNameNoExtension)
                .ToList();

            if (fileNamesAndExts.Count != 0)
            {
                // Store each item
                int itemNumber = 1;

                foreach (var fileNameAndExt in fileNamesAndExts)
                {
                    folderItems.Add(new FileNameAndExtRecord(
                        PathToFolder: folderFilePath,
                        FileNameNoExtension: fileNameAndExt.FileNameNoExtension,
                        Extension: fileNameAndExt.ExtensionWithDot,
                        LeafNumber: leafNumber,
                        ItemNumber: itemNumber));

                    itemNumber++;
                }

                // This item used up 1 leaf number, because it has items in it
                leafNumber++;
            }

            // Then, see if there are any sub directories, if so call it recursively
            IReadOnlyList<string> subDirs = Helpers.FolderEnumeratorHelper.GetDirectoriesInFolder(folderFilePath);
            foreach (var subDir in subDirs)
            {
                leafNumber = BuildFolderItemsRecursively(
                    folderFilePath: subDir,
                    leafNumber: leafNumber);
            }

            return leafNumber;

        }




        //**********************************

        public void Output(string outputFolderPath)
        {
            // First we need to calculate the largest item number
            int largestLeafNumber = 0;
            int largestItemNumber = 0;

            foreach (var folderItem in folderItems)
            {
                if (folderItem.LeafNumber > largestLeafNumber) largestLeafNumber = folderItem.LeafNumber;
                if (folderItem.ItemNumber > largestItemNumber) largestItemNumber = folderItem.ItemNumber;
            }

            int largestLeafNumDigits = largestLeafNumber.ToString().Length;
            int largestItemNumDigits = largestItemNumber.ToString().Length;

            //***********************

            foreach (var item in this.folderItems)
            {


                string newFileName = FormatToNewFileName(
                    leafNumber: item.LeafNumber,
                    itemNumber: item.ItemNumber,
                    largestLeafNumDigits: largestLeafNumDigits,
                    largestItemNumDigits: largestItemNumDigits);

                Helpers.FileCopyHelper.CopyRename(
                    item,
                    newFileName,
                    outputFolderPath
                    );
            }

        }


        /// <summary>
        /// This only makes the final file name, not the folder or the extension
        /// </summary>
        private string FormatToNewFileName(
            int leafNumber,
            int itemNumber,
            int largestLeafNumDigits,
            int largestItemNumDigits)
        {
            // We need to format the number to strings, but it 

            string leafNumberPadded = leafNumber.ToString();
            leafNumberPadded = leafNumberPadded.PadLeft(
                totalWidth: largestLeafNumDigits,
                paddingChar: '0');

            string itemNumberPadded = itemNumber.ToString();
            itemNumberPadded = itemNumberPadded.PadLeft(
                totalWidth: largestItemNumDigits,
                paddingChar: '0');

            return $"{leafNumberPadded}_{itemNumberPadded}";
        }
    }
}
