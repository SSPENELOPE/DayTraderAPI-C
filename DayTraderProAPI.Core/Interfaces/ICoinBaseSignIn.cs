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
        Task<ActionResult> RequestTemporaryCode(string response_type, string client_id);

        // grant_type = authorization_code
        Task<ActionResult> RequestAccesKey(
            string grant_type, 
            string temporary_code, 
            string client_id, 
            string client_secret,
            string redirect_uri);
    }
}
