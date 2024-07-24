using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel;
using Windows.Management.Deployment;
using xtrance.Properties;

namespace xtrance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            new Thread(() =>
            {
                try
                {
                    PackageManager packageManager = new PackageManager();
                    Package currentPackage = packageManager.FindPackageForUser(string.Empty, Package.Current.Id.FullName);

                    PackageUpdateAvailabilityResult status = currentPackage.CheckUpdateAvailabilityAsync().GetResults();
                    labelUpdate.Invoke((Action)(() => labelUpdate.Text = status.ToString()));
                    if (status.Availability == PackageUpdateAvailability.Required || status.Availability == PackageUpdateAvailability.Available)
                    {
                        buttonUpdate.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    labelUpdate.Invoke((Action)(() => labelUpdate.Text = $"error {ex}"));
                }
            })
            { IsBackground = true }.Start();

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.username = textBoxUser.Text;
            Properties.Settings.Default.password = textBoxPassword.Text;
            Properties.Settings.Default.serverid = textBoxServer.Text;
            Properties.Settings.Default.from = textBoxFrom.Text;
            Properties.Settings.Default.to = textBoxTo.Text;
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
                    Log(ex.ToString());
                }
            }, _cancellationToken.Token).ContinueWith(_ => Log("done"));
        }

        private void SaveBook(Book book)
        {
            Log("Saving book : " + book.ToString());
            string path = Sanitize(book.Parent.Author) + @"\" + Sanitize(book.Parent.Name);
            Directory.CreateDirectory(path);
            Stream stream = File.Create(path + @"\" + Sanitize(book.Revision) + " - " + Sanitize(book.Format) + ".url");
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
            Directory.CreateDirectory(path);
            if (metaBook.Data != null)
            {
                Stream stream = File.Create(path + @"\" + "metadata.zip");
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
            return String.Join("_", input.Split(Path.GetInvalidFileNameChars())).TrimEnd('.').Trim();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            PackageManager pm = new PackageManager();
            Package currentPackage = pm.FindPackageForUser(string.Empty, Package.Current.Id.FullName);

            var installTask = pm.AddPackageByAppInstallerFileAsync(
                currentPackage.GetAppInstallerInfo().Uri,
                AddPackageByAppInstallerOptions.ForceTargetAppShutdown,
                pm.GetDefaultPackageVolume());

            installTask.Progress = (installResult, progress) => labelUpdate.BeginInvoke(() =>
            {
                labelUpdate.Text = $"Progress: {progress.percentage} {progress.state}";
            });

            var res = installTask.GetResults();

            if (res.IsRegistered == true)
            {
                uint res2 = RelaunchHelper.RegisterApplicationRestart(null, RelaunchHelper.RestartFlags.NONE);
                labelUpdate.Text = "Please close the application";
            } else
            {
                labelUpdate.Text = $"Error {res.ErrorText}";
            }
            
        }
    }
}
