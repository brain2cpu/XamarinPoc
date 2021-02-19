namespace XamarinPoc.Models
{
    class Pizza
    {
        public string Name { get; set; }
        
        public string ImageUri { get; set; }

        public override string ToString() => Name;
    }
}
