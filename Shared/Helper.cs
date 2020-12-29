using System.Linq;

namespace BlogEngineApi.Shared
{
    public class Helper
    {
        public static void ConsoleLog(string message)
        {
            System.Console.WriteLine(message);
        }

        public static string getUserName(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var identity = context.User.Identities.FirstOrDefault();
            return identity.Name;
        }
    }
}
