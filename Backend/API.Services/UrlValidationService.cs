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
        private readonly ApiGatewayContext _context;

        public UrlValidationService(ApiGatewayContext apiGatewayContext)
        {
            _context = apiGatewayContext;
        }

        /// <summary>
        /// Check if a team's callback url is unique
        /// </summary>
        /// <param name="url">Url to check</param>
        /// <returns>Bool representing whether a url is unique</returns>
        public bool IsCallBackURLUnique(string url)
        {

            // Attempt to find a team call back url with inputted url.
            var callBackUrls = _context.Team
                                    .Where(s => s.CallbackUrl == url)
                                    .ToList();

            // Return true if url is unique.
            return callBackUrls.Count == 0;

        }

        /// <summary>
        /// Check if a team's website url is unique
        /// </summary>
        /// <param name="url">Url to check</param>
        /// <returns>Bool representing whether the url is unique</returns>
        public bool IsWebsiteURLUnique(string url)
        {
            // Attempt to find a team website url with inputted url.
            var websiteUrls = _context.Team
                                .Where(s => s.WebsiteUrl == url)
                                .ToList();

            // Return true if url is unique.
            return websiteUrls.Count == 0;
        }

        /// <summary>
        /// Check if a url is https
        /// </summary>
        /// <param name="url">Url to test</param>
        /// <returns>Bool representing whether the url is https</returns>
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

        /// <summary>
        /// Test if a url is valid
        /// </summary>
        /// <param name="url">Url to test</param>
        /// <returns>Bool representing whether the url is valid</returns>
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

        /// <summary>
        /// Test whether a url is alive using a HEAD request
        /// </summary>
        /// <param name="url">Url to test</param>
        /// <returns>Bool representing whether the url is alive</returns>
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
