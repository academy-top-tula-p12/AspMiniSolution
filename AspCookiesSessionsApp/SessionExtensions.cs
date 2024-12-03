using System.Text.Json;

namespace AspCookiesSessionsApp
{
    public static class SessionExtensions
    {
        public static void SetData<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize<T>(value));
        }

        public static T? GetData<T>(this ISession session, string key)
        {
            var valueString = session.GetString(key);
            if (valueString is not null)
                return JsonSerializer.Deserialize<T>(valueString);
            else
                return default(T);
        }
    }
}
