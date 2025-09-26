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
        private IList<FileNameAndExtRecord> fileRecords;

        public AlphabeticalFlatteningStrategy()
        {
            fileRecords = new List<FileNameAndExtRecord>();
        }

        //**********************************

        public void DetectFiles(IEnumerable<string> folderPaths)
        {
            fileRecords.Clear();


            //recursively loop through the folders, and count how many items there are
            int itemNumber = 1; // start from 1 for humans

            foreach (var folderPath in folderPaths)
            {
                itemNumber = BuildFolderItemsRecursively(folderPath, itemNumber);
            }

        }


        /// <summary>
        /// Adds items to <see cref="fileRecords"/> recursively.Returns the new item
        /// number to use.
        /// </summary>
        /// <param name="folderFilePath"></param>
        /// <param name="leafNumber"></param>
        /// <returns></returns>
        private int BuildFolderItemsRecursively(
            string folderFilePath,
            int itemNumber)
        {
            // Get files, and store them
            var fileNamesAndExts = 
                Helpers.FolderEnumeratorHelper.GetFileNamesAndExtensionsInFolder(folderFilePath)
                .OrderBy(itm => itm.FileNameNoExtension)
                .ToList();

            if (fileNamesAndExts.Count != 0)
            {
                foreach (var fileNameAndExt in fileNamesAndExts)
                {
                    int numFilesInFile = 1;
                    bool isCompressedFile = false;

                    // determine if the file is a compressed file
                    if (CompressedFileHelpers.IsCompressedFile(fileNameAndExt.ExtensionWithDot))
                    {
                        numFilesInFile = CompressedFileHelpers.GetNumItemsInCompressedFile(
                            pathToFolderNotIncludingFile: folderFilePath,
                            fileNameOnly: fileNameAndExt.FileNameNoExtension,
                            fileExtensionWithDotOnly: fileNameAndExt.ExtensionWithDot);

                        isCompressedFile = true;
                    }


                    fileRecords.Add(new FileNameAndExtRecord(
                        PathToFolder: folderFilePath,
                        FileNameNoExtension: fileNameAndExt.FileNameNoExtension,
                        Extension: fileNameAndExt.ExtensionWithDot,
                        NumFilesInFile: numFilesInFile,
                        isCompressedFile: isCompressedFile));

                    itemNumber++;
                }
            }

            // Then, see if there are any sub directories, if so call it recursively
            IReadOnlyList<string> subDirs = Helpers.FolderEnumeratorHelper.GetDirectoriesInFolder(folderFilePath);
            foreach (var subDir in subDirs)
            {
                itemNumber = BuildFolderItemsRecursively(
                    folderFilePath: subDir,
                    itemNumber: itemNumber);
            }

            return itemNumber;

        }




        //**********************************

        public void Output(string outputFolderPath)
        {
            // First we need to calculate the largest item number
            int totalItems = 0;

            foreach (var folderItem in fileRecords)
            {
                totalItems += folderItem.NumFilesInFile;
            }

            int numDigits = totalItems.ToString().Length;

            var nameIterator = new NameIterator(numDigits);

            //***********************

            foreach (var item in this.fileRecords)
            {
                // handle compressed files
                if (item.isCompressedFile)
                {
                    CompressedFileHelpers.WriteCompressedFileContents(
                        item,
                        nameIterator,
                        outputFolderPath
                        );
                }
                else
                {
                    string newFileName = nameIterator.GetFilenameAdvanceNext();

                    Helpers.FileCopyHelper.CopyRename(
                        item,
                        newFileName,
                        outputFolderPath
                        );
                }


                    
            }

        }


        
    }
}
