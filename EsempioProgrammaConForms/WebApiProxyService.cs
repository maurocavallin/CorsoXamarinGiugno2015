using System;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net;

namespace EsempioProgrammaConForms
{
	public class WebApiProxyService
	{
 
		HttpClientHandler handler = null;

		public WebApiProxyService ()
		{
			 

		}


		private async Task<HttpClient> createHttpClientAsync()
		{
			string serverAddress = CustomSettings.ServerAddress;


			if (handler == null) {
				CookieContainer cookieContainerAuth = new CookieContainer();
				handler = new HttpClientHandler();
				handler.CookieContainer = cookieContainerAuth;

				var clientAuth = new HttpClient(handler);

				clientAuth.BaseAddress = new Uri(String.Format("http://{0}/ENOPIAVE/Account/LogOn", serverAddress));
				var authResp = await clientAuth.GetAsync ("");
			}

			var client = new HttpClient(handler);
			client.Timeout = TimeSpan.FromSeconds(40);
			client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue ("application/json"));

			//// http://10.123.26.1/ENOPIAVE/rest/catalogoaof/articolo
			// http://10.123.26.1/ENOPIAVE/Account/LogOn
			//

			string fullUrl = String.Format ("http://{0}/ENOPIAVE/catalogoaof/", serverAddress); // articolo

			client.BaseAddress = new Uri(fullUrl);

			return client;
		}

		public async Task<List<Articolo>> GetArticoliAsync()
		{
			// var requestDTO = createRequestObject<RequestDTO> ();
			// string requestParamsAsString = JsonConvert.SerializeObject(requestDTO);


			// var httpContent = createHttpPostRequestContent (requestParamsAsString);
  
			var httpClient = await createHttpClientAsync ();

			HttpResponseMessage response = await httpClient.GetAsync ("articologet"); // , httpContent); 
			response.EnsureSuccessStatusCode (); // http://www.jayway.com/2012/03/13/httpclient-makes-get-and-post-very-simple/
			string content = await response.Content.ReadAsStringAsync ();


			List<Articolo> resp = 
				JsonConvert.DeserializeObject<List<Articolo>>(content);



			return resp; // Task<TResult> returns an object of type TResult
		}

		public async Task<List<Categoria>> GetCategorieAsync()
		{
			// var requestDTO = createRequestObject<RequestDTO> ();
			// string requestParamsAsString = JsonConvert.SerializeObject(requestDTO);


			// var httpContent = createHttpPostRequestContent (requestParamsAsString);

			var httpClient = await createHttpClientAsync ();

			// 10.123.26.1/ENOPIAVE/catalogoaof/categoriaget
			HttpResponseMessage response = await httpClient.GetAsync ("categoriaget"); // , httpContent); 
			response.EnsureSuccessStatusCode (); // http://www.jayway.com/2012/03/13/httpclient-makes-get-and-post-very-simple/
			string content = await response.Content.ReadAsStringAsync ();


			List<Categoria> resp = 
				JsonConvert.DeserializeObject<List<Categoria>>(content);



			return resp; // Task<TResult> returns an object of type TResult
		}

		private static string test = 
			"[{\"art_ID\":\".ALI1PP\",\"art_DESC\":\"BOMBOLA PACCO 16P ALIGAL1   \"," +
			"\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_ALI1PP.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_ALI1PP.jpg\"," +
			"\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".ALI1PP\"}," + 
		     "{\"art_ID\":\".BOMBOL\",\"art_DESC\":\"BOMBOLA ANIDRIDE SOLFOROSA   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_BOMBOL.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_BOMBOL.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".BOMBOL\"},{\"art_ID\":\".CASS02\",\"art_DESC\":\"CASSONE VUOTO GHIACCIO KG 200   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_CASS02.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_CASS02.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".CASS02\"},{\"art_ID\":\".CASS04\",\"art_DESC\":\"CASSONE VUOTO GHIACCIO KG 400   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_CASS04.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_CASS04.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".CASS04\"},{\"art_ID\":\".CISTER\",\"art_DESC\":\"CISTERNETTA SU PALLET   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_CISTER.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_CISTER.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".CISTER\"},{\"art_ID\":\".FUST50\",\"art_DESC\":\"FUSTINI VUOTI 50 SODIO IDRATO   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_FUST50.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_FUST50.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".FUST50\"},{\"art_ID\":\".GHIA01\",\"art_DESC\":\"CONTENITORE GHIACCIO KG 120   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_GHIA01.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_GHIA01.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".GHIA01\"},{\"art_ID\":\".GHIA02\",\"art_DESC\":\"CONTENITORE GHIACCIO KG 200   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_GHIA02.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_GHIA02.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".GHIA02\"},{\"art_ID\":\".GHIA03\",\"art_DESC\":\"CONTENITORE GHIACCIO KG 400   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_GHIA03.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_GHIA03.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".GHIA03\"},{\"art_ID\":\".INT00\",\"art_DESC\":\"INTERFALDA PLASTICA CAUZIONATA   \",\"art_IMG\":\"/ENOPIAVE/Image/GetAttachmentsImage?relativePath=Articolo/Images/_INT00.jpg\\u0026relativePathDefault=/Media/Image/NoArticolo.png\",\"art_IMGNAME\":\"Articolo/Images/_INT00.jpg\",\"art_TIPOLOGIA\":\"PF\",\"art_CODICE\":\".INT00\"}]";

  
	 
	}
}
