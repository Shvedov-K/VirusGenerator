using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace VirusGenerator
{
    public class GetRequest
    {
        HttpWebRequest request;
        string url;

        public string Response { get; set; }

        public GetRequest (string url)
        {
            this.url = url;
        }

        public void Run()
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    Response = new StreamReader(stream).ReadToEnd();
                }
            }
            catch (Exception)
            {

            }
            
        }
        
    }
}
