using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Windows.ApplicationModel;
using Windows.Management.Deployment;
using xtrance.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace xtrance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*string configPath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

            if (Properties.Settings.Default.UpdateSettings)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.Reload();
                Properties.Settings.Default.UpdateSettings = false;
                Properties.Settings.Default.Save();
            }*/

            Text = Text + " - " + Application.ProductVersion;

            textBoxUser.Text = Properties.Settings.Default.username;
            textBoxUser.TextChanged += TextBox_TextChanged;
            textBoxPassword.Text = Properties.Settings.Default.password;
            textBoxPassword.TextChanged += TextBox_TextChanged;
            textBoxServer.Text = Properties.Settings.Default.serverid;
            textBoxServer.TextChanged += TextBox_TextChanged;

            textBoxFrom.Text = Properties.Settings.Default.from;
            textBoxFrom.TextChanged += TextBox_TextChanged;

            textBoxTo.Text = Properties.Settings.Default.to;
            textBoxTo.TextChanged += TextBox_TextChanged;

            textBoxDirectory.Text = Properties.Settings.Default.FilesPath;
            textBoxDirectory.TextChanged += TextBox_TextChanged;

            buttonOK.Enabled = true; buttonCancel.Enabled = false;


            new Thread(() =>
            {
                try
                {
                    PackageManager packageManager = new PackageManager();
                    Package currentPackage = packageManager.FindPackageForUser(string.Empty, Package.Current.Id.FullName);

                    while (true)
                    {
                        PackageUpdateAvailabilityResult status = currentPackage.CheckUpdateAvailabilityAsync().GetAwaiter().GetResult();

                        if (status.Availability == PackageUpdateAvailability.Unknown)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(5));
                        }
                        else
                        if (status.Availability == PackageUpdateAvailability.Error)
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                        }
                        else
                        if (status.Availability == PackageUpdateAvailability.NoUpdates)
                        {
                            labelUpdate.Invoke((Action)(() => labelUpdate.Text = status.Availability.ToString()));
                            Thread.Sleep(TimeSpan.FromMinutes(5));
                        } else
                        if (status.Availability == PackageUpdateAvailability.Required || status.Availability == PackageUpdateAvailability.Available)
                        {
                            labelUpdate.Invoke((Action)(() => labelUpdate.Text = status.Availability.ToString()));
                            buttonUpdate.Invoke((Action)(() => buttonUpdate.Visible = true));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    labelUpdate.Invoke((Action)(() => labelUpdate.Text = $"error {ex.Message}"));
                }
            })
            { IsBackground = true }.Start();
        }

        private string RootPath => @"\\?\"+textBoxDirectory.Text+ @"\";

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.username = textBoxUser.Text;
            Properties.Settings.Default.password = textBoxPassword.Text;
            Properties.Settings.Default.serverid = textBoxServer.Text;
            Properties.Settings.Default.from = textBoxFrom.Text;
            Properties.Settings.Default.to = textBoxTo.Text;
            Properties.Settings.Default.FilesPath = textBoxDirectory.Text;
            Properties.Settings.Default.Save();
        }
        private void Log(string str)
        {
            textBoxLog.Invoke((Action)(() => textBoxLog.AppendText(str + "\r\n")));
        }

        private Task _task = null;
        CancellationTokenSource _cancellationToken = new CancellationTokenSource();

        private void buttonOK_Click(object sender, EventArgs e)
        {
            TimeSpan sleeptime = new TimeSpan(0, 0, 0, Int32.Parse(textBoxSleep.Text));
            string user = textBoxUser.Text;
            string pass = textBoxPassword.Text;
            int serverId = Int32.Parse(textBoxServer.Text);
            buttonOK.Enabled = false; buttonCancel.Enabled = true;

            _task = Task.Run(async () =>
            {
                try
                {
                    XtranceAccessor xtrance = new XtranceAccessor(textBoxURL.Text, Log, serverId, _cancellationToken.Token);
                    await xtrance.LogingAndGetCookie(user, pass);
                    MetaBooks metaBooks = new MetaBooks();
                    Books books = await xtrance.GetLatestBooks(Int32.Parse(textBoxFrom.Text), Int32.Parse(textBoxTo.Text));

                    await Parallel.ForEachAsync(books.Values, new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 5
                    }, async (book, ct) =>
                    {
                        _cancellationToken.Token.ThrowIfCancellationRequested();
                        //Thread.Sleep(sleeptime);
                        await xtrance.FillBookInfo(metaBooks, book);
                    });

                    /*await Parallel.ForEachAsync(books.Values, async (book, ct) => {
                        _cancellationToken.Token.ThrowIfCancellationRequested();
                        //Thread.Sleep(sleeptime);
                        await xtrance.FillBookInfo(metaBooks, book);
                    });*/


                    Log("Collected " + metaBooks.Count + " metabooks...");
                    await Parallel.ForEachAsync(metaBooks.Values, new ParallelOptions()
                    {
                        MaxDegreeOfParallelism = 5
                    }, async (metaBook, ct) =>
                    {
                        _cancellationToken.Token.ThrowIfCancellationRequested();
                        //Thread.Sleep(sleeptime);
                        await xtrance.FillBookMeta(metaBook);
                    });

                    /*foreach (MetaBook metaBook in metaBooks.Values)
                                        {
                                            _cancellationToken.Token.ThrowIfCancellationRequested();
                                            Thread.Sleep(sleeptime);
                                            await xtrance.FillBookMeta(metaBook);
                                        }*/


                    foreach (MetaBook metaBook in metaBooks.Values)
                    {
                        SaveMetaBook(metaBook);
                    }

                    foreach (Book book in books.Values)
                    {
                        SaveBook(book);
                    }
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                }
            }, _cancellationToken.Token).ContinueWith(_ => {
                Log("done");
                buttonOK.Invoke((Action)(() => buttonOK.Enabled = true));
                buttonCancel.Invoke((Action)(() => buttonCancel.Enabled = false));
            });
        }

        private void SaveBook(Book book)
        {
            Log("Saving book : " + book.ToString());
            string path = Sanitize(book.Parent.Author) + @"\" + Sanitize(book.Parent.Name);
            Directory.CreateDirectory(RootPath + path);
            Stream stream = File.Create(RootPath + path + @"\" + Sanitize(book.Revision) + " - " + Sanitize(book.Format) + ".url");
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine("[DEFAULT]");
            sw.WriteLine("[InternetShortcut]");
            foreach (string downloadLink in book.DownloadLinks)
            {
                sw.WriteLine("URL=" + downloadLink);
            }
            sw.Close();
            stream.Close();
        }

        private void SaveMetaBook(MetaBook metaBook)
        {
            Log("Saving metabook : " + metaBook.ToString());
            string path = Sanitize(metaBook.Author) + @"\" + Sanitize(metaBook.Name);
            
            Directory.CreateDirectory(RootPath + path);
            if (metaBook.Data != null)
            {
                Stream stream = File.Create(RootPath + path + @"\" + "metadata.zip");
                stream.Write(metaBook.Data, 0, metaBook.Data.Length);
                stream.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cancellationToken.Cancel();
        }

        private void labelFrom_Click(object sender, EventArgs e)
        {

        }

        private string Sanitize(string input)
        {
            string s = String.Join("_", input.Split(Path.GetInvalidFileNameChars())).TrimEnd('.').Trim();
            if (s.Length > 250)
            {
                s = s.Substring(0, 250);
            }
            return s;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            buttonOK.Enabled = false;
            buttonCancel.Enabled = false;
            labelUpdate.Text = $"Updating... Application will automatically restart to the new version.";

            new Thread(() =>
            {
                try
                {
                    RelaunchHelper.RegisterApplicationRestart(null, RelaunchHelper.RestartFlags.NONE);

                    PackageManager pm = new PackageManager();
                    Package currentPackage = pm.FindPackageForUser(string.Empty, Package.Current.Id.FullName);

                    var installTask = pm.AddPackageByAppInstallerFileAsync(
                        currentPackage.GetAppInstallerInfo().Uri,
                        AddPackageByAppInstallerOptions.ForceTargetAppShutdown,
                        pm.GetDefaultPackageVolume());

                    installTask.Progress = (installResult, progress) => labelUpdate.BeginInvoke(() =>
                    {
                        labelUpdate.Invoke((Action)(() => labelUpdate.Text = $"Progress: {progress.percentage} {progress.state}"));
                    });

                    var res = installTask.GetAwaiter().GetResult();

                    if (res.IsRegistered == true)
                    {
                        labelUpdate.Invoke((Action)(() => labelUpdate.Text = "Status ok. Application should automatically restart..."));
                    }
                    else
                    {
                        labelUpdate.Invoke((Action)(() => labelUpdate.Text = $"Error {res.ErrorText}"));
                    }
                }
                catch (Exception ex)
                {
                    labelUpdate.Invoke((Action)(() => labelUpdate.Text = $"Error {ex.Message}"));
                }
            })
            { IsBackground = true }.Start();

        }

        private void buttonDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.InitialDirectory = textBoxDirectory.Text;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxDirectory.Text = dlg.SelectedPath;
            }
        }
    }
}
