namespace xtrance
{
    public class MetaBook
    {
        public string Id { get; set; }
        public byte[] Data { get; set; }

        public string Author { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return "(" + Id + ")" + Author + " - " + Name;
        }

    }
}