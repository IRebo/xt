using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace xtrance
{
    public class XtranceAccessor
    {
        private readonly string _urlbase;
        private Log.LogFunction _logFunction;
        private WebClientEx _webClient;
        private int _serverId;

        public XtranceAccessor(string urlbase, Log.LogFunction logFunction, int serverId, CancellationToken token)
        {
            _webClient = new WebClientEx(logFunction, Encoding.UTF8, 15*1000, token);
            _logFunction = logFunction;
            _urlbase = urlbase;
            _serverId = serverId;
        }

        public async Task LogingAndGetCookie(string username, string password)
        {
            _logFunction("starting...");

            _webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.76 Safari/537.36");

            string firstUrl = _urlbase + "/new/?";
            string resp = await _webClient.DownloadStringTaskAsync(firstUrl);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(resp);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode(@"//form/@action");
            string newUrl = node.Attributes["action"].Value;
            _logFunction("form post url : " + newUrl);

            /*HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("/head/meta[@http-equiv=\"set-cookie\"]/@content");
            string[] set_cookie = node.Attributes["content"].Value.Split('=');
            _webClient.Cookies.Add(new Uri(_urlbase), new Cookie(set_cookie[0], set_cookie[1]));
            _logFunction("logged in and got login cookie : " + set_cookie[1]);
            */
            Dictionary<string, string> outgoingQueryString = new Dictionary<string, string>();// HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("sendform", "1");
            outgoingQueryString.Add("login_name", username);
            outgoingQueryString.Add("login_password", password);

            //_webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            //_webClient.Headers.Add("Accept-Encoding", "gzip, deflate");
            //_webClient.Headers.Add("Accept-Language", "en-US,en;q=0.8,sk;q=0.6,cs;q=0.4");
            //_webClient.Headers.Add("Cache-Control", "max-age=0");
            //_webClient.Headers.Add("Host", "xtrance.info");
            //_webClient.Headers.Add("Referer", firstUrl);
            ///_webClient.Headers.Add("Origin", "http://xtrance.info");
            //_webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            //_webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.76 Safari/537.36");
            _logFunction("Logging in...");
            var bytes = await _webClient.UploadValuesTaskAsync(_urlbase + newUrl, outgoingQueryString);
            resp = Encoding.Default.GetString(bytes);

            //_webClient.Headers.Remove("Origin");
            //_webClient.Headers.Remove("Content-Type");
            //_webClient.Headers.Add("Referer", firstUrl);
            //_webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.76 Safari/537.36");
            //_webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

            resp = await _webClient.DownloadStringTaskAsync(firstUrl);
            if (resp.Contains("login_password"))
            {
                throw new Exception("Login error...");
            }
            _logFunction("hopefully logged in...");

        }

        public async Task<Books> GetLatestBooks(int from, int to)
        {
            Books bookDict = new Books();
            string result = await _webClient.DownloadStringTaskAsync(_urlbase + "/new/?mainpage=nov&nov_view=2");

            int bookIndex = 0;

            for (int page = from; page <= to; page++)
            {
                result = await _webClient.DownloadStringTaskAsync(_urlbase + "/new/?mainpage=nov&subpage=&id=0&nov_ebk_page=" + page);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(result);
                HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//*[@class=\"list_td\"]/@href");
                foreach (HtmlNode htmlNode in nodes)
                {
                    string href = htmlNode.Attributes["href"].Value;
                    Dictionary<string, string> dict = href.Split('?')[1]
                        .Split('&')
                        .Select(x => x.Split('='))
                        .ToDictionary(y => y[0], y => y[1]);

                    string id = dict["id"];

                    if (!bookDict.ContainsKey(id))
                    {
                        bookIndex++;
                        bookDict.Add(id, new Book() { Id = id, Index = bookIndex });
                    }
                }
            }
            _logFunction("found " + bookDict.Count + " books...");
            return bookDict;
        }

        public async Task FillBookInfo(MetaBooks metaBooks, Book book)
        {
            string result = await _webClient.DownloadStringTaskAsync(_urlbase + "/new/?mainpage=ebk&subpage=detail&id=" + book.Id);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(result);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//*[@class=\"detail_a_data\"]/@href");
            string href = node.Attributes["href"].Value;
            Dictionary<string, string> dict = href.Split('?')[1]
          .Split('&')
          .Select(x => x.Split('='))
          .ToDictionary(y => y[0], y => y[1]);
            string masterId = dict["id"];

            node = htmlDoc.DocumentNode.SelectSingleNode("//*[text()[contains(.,'Autor:')]]");
            node = node.NextSibling;
            string author = HttpUtility.HtmlDecode(node.InnerText);

            node = htmlDoc.DocumentNode.SelectSingleNode("//*[text()[contains(.,'Publikace:')]]");
            node = node.NextSibling;
            string bookname = HttpUtility.HtmlDecode(node.InnerText);

            metaBooks.FillBook(masterId, book, author, bookname);

            node = htmlDoc.DocumentNode.SelectSingleNode("//*[text()[contains(.,'Stav:')]]");
            node = node.NextSibling;
            book.Revision = HttpUtility.HtmlDecode(node.InnerText);

            node = htmlDoc.DocumentNode.SelectSingleNode("//*[text()[contains(.,'Formát ebooku:')]]");
            node = node.NextSibling;
            book.Format = HttpUtility.HtmlDecode(node.InnerText.Split(' ')[0]);

            book.DownloadLinks = GetBookLinks(book);

            _logFunction("Found book " + book.ToString());
        }

        public string[] GetBookLinks(Book book)
        {
            List<string> links = new List<string>();
            links.Add(_urlbase + "/new/?mainpage=link&subpage=ebook&server=" + _serverId + "&id=" + book.Id);
            return links.ToArray();
        }

        public async Task FillBookMeta(MetaBook metaBook)
        {
            try
            {
                byte[] bytes = await _webClient.DownloadDataTaskAsync(_urlbase + "/new/?calibre_opf_pub=" + metaBook.Id + "&image=1");
                _logFunction(bytes.Length + " bytes loaded for metadata of book " + metaBook.Id + " : " + metaBook.Author + " - " + metaBook.Name);
                metaBook.Data = bytes;
            } catch (WebException we)
            {
                metaBook.Data = null;
                _logFunction($"Error getting metadata for book {metaBook.Id} with error {we.Message}");
            }
        }
    }
}