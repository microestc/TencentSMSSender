# TencentSMSSender
腾讯云 短信API FOR NetStandard 2.0

1. Install Package
Package Manager
Install-Package Microestc.TencentSMSSender -Version 1.0.0
.NET CLI
dotnet add package Microestc.TencentSMSSender --version 1.0.0
Paket CLI
paket add Microestc.TencentSMSSender --version 1.0.0

2. In Startup class

public void ConfigureServices(IServiceCollection services)
{
            services.AddTencentSMSSenderServices(options =>
            {
                options.AppUrl = Configuration.GetSection("TencentSMSSettings").GetValue<string>("AppUrl");
                options.AppId = Configuration.GetSection("TencentSMSSettings").GetValue<string>("AppId");
                options.AppKey = Configuration.GetSection("TencentSMSSettings").GetValue<string>("AppKey");
                options.Sign = Configuration.GetSection("TencentSMSSettings").GetValue<string>("Sign");
                options.Extend = Configuration.GetSection("TencentSMSSettings").GetValue<string>("Extend");
            });
}
  
3. Use ITencentSMSSender

public class HomeController : Controller
{
        private readonly ITencentSMSSender _smsSender;

        public HomeController(ITencentSMSSender smsSender)
        {
            _smsSender = smsSender;
        }

        public async Task<IActionResult> Index()
        {
        
            await _smsSender.SendAsync();
            await _smsSender.MultiSendAsync(28***7, new List<Destination>(){ new Destination("86", "173*****539"), new     Destination("86", "177****22") }, "", "267673");
            return View();
        }
}

public interface ITencentSMSSender
{
        Task MultiSendAsync(int tplId, List<Destination> destinations, string ext, params string[] @params);
            
        Task MultiSendAsync(List<Destination> destinations, string message, string ext = "", bool normal = true);
        
        Task SendAsync(int tplId, string nationCode, string destination, string ext, params string[] @params);
        
        Task SendAsync(string nationCode, string destination, string message, string ext = "", bool normal = true);
}


Ok,are you enable to use. contact me if you have a problem.
