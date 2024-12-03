namespace AspConfigurationApp
{
    public class UserConfig
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public override string ToString()
        {
            return $"Config info. Login: {Login}, Password: {Password}";
        }
    }
}
