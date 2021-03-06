﻿using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using zRageMapDownloader.Commands;
using zRageMapDownloader.Core;
using zRageMapDownloader.Views;

namespace zRageMapDownloader.ViewModels
{
    public class ServerSelectionViewModel
    {
        public ObservableCollection<ServerModel> AvaliableServers { get; set; }
        public ServerModel SelectedServer { get; set; }

        public SelectServerContextCommand SelectServerContextCommand { get; set; }
        public OpenAboutWindowCommand OpenAboutWindowCommand { get; set; }

        public ServerSelectionViewModel()
        {
            AvaliableServers = new ObservableCollection<ServerModel>();
            SelectServerContextCommand = new SelectServerContextCommand(this);
            OpenAboutWindowCommand = new OpenAboutWindowCommand();

            PopulateAvaliableServers();
        }

        public void PopulateAvaliableServers()
        {
            try
            {
                var serversContextRemoteFile = zRageMapDownloader.Core.Utils.GetServersContextRemoteFile();

                var client = new WebClient();
                var jsonContent = client.DownloadString(serversContextRemoteFile);
                client.Dispose();

                var serversList = JsonConvert.DeserializeObject<ServersJson>(jsonContent)?.Servers;

                AvaliableServers.Clear();
                foreach (var server in serversList)
                {
                    AvaliableServers.Add(server);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when trying to load the avaliable servers: " + ex.Message);
            }
        }

        public void OpenDownloadWindow()
        {
            var win = new WinDownloadMapsView();
            var vmDownload = win.FindResource(nameof(DownloadMapsViewModel)) as DownloadMapsViewModel;
            vmDownload.BindServerObject(SelectedServer);

            win.ShowDialog();
        }
    }
}
