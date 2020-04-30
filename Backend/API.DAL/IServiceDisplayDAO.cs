
using System.Collections.Generic;

using API.Models.gateway;
using API.Models.json;

namespace API.DAL
{
    public interface IServiceDisplayDAO
    {
        ICollection<ServiceDisplayResp> GetAllData(string input);
        bool IfClientExist(string apiKey);
    }
}
