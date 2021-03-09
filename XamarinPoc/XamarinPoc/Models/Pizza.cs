namespace XamarinPoc.Models
{
    public class Pizza
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUri { get; set; }

        public override string ToString() => Name;
    }
}