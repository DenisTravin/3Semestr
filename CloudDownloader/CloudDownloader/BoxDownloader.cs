using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box.V2;
using Box.V2.Config;
using Box.V2.Auth;
using Box.V2.Models;
using System.Collections.Generic;

namespace BoxDownloader
{
    public partial class DownloadProject : Form
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        public const string clientId = "4yy8sir940cdloyadd84ct8ne6aazgj3";
        public const string clientSecret = "ytH0lH2YDag79Ra90DBplykd7tTY2Kou";
        public Uri redirect_uri = new Uri("https://localhost/");
        List<BoxItemWithPath> filesList = new List<BoxItemWithPath>();
        private string authToken = "";
        private string URL = "";
        BoxClient boxClient;
        ProgressBar progressBar = new ProgressBar();
        Button download = new Button();
        Button browse = new Button();
        Button cancel = new Button();
        TextBox directory = new TextBox();
        TextBox directoryInfo = new TextBox();
        TextBox wrongRepository = new TextBox();
        TextBox endOfDownload = new TextBox();
        TextBox cancelComplited = new TextBox();
        ListView detailedInfoView = new ListView();
        ListView resultsView = new ListView();
        ColumnHeader field = new ColumnHeader();
        ColumnHeader value = new ColumnHeader();
        ColumnHeader fileName = new ColumnHeader();
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public DownloadProject()
        {
            InitializeComponent();
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Uri oneUrl = new Uri("https://account.box.com/api/oauth2/authorize?response_type=code&client_id=" + clientId + "&state=security_token");
            browser.Navigate(oneUrl, "Content-Type: application/x-www-form-urlencoded");
            //browser.Navigate(oneUrl);
            timer.Interval = 5000;
            ProgressBar();
            DownloadButton();
            DirectoryBox();
            BrowseButton();
            DetailedInfoView();
            browser.ProgressChanged += SignIn;
            download.Click += Download_Click;
            browse.Click += Browse_Click;
            cancel.Click += Cancel_Click;
            timer.Tick += Timer_Tick;
            resultsView.ItemActivate += TakeInfo;
        }

        private class BoxItemWithPath
        {
            public BoxItem boxItem { get; set; }
            public string extraPath { get; set; }

            public BoxItemWithPath(BoxItem boxItem, string extraPath)
            {
                this.boxItem = boxItem;
                this.extraPath = extraPath;
            }
        }

        private static string POST(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
            req.ContentType = "text/html; charset=UTF-8";
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = System.Text.Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, System.Text.Encoding.UTF8);
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }

        private void SignIn(object sender, EventArgs e)
        {
            if (browser.Url != null)
            {
                URL = browser.Url.ToString();
                if ((URL != "https://account.box.com/api/oauth2/authorize?response_type=code&client_id=" + clientId + "&state=security_token") && (URL.Contains("code")))
                {
                    if (authToken == "")
                    {
                        var config = new BoxConfig(clientId, clientSecret, redirect_uri);
                        string authCode = "";
                        int fragment = URL.IndexOf("code");
                        int i = 0;
                        int stringSize = URL.Length;
                        while (fragment + 5 + i < stringSize)
                        {
                            authCode += URL[fragment + 5 + i];
                            i++;
                        }
                        string postUrl = "https://api.box.com/oauth2/token";
                        string postData = "grant_type=authorization_code&code=" + authCode + "&client_id=" + clientId + "&client_secret=" + clientSecret;
                        string respons = POST(postUrl, postData);
                        i = 0;
                        fragment = respons.IndexOf("access_token");
                        while (respons[fragment + 15 + i] != '"')
                        {
                            authToken += respons[fragment + 15 + i];
                            i++;
                        }
                    }
                    DownloadForm();
                }
            }
        }

        private async Task GetFilesList(BoxClient boxClient, string folder, string extraPath)
        {
            CancellationToken cancelToken = cancelTokenSource.Token;
            filesList.Clear();
            var items = await boxClient.FoldersManager.GetFolderItemsAsync(folder, 500);
            foreach (var item in items.Entries)
            {
                cancelToken.ThrowIfCancellationRequested();
                if (item.Type == "folder")
                {
                    await GetFilesList(boxClient, item.Id, extraPath + '\\' + item.Name);
                }
                else
                {
                    var tempItem = await boxClient.FilesManager.GetInformationAsync(item.Id);
                    var tempPathItem = new BoxItemWithPath(tempItem, extraPath);
                    resultsView.Items.Add(tempPathItem.boxItem.Name);
                    filesList.Add(tempPathItem);
                }
            }
            progressBar.Maximum = filesList.Count;
        }

        private async Task Downloader(BoxClient boxClient, string newPath, string folder)
        {
            CancellationToken cancelToken = cancelTokenSource.Token;
            await GetFilesList(boxClient, "0", "");
            progressBar.Visible = true;
            foreach (var item in filesList)
            {
                cancelToken.ThrowIfCancellationRequested();
                string path = newPath;
                if (item.extraPath != "")
                {
                    path += item.extraPath;
                    Directory.CreateDirectory(path);
                }
                path += '\\' + item.boxItem.Name;
                using (var fileStream = System.IO.File.Create(path))
                {
                    (await boxClient.FilesManager.DownloadStreamAsync(item.boxItem.Id)).CopyTo(fileStream);
                }
                progressBar.PerformStep();
            }
            progressBar.Visible = false;
            progressBar.Value = 0;
        }

        private void TakeInfo(object sender, EventArgs e)
        {
            detailedInfoView.Items.Clear();
            foreach (var item in filesList)
            {
                if (item.boxItem.Name == resultsView.FocusedItem.Text)
                {
                    ListViewItem modifedAt = new ListViewItem(new string[] { "ModifedAt", item.boxItem.ModifiedAt.ToString() });
                    ListViewItem createdAt = new ListViewItem(new string[] { "CreatedAt", item.boxItem.CreatedAt.ToString() });
                    ListViewItem id = new ListViewItem(new string[] { "Id", item.boxItem.Id });
                    ListViewItem des = new ListViewItem(new string[] { "Description", item.boxItem.Description });
                    ListViewItem size = new ListViewItem(new string[] { "Size", item.boxItem.Size.ToString() + " Bytes" });
                    detailedInfoView.Items.Add(modifedAt);
                    detailedInfoView.Items.Add(createdAt);
                    detailedInfoView.Items.Add(id);
                    detailedInfoView.Items.Add(des);
                    detailedInfoView.Items.Add(size);
                }
            }
        }

        private async void Download_Click(object sender, EventArgs e)
        {
            resultsView.Items.Clear();
            detailedInfoView.Items.Clear();
            wrongRepository.Visible = false;
            if (Directory.Exists(directory.Text))
            {
                var config = new BoxConfig(clientId, clientSecret, redirect_uri);
                var session = new OAuthSession(authToken, "NOT_NEEDED", 3600, "bearer");
                boxClient = new BoxClient(config, session);
                cancel.Visible = true;
                directory.Enabled = false;
                browse.Enabled = false;
                download.Enabled = false;
                try
                {
                    await Downloader(boxClient, directory.Text, "0");
                    cancel.Enabled = false;
                    timer.Start();
                    endOfDownload.Visible = true;
                    cancel.Visible = false;
                }
                catch (OperationCanceledException)
                {
                    timer.Start();
                    cancelComplited.Visible = true;
                    cancel.Visible = false;
                    cancelTokenSource = new CancellationTokenSource();
                }
            }
            else
            {
                directory.Text = "";
                wrongRepository.Visible = true;
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.ShowDialog();
                directory.Text = folderDialog.SelectedPath;
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            progressBar.Visible = false;
            resultsView.Items.Clear();
            detailedInfoView.Items.Clear();
            cancelTokenSource.Cancel();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            cancelComplited.Visible = false;
            endOfDownload.Visible = false;
            browse.Enabled = true;
            directory.Enabled = true;
            download.Enabled = true;
            cancel.Enabled = true;
        }

        private void ProgressBar()
        {
            progressBar.Visible = false;
            progressBar.Location = new Point(410, 100);
            progressBar.Size = new Size(300, 50);
            progressBar.Step = 1;
            progressBar.Minimum = 0;

            Controls.Add(progressBar);
        }

        private void BrowseButton()
        {
            browse.Visible = false;
            browse.Text = "Browse";
            browse.Location = new Point(310, 69);
            browse.Size = new Size(90, 22);

            Controls.Add(browse);
        }

        private void DirectoryBox()
        {
            directory.Visible = false;
            directory.Location = new Point(100, 70);
            directory.Size = new Size(200, 10);

            directoryInfo.Visible = false;
            directoryInfo.Enabled = false;
            directoryInfo.Size = new Size(140, 10);
            directoryInfo.BorderStyle = BorderStyle.None;
            directoryInfo.Text = "Directory to download files:";
            directoryInfo.Location = new Point(100, 50);

            Controls.Add(directory);
            Controls.Add(directoryInfo);
        }

        private void DownloadButton()
        {
            download.Visible = false;
            download.Text = "Download files from Box repository";
            download.Location = new Point(100, 100);
            download.Size = new Size(300, 50);

            endOfDownload.Enabled = false;
            endOfDownload.Visible = false;
            endOfDownload.Text = "Successfull download";
            endOfDownload.Location = new Point(190, 170);
            endOfDownload.BorderStyle = BorderStyle.None;
            endOfDownload.Size = new Size(150, 22);

            wrongRepository.Visible = false;
            wrongRepository.Enabled = false;
            wrongRepository.BorderStyle = BorderStyle.None;
            wrongRepository.Text = "Wrong Repository";
            wrongRepository.Location = new Point(410, 70);
            wrongRepository.Size = new Size(100, 10);

            cancel.Visible = false;
            cancel.Text = "Cancel";
            cancel.Location = new Point(410, 69);
            cancel.Size = new Size(100, 22);

            cancelComplited.Visible = false;
            cancelComplited.Text = "Cancel was successfull";
            cancelComplited.Location = new Point(190, 170);
            cancelComplited.Size = new Size(150, 22);
            cancelComplited.BorderStyle = BorderStyle.None;
            cancelComplited.Enabled = false;

            Controls.Add(download);
            Controls.Add(wrongRepository);
            Controls.Add(cancel);
            Controls.Add(cancelComplited);
            Controls.Add(endOfDownload);
        }

        private void DetailedInfoView()
        {
            field.Text = "Field";
            field.Width = 200;

            value.Text = "Value";
            value.Width = 300;

            fileName.Text = "File name (double click to see information)";
            fileName.Width = 250;

            detailedInfoView.Location = new Point(280, 200);
            detailedInfoView.Size = new Size(490, 300);
            detailedInfoView.Columns.AddRange(new ColumnHeader[] {
            field,
            value});
            detailedInfoView.GridLines = true;
            detailedInfoView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            detailedInfoView.LabelEdit = true;
            detailedInfoView.Name = "detailedInfoView";
            detailedInfoView.TabIndex = 0;
            detailedInfoView.UseCompatibleStateImageBehavior = false;
            detailedInfoView.View = View.Details;
            detailedInfoView.Visible = false;

            resultsView.Location = new Point(20, 200);
            resultsView.Name = "resultsView";
            resultsView.Size = new Size(250, 300);
            resultsView.TabIndex = 0;
            resultsView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            resultsView.Columns.Add(fileName);
            resultsView.View = View.Details;
            resultsView.GridLines = true;
            resultsView.Visible = false;

            Controls.Add(detailedInfoView);
            Controls.Add(resultsView);
        }

        private void DownloadForm()
        {
            browser.Stop();
            browser.Hide();
            detailedInfoView.Visible = true;
            resultsView.Visible = true;
            directoryInfo.Visible = true;
            directory.Visible = true;
            browse.Visible = true;
            download.Visible = true;
        }

    }
}