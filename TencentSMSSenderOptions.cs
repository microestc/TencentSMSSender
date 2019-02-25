using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace TencentSMSSender
{
    public class TencentSMSSenderOptions
    {
        public string AppUrl { get; set; }

        public string AppId { get; set; }

        public string AppKey { get; set; }

        public string Sign { get; set; }

        public string Extend { get; set; }

        public static string Sha256(string @string)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(@string);
            byte[] hash = SHA256.Create().ComputeHash(bytes);
            var builder = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString().ToLower();
        }

        public string GetUrl(int random) => $"{AppUrl}?sdkappid={AppId}&random={random}";

        public int Random => new Random(100000).Next(999999);

        public long TimeStamp => (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        public StringContent HttpContent(string ext, int tplId, string nationCode, string destination, int random, long timeStamp, string[] @params)
        {
            var @string = new StringBuilder("appkey=").Append(AppKey).Append("&random=").Append(random).Append("&time=").Append(timeStamp).Append("&mobile=").Append(destination).ToString();
            var sig = Sha256(@string);
            var command = new Command(ext: ext, extend: Extend, sig: sig, sign: Sign, tplId: tplId, nationCode: nationCode, destination: destination, time: timeStamp, @params: @params);
            var content = JsonConvert.SerializeObject(command, Formatting.Indented);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public StringContent HttpContent(string ext, string nationCode, string destination, int random, long timeStamp, string message, bool normal = true)
        {
            var @string = new StringBuilder("appkey=").Append(AppKey).Append("&random=").Append(random).Append("&time=").Append(timeStamp).Append("&mobile=").Append(destination).ToString();
            var sig = Sha256(@string);
            var command = new MsgCommand(ext: ext, extend: Extend, sig: sig, nationCode: nationCode, destination: destination, time: timeStamp, message: message, normal: normal);
            var content = JsonConvert.SerializeObject(command, Formatting.Indented);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public StringContent HttpMultiContent(string ext, int tplId, List<Destination> destinations, int random, long timeStamp, string[] @params)
        {
            var destination = string.Join(",", destinations.Select(m => m.Mobile));
            var @string = new StringBuilder("appkey=").Append(AppKey).Append("&random=").Append(random).Append("&time=").Append(timeStamp).Append("&mobile=").Append(destination).ToString();
            var sig = Sha256(@string);
            var command = new MultiCommand(ext: ext, extend: Extend, sig: sig, sign: Sign, tplId: tplId, tels: destinations, time: timeStamp, @params: @params);
            var content = JsonConvert.SerializeObject(command, Formatting.Indented);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        public StringContent HttpMultiContent(string ext, List<Destination> destinations, int random, long timeStamp, string message, bool normal = true)
        {
            var destination = string.Join(",", destinations.Select(m => m.Mobile));
            var @string = new StringBuilder("appkey=").Append(AppKey).Append("&random=").Append(random).Append("&time=").Append(timeStamp).Append("&mobile=").Append(destination).ToString();
            var sig = Sha256(@string);
            var command = new MultiMsgCommand(ext: ext, extend: Extend, sig: sig, tels: destinations, time: timeStamp, message: message, normal: normal);
            var content = JsonConvert.SerializeObject(command, Formatting.Indented);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
