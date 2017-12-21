﻿using Newtonsoft.Json;
using System;
using System.Net;

namespace XmrStakApi
{
	public class Miner
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public string Response { private get; set; }
		public WebError Error { get; set; }
		public WebProxy Proxy { get; set; }

		public Miner() { }

		public Miner(string name, string url, int port, string username, string password, WebProxy proxy = null)
		{
			Name = name;
			Url = url;
			Port = port;
			Username = username;
			Password = password;
			Proxy = proxy;
		}

		public Miner(string name, string url, int port, string username, string password, string proxyUrl, string proxyUsername, string proxyPassword)
		{
			Name = name;
			Url = url;
			Port = port;
			Username = username;
			Password = password;

			if (!string.IsNullOrWhiteSpace(proxyUrl))
			{
				Proxy = new WebProxy(proxyUrl, true)
				{
					Credentials = new NetworkCredential(proxyUsername, proxyPassword)
				};
			}
		}

		public Data Data
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Response))
				{
					return JsonConvert.DeserializeObject<Data>(Response);
				}

				return null;
			}
		}

		public Uri Uri
		{
			get
			{
				return new Uri($"{Url}:{Port}/api.json");
			}
		}

		public bool Secured
		{
			get
			{
				return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
			}
		}

		public NetworkCredential Credentials
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
				{
					return new NetworkCredential(Username, Password);
				}

				return null;
			}
		}

		public void SetProxy(string url, string username, string password)
		{
			Proxy = !string.IsNullOrWhiteSpace(url) ? new WebProxy(url, true)
			{
				Credentials = new NetworkCredential(username, password)
			} : null;
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}
	}
}