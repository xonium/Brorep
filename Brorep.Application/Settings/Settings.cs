namespace Brorep.Application.Settings
{
    public interface ISettings
    {
        string Secret { get; set; }
    }

    public class Settings : ISettings
    {
        public Settings(string secret)
        {
            Secret = secret;
        }

        public string Secret { get; set; }
    }
}
