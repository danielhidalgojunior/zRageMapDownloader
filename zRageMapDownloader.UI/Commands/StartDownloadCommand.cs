﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using zRageMapDownloader.ViewModels;

namespace zRageMapDownloader.Commands
{
    public class StartDownloadCommand : ICommand
    {
        DownloadMapsViewModel _vm { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public StartDownloadCommand(DownloadMapsViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_vm.MapManager?.MapsDirectory) && Directory.Exists(_vm.MapManager?.MapsDirectory);
        }

        public void Execute(object parameter)
        {
            _vm.StartDownload();
        }
    }
}
