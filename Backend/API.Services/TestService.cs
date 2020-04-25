using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.Models.gateway;

namespace API.Services
{
    public class TestService
    {
        private readonly ApiGatewayContext _apiGatewayContext;
        public TestService(ApiGatewayContext apiGatewayContext)
        {
            _apiGatewayContext = apiGatewayContext;
        }

        public string GetTeams(string username)
        {
            var results = _apiGatewayContext.Team
                                        .Where(team => team.Username == username)
                                        .Select(t => new { t.Username })
                                        .ToList()[0].Username;
            return results.ToString();
        }

        public object CheckIfServiceOwner(string username, string endpoint)
        {
            var owner = from team in _apiGatewayContext.Team
                        join service in _apiGatewayContext.Service on team.ClientId equals service.Owner
                        where service.Endpoint == endpoint && team.Username == username
                        select new { team.Username, service.Endpoint };

            var listResults = owner.ToList();
            // return false if we query returned no results.
            if(listResults.Count == 0)
            {
                return false;
            }





            var bah = owner.ToList()[0];
            return bah;
        }

    }
}
