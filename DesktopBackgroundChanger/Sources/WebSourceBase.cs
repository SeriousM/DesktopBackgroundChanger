using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopBackgroundChanger.Sources
{
	public abstract class WebSourceBase
	{
		private readonly HttpClientHandler _httpClientHandler;
		private readonly HttpClient _httpClient;

		protected WebSourceBase()
		{
			_httpClientHandler = new HttpClientHandler();
			_httpClient = new HttpClient(_httpClientHandler);
		}

		protected CookieContainer GetCookieContainer()
		{
			return _httpClientHandler.CookieContainer;
		}

		protected async Task<string> HttpGet(string requestUri)
		{
			return await _httpClient.GetStringAsync(requestUri);
		}
	}
}