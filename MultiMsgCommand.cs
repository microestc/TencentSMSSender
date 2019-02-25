using System.Collections.Generic;
using Newtonsoft.Json;

namespace TencentSMSSender
{
    public class MultiMsgCommand
    {
        public MultiMsgCommand()
        {
            Type = TencentSMSType.Normal;
            Tel = new List<Destination>();
        }

        public MultiMsgCommand(string ext, string extend, string sig, List<Destination> tels, long time, string message, bool normal = true)
        {
            Type = normal ? TencentSMSType.Normal : TencentSMSType.Marketing;
            Ext = ext;
            Extend = extend;
            Sig = sig;
            Time = time;
            Tel = tels;
            Msg = message;
        }

        [JsonProperty("type")]
        public TencentSMSType Type { get; set; }

        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("extend")]
        public string Extend { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("sig")]
        public string Sig { get; set; }

        [JsonProperty("tel")]
        public List<Destination> Tel { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

    }
}
