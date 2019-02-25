using System.Collections.Generic;
using System.Threading.Tasks;

namespace TencentSMSSender
{
    public interface ITencentSMSSender
    {
        Task MultiSendAsync(int tplId, List<Destination> destinations, string ext, params string[] @params);
        Task MultiSendAsync(List<Destination> destinations, string message, string ext = "", bool normal = true);
        Task SendAsync(int tplId, string nationCode, string destination, string ext, params string[] @params);
        Task SendAsync(string nationCode, string destination, string message, string ext = "", bool normal = true);
    }
}