using Newtonsoft.Json;

namespace TencentSMSSender
{
    public class MsgCommand
    {
        public MsgCommand()
        {
            Type = TencentSMSType.Normal;
            Tel = new Destination();
        }

        public MsgCommand(string ext, string extend, string sig, string nationCode, string destination, long time, string message, bool normal = true)
        {
            Type = normal ? TencentSMSType.Normal : TencentSMSType.Marketing;
            Ext = ext;
            Extend = extend;
            Sig = sig;
            Time = time;
            Tel = new Destination(nationCode, destination);
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
        public Destination Tel { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

    }
}
