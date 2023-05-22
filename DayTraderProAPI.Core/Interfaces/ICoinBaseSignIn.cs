using DayTraderProAPI.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Interfaces
{
    public interface ICoinBaseSignIn
    {
        // response_type = code
        Task<string> RequestTemporaryCode();

        // grant_type = authorization_code
        Task<AccessResponse> RequestAccessKey(string AppUserId);
    }
}
