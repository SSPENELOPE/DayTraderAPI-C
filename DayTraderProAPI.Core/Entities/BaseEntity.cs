using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTraderProAPI.Core.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }


        public BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
