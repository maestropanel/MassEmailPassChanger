namespace MassEmailPassChanger
{
    using MassEmailPassChanger.Properties;
    using MpMigrate.MaestroPanel.Api;
    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {            
            if(!File.Exists(Settings.Default.AutTabPath))
                throw new FileNotFoundException(Settings.Default.AutTabPath);

            var authTab = File.ReadAllLines(Settings.Default.AutTabPath);

            ApiClient _client = new ApiClient(Settings.Default.ApiKey, Settings.Default.ApiHost, format:"XML");
            string account, password, domain = String.Empty;
            
            foreach (var item in authTab)
            {
                var tabline = item.Split('\t');

                account = tabline[0].Split('@').FirstOrDefault();
                password = tabline[2];
                domain = tabline[3];

                var result = _client.ChangeEmailPassword(domain, account, password);
                Console.WriteLine("{0}@{1}\t{2} (Code:{3})", account, domain, result.Message, result.ErrorCode );                
            }
        }
    }
}
