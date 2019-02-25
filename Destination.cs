using Newtonsoft.Json;

namespace TencentSMSSender
{
    public class Destination
    {
        public Destination() { }

        public Destination(string nationCode, string mobile)
        {
            NationCode = nationCode;
            Mobile = mobile;
        }

        [JsonProperty("nationcode")]
        public string NationCode { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
}
