﻿using Newtonsoft.Json;
using RestSharp;
using RuiJi.Net.Core.Configuration;
using RuiJi.Net.Core.Extensions;
using RuiJi.Net.Core.Extractor;
using RuiJi.Net.Core.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Net.NodeVisitor
{
    public class Feeder
    {
        public static List<ExtractFeatureBlock> GetExtractBlock(string url, bool useBlock = false)
        {
            var proxyUrl = "";

            if (NodeConfigurationSection.Standalone)
            {
                proxyUrl = ConfigurationManager.AppSettings["RuiJiServer"];
            }
            else
            {
                proxyUrl = ProxyManager.Instance.Elect(NodeProxyTypeEnum.FEEDPROXY);
            }

            if (string.IsNullOrEmpty(proxyUrl))
                throw new Exception("no available Extractor proxy servers");

            proxyUrl = IPHelper.FixLocalUrl(proxyUrl);

            proxyUrl = proxyUrl.Replace("118.31.61.230", "172.16.50.52");

            var client = new RestClient("http://" + proxyUrl);
            var restRequest = new RestRequest("api/fp/rule/match?url=" + url);
            restRequest.Method = Method.GET;
            restRequest.JsonSerializer = new NewtonJsonSerializer();

            restRequest.Timeout = 15000;

            var restResponse = client.Execute(restRequest);

            var response = JsonConvert.DeserializeObject<List<ExtractFeatureBlock>>(restResponse.Content);

            return response;
        }

        public static string GetFeedJobs(string pages)
        {
            var proxyUrl = "";

            if (NodeConfigurationSection.Standalone)
            {
                proxyUrl = ConfigurationManager.AppSettings["RuiJiServer"];
            }
            else
            {
                proxyUrl = ProxyManager.Instance.Elect(NodeProxyTypeEnum.FEEDPROXY);
            }

            if (string.IsNullOrEmpty(proxyUrl))
                throw new Exception("get feedjobs: proxyUrl can't be null");

            if (string.IsNullOrEmpty(pages))
                throw new Exception("get feedjobs: pages can't be null");

            proxyUrl = IPHelper.FixLocalUrl(proxyUrl);

            var client = new RestClient("http://" + proxyUrl);
            var restRequest = new RestRequest("api/fp/feed/page");
            restRequest.Method = Method.GET;
            restRequest.AddParameter("pages", pages);
            restRequest.Timeout = 15000;

            var restResponse = client.Execute(restRequest);

            return restResponse.Content;

        }

        public static bool SaveContent(object content)
        {
            var proxyUrl = "";

            if (NodeConfigurationSection.Standalone)
            {
                proxyUrl = ConfigurationManager.AppSettings["RuiJiServer"];
            }
            else
            {
                proxyUrl = ProxyManager.Instance.Elect(NodeProxyTypeEnum.FEEDPROXY);
            }

            if (string.IsNullOrEmpty(proxyUrl))
                throw new Exception("no available Extractor proxy servers");

            proxyUrl = IPHelper.FixLocalUrl(proxyUrl);

            var client = new RestClient("http://" + proxyUrl);
            var restRequest = new RestRequest("api/fp/content/save");
            restRequest.Method = Method.POST;
            restRequest.AddJsonBody(content);
            restRequest.Timeout = 15000;

            var restResponse = client.Execute(restRequest);

            var response = JsonConvert.DeserializeObject<bool>(restResponse.Content);

            return response;
        }
    }
}