﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xtrance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
#if DEBUG
            textBoxUser.Text = "hujerhoe";
            textBoxPassword.Text = "jio23jeo23jej";
#endif
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
                    foreach (Book book in books.Values)
                    {
                        _cancellationToken.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(sleeptime);
                        await xtrance.FillBookInfo(metaBooks, book);
                    }
                    Log("Collected " + metaBooks.Count + " metabooks...");
                    foreach (MetaBook metaBook in metaBooks.Values)
                    {
                        _cancellationToken.Token.ThrowIfCancellationRequested();
                        Thread.Sleep(sleeptime);
                        await xtrance.FillBookMeta(metaBook);
                    }

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

    }
}