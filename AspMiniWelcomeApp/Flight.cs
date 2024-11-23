namespace AspMiniWelcomeApp
{
    public class Flight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, From City: {FromCity}, To City: {ToCity}, Date: {Date.ToLongDateString()}, Time: {Time.ToShortTimeString}";
        }
    }
}
