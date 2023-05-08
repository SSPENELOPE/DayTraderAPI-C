using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace DayTraderProAPI.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string? Token { get; set; }
    }
}
