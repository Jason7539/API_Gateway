
using System.Collections.Generic;

using API.Models.gateway;

namespace DataAccessLayer
{
    public interface IServiceDisplayDAO
    {
        ICollection<Service> GetAllData(string input);
        bool IfTeamExist(string apiKey);
    }
}
