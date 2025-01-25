using System.IO;
using System.Net;
using System.Text;

namespace MeteoraBot.Utilities
{
    public class HttpHelper
    {
        private static HttpHelper helper;
        public static HttpHelper Instance => helper ?? (helper = new HttpHelper());
        public async Task<ResponseParameter> GetRequestAsync(string Url)
        {
            try
            {
                var request = WebRequest.Create(Url) as HttpWebRequest;
                request.Method = "GET";
                #region SetRequest Parameter.

                #endregion
                using(var response = await request.GetResponseAsync())
                using(var stream = response.GetResponseStream())
                {
                    return new ResponseParameter { Success = true, Issue = null, Response = new StreamReader(stream).ReadToEnd() };
                }
            }
            catch(Exception e) { return new ResponseParameter {Response=string.Empty,Success=false,Issue=e }; }
        }
        public async Task<ResponseParameter> PostRequestAsync(string Url,string Body)
        {
            try
            {
                var request = WebRequest.Create(Url) as HttpWebRequest;
                request.Method = "POST";
                #region SetRequest Parameter.

                #endregion
                var body = Encoding.UTF8.GetBytes(Body);
                request.ContentLength = body.Length;
                using(var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(body, 0, body.Length);
                }
                using (var response = await request.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                {
                    return new ResponseParameter { Success = true, Issue = null, Response = new StreamReader(stream).ReadToEnd() };
                }
            }
            catch (Exception e) { return new ResponseParameter { Response = string.Empty, Success = false, Issue = e }; }
        }
    }
    public class ResponseParameter
    {
        public string Response { get; set; }
        public bool Success {  get; set; }
        public Exception Issue { get; set; }
    }
}
