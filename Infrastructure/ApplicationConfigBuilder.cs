using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationConfigBuilder
    {

        private static AppSettings? _instance;
        public static AppSettings Instance => _instance ?? Create();

        private static AppSettings Create()
        {
            
            var contentRoot = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables()
                .Build();

            return _instance = config.GetSection("appSettings").Get<AppSettings>();
        }
    }
}
