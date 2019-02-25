using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace TencentSMSSender
{
    public class TencentSMSSender : ITencentSMSSender
    {
        private readonly HttpClient _httpClient;
        private readonly TencentSMSSenderOptions _options;

        public TencentSMSSender(HttpClient httpClient, IOptions<TencentSMSSenderOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task SendAsync(int tplId, string nationCode, string destination, string ext, params string[] @params)
        {
            var random = _options.Random;
            var timeStamp = _options.TimeStamp;
            var httpContent = _options.HttpContent(ext: ext, tplId: tplId, nationCode: nationCode, destination: destination, random: random, timeStamp: timeStamp, @params: @params);

            var response = await _httpClient.PostAsync(_options.GetUrl(random), httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("post method send sms failed, try later.");
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task SendAsync(string nationCode, string destination, string message, string ext = "", bool normal = true)
        {
            var random = _options.Random;
            var timeStamp = _options.TimeStamp;
            var httpContent = _options.HttpContent(ext: ext, nationCode: nationCode, destination: destination, random: random, timeStamp: timeStamp, message: message, normal: normal);

            var response = await _httpClient.PostAsync(_options.GetUrl(random), httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("post method send sms failed, try later.");
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task MultiSendAsync(int tplId, List<Destination> destinations, string ext, params string[] @params)
        {
            var random = _options.Random;
            var timeStamp = _options.TimeStamp;
            var httpContent = _options.HttpMultiContent(ext: ext, tplId: tplId, destinations: destinations, random: random, timeStamp: timeStamp, @params: @params);

            var response = await _httpClient.PostAsync(_options.GetUrl(random), httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("post method send sms failed, try later.");
            }
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync();
        }

        public async Task MultiSendAsync(List<Destination> destinations, string message, string ext = "", bool normal = true)
        {
            var random = _options.Random;
            var timeStamp = _options.TimeStamp;
            var httpContent = _options.HttpMultiContent(ext: ext, destinations: destinations, random: random, timeStamp: timeStamp, message: message, normal: normal);

            var response = await _httpClient.PostAsync(_options.GetUrl(random), httpContent);
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("post method send sms failed, try later.");
            }
            response.EnsureSuccessStatusCode();
        }
    }
}
