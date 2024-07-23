namespace xtrance
{
    public class Book
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public MetaBook Parent;

        public string Revision { get; set; }
        public string[] DownloadLinks { get; set; }
        public string Format { get; set; }

        public override string ToString()
        {
            return "(" + Index + " - " + Id + ") " + Parent.ToString() + " " + Revision + " " + Format;
        }
    }
}