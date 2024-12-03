namespace AspCookiesSessionsApp
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"Name = {Name}, Price = {Price}, Count = {Count}";
        }
    }
}
