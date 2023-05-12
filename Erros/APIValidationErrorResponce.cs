using Microsoft.AspNetCore.Mvc;
using DayTraderProAPI.Controllers;
using System.Collections.Generic;
using DayTraderProAPI.Erros;

namespace DayTraderProAPI.Errors
{
    public class APIValidationErrorResponce : APIResponce
    {
        public APIValidationErrorResponce() : base(400)
        {

        }
        public IEnumerable<string> Errors { get; set; }

    }
}
