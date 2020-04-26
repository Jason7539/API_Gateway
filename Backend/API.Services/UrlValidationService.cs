using API.AppConstants;
using API.Models.gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace API.Services
{
    public class UrlValidationService
    {
        public bool IsCallBackURLUnique(string url)
        {
            using (var context = new ApiGatewayContext())
            {
                // Attempt to find a team call back url with inputted url.
                var callBackUrls = context.Team
                                        .Where(s => s.CallbackUrl == url)
                                        .ToList();

                // Return true if url is unique.
                return callBackUrls.Count == 0;
            }
        }

        public bool IsWebsiteURLUnique(string url)
        {
            using (var context = new ApiGatewayContext())
            {
                // Attempt to find a team website url with inputted url.
                var websiteUrls = context.Team
                                    .Where(s => s.WebsiteUrl == url)
                                    .ToList();

                // Return true if url is unique.
                return websiteUrls.Count == 0;
            }
        }

        public bool IsUrlHttps(string url)
        {
            try
            {
                var urlCheck = new Uri(url);

                return urlCheck.Scheme == Uri.UriSchemeHttps;
            }
            catch(System.UriFormatException)
            {
                return false;
            }
        }

        public bool IsUrlValid(string url)
        {
            try
            {
                var urlCheck = new Uri(url);
                return true;
            }
            catch(System.UriFormatException)
            {
                return false;
            }
        }

        public bool IsUrlAlive(string url)
        {
    
            var webRequest =  WebRequest.Create(url) as HttpWebRequest;

            // Set the HTTP method to: HEAD.
            webRequest.Method = Constants.HttpHEAD;

            // Get the resonse from the request.
            try
            {
                using (var response = webRequest.GetResponse() as HttpWebResponse)
                {
                    // Return true if we get an 200 status code.
                    return response.StatusCode == HttpStatusCode.OK;
                }

            }
            // Return false if the webrequest doesn't accept HEAD request.
            catch(System.Net.WebException)
            {
                return false;
            }
        }

    }
}
