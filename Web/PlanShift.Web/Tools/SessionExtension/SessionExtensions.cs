namespace PlanShift.Web.Tools.SessionExtension
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public static class SessionExtensions
    {
        public static async Task SetStringAsync(this ISession session, string key, string value)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }

            session.SetString(key, value);
        }

        public static async Task<string> GetStringAsync(this ISession session, string key)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }

            var value = session.GetString(key);
            return value;
        }

        public static async Task<bool> HasKeyAsync(this ISession session, string key)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }

            return session.TryGetValue(key, out _);
        }
    }
}
