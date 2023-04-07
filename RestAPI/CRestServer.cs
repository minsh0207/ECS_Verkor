using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using log4net;
using Nancy;
using Nancy.Hosting.Self;

namespace RestAPIServer
{
	/// <summary>
	/// REST Server
	/// </summary>
	public static class CRestServer
	{
		//private static readonly ILog _LOG = LogManager.GetLogger(typeof(CRestServer));

		private static NancyHost _RestHost;
		private static string _HostURI = string.Empty;

		public static bool _IsServerRunning = false;


		public static string HostURI()
		{
			return _HostURI;
		}
		public static bool Start(string HostURL, string HostPort)
		{
			if (InitRestServer(HostURL, HostPort) == false) return false;
			if (ServerStart() == false) return false;
			return true;
		}

		public static void Stop()
		{
			ServerStop();
		}

		/// <summary>
		/// Initialize
		/// </summary>
		private static bool InitRestServer(string HostURL, string HostPort)
		{
			try
			{
				//string strHostURL = CConfig.GetAppConfig("REST_BASE_URL");
				//string strHostPort = CConfig.GetAppConfig("REST_PORT");
				string strHostURL = HostURL;
				string strHostPort = HostPort;

				_HostURI = $"{strHostURL}:{strHostPort}";

				Uri uri = new Uri(_HostURI);
				var varConfig = new HostConfiguration
				{   
					//RewriteLocalhost = true,
					UrlReservations = new UrlReservations { CreateAutomatically = true }
				};

				_RestHost = new NancyHost(varConfig, uri);
				return true;
			}
			catch (Exception ex)
			{
				//Console.WriteLine($"[MAIN] Application running error(Err) : {ex.Message}");
				//_LOG.Error($"InitRestServer() : Exception : {e.Message}");
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// REST Server Start
		/// </summary>
		private static bool ServerStart()
		{
			try
			{
				_RestHost.Start();
				_IsServerRunning = true;

				//Console.WriteLine($"[MAIN] Application running ON : {_HostURI}");
				//_LOG.Info($"ServerStart({_HostURI})");

				//MainForm.EnableStartStopButton(true);
				//m_Host.Stop();
				return true;
			}
			catch (System.Net.HttpListenerException ex)
			{
				//Console.WriteLine($"[MAIN] Application running error(Net) : {e.Message}");
				//_LOG.Error($"ServerStart() : HttpListenerException : {e.Message}");
				Console.WriteLine(ex.Message);
				return false;
			}
			catch (Exception ex)
			{
				//Console.WriteLine($"[MAIN] Application running error(Err) : {ex.Message}");
				//_LOG.Error($"ServerStart() : Exception : {e.Message}");
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		/// <summary>
		/// REST Server Stop
		/// </summary>
		private static void ServerStop()
		{
			try
			{
				if (_IsServerRunning == false) return;
				if (_RestHost != null)
				{
					_RestHost.Stop();
					_RestHost.Dispose();
					//_LOG.Info($"ServerStop()");
				}
				_IsServerRunning = false;
			}
			catch (Exception ex)
			{
				//Console.WriteLine($"[MAIN] Application running error(Err) : {ex.Message}");
				//_LOG.Error($"ServerStop() : Exception : {e.Message}");
				Console.WriteLine(ex.Message);
			}
		}



	}
}
