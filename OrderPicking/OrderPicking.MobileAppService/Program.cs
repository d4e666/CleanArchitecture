#region Usings

using System.IO;
using Microsoft.AspNetCore.Hosting;

#endregion

namespace OrderPicking.MobileAppService
{
    public class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                       .UseKestrel()
                       .UseContentRoot(Directory.GetCurrentDirectory())
                       .UseIISIntegration()
                       .UseStartup<Startup>()
                       .Build();

            host.Run();
        }

        #endregion
    }
}