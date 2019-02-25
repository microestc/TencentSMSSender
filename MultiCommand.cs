using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TencentSMSSender
{
    public class MultiCommand
    {
        public MultiCommand()
        {
            Tel = new List<Destination>();
            Params = Array.Empty<string>();
        }

        public MultiCommand(string ext, string extend, string sig, string sign, int tplId, List<Destination> tels, long time, string[] @params)
        {
            Ext = ext;
            Extend = extend;
            Sig = sig;
            Sign = sign;
            Time = time;
            Tpl_Id = tplId;
            Tel = tels;
            Params = @params;
        }

        [JsonProperty("ext")]
        public string Ext { get; set; }

        [JsonProperty("extend")]
        public string Extend { get; set; }

        [JsonProperty("params")]
        public string[] Params { get; set; }

        [JsonProperty("sig")]
        public string Sig { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("tel")]
        public List<Destination> Tel { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("tpl_id")]
        public int Tpl_Id { get; set; }

        
    }
}
