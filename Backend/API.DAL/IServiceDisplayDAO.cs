
using System.Collections.Generic;

using API.Models.gateway;

namespace API.DAL
{
    public interface IServiceDisplayDAO
    {
        ICollection<Service> GetAllData(string input);
        bool IfTeamExist(string apiKey);
    }
}
