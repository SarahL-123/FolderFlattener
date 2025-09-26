using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FolderFlattener.Interfaces;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace FolderFlattenerWpfUi
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        private readonly IFlatteningStrategyFactory flatteningStrategyFactory;

        public MainWindowVM(IFlatteningStrategyFactory flatteningStrategyFactory)
        {
            this.flatteningStrategyFactory = flatteningStrategyFactory;

            this.SelectTargetFolderCommand = new RelayCommand(SelectTargetFolderCommandExecute);
            this.AddSourceFoldersCommand = new RelayCommand(this.AddSourceFoldersCommandExecute);
            this.ResetSourceFoldersCommand = new RelayCommand(this.ResetSourceFoldersCommandExecute);

            this.FlattenCommand = new RelayCommand(
                FlattenCommandExecute,
                FlattenCommandCanExecute);
        
        }

        /// <summary>
        /// The source folders to get files from
        /// </summary>
        public ReadOnlyObservableCollection<string> SourceFolders
        {
            get { return new ReadOnlyObservableCollection<string>(sourceFolders); }
        }
        private ObservableCollection<string> sourceFolders = new ObservableCollection<string>();


        /// <summary>
        /// Where the files are to be outputted
        /// </summary>
        public string? TargetFolder
        {
            get { return targetFolder; }
            protected set
            {
                this.targetFolder = value;
                OnPropChanged(nameof(this.TargetFolder));
                this.FlattenCommand.NotifyCanExecuteChanged();
            }
        }
        private string? targetFolder = null;

        public RelayCommand SelectTargetFolderCommand { get; }
        private void SelectTargetFolderCommandExecute()
        {
            var folderDiag = new CommonOpenFileDialog();
            folderDiag.IsFolderPicker = true;

            if (folderDiag.ShowDialog() == CommonFileDialogResult.Ok)
            {
                targetFolder = folderDiag.FileName;
            }

            this.FlattenCommand.NotifyCanExecuteChanged();
        }

        public RelayCommand AddSourceFoldersCommand { get; }

        private void AddSourceFoldersCommandExecute()
        {
            var folderDiag = new CommonOpenFileDialog();
            folderDiag.IsFolderPicker = true;
            folderDiag.Multiselect = true;

            if (folderDiag.ShowDialog() == CommonFileDialogResult.Ok)
            {
                foreach (var filePath in folderDiag.FileNames)
                {
                    sourceFolders.Add(filePath);
                }
            }

            this.FlattenCommand.NotifyCanExecuteChanged();
        }

        public RelayCommand ResetSourceFoldersCommand { get; }
        private void ResetSourceFoldersCommandExecute()
        {
            this.sourceFolders.Clear();
            this.FlattenCommand.NotifyCanExecuteChanged();
        }




        public RelayCommand FlattenCommand { get; }

        

        private void FlattenCommandExecute()
        {
            if (this.targetFolder is null || this.sourceFolders.Count == 0)
            {
                throw new InvalidOperationException(
                    "Programming error: cannot call this method if target folder is null, or " +
                    "there are no items to copy from");
            }

            // for now just hard code this in
            IFlatteningStrategy strat = this.flatteningStrategyFactory.Create(FlatteningStrategyEnum.Alphabetical);
            strat.DetectFiles(
                folderPaths: this.sourceFolders);

            strat.Output(this.targetFolder);
        }

        private bool FlattenCommandCanExecute()
        {
            return
                (targetFolder is not null)
                && (sourceFolders.Count > 0);
        }


        //*******************************

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}
