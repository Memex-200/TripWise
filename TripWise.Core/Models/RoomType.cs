using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Core.Models;

namespace TripWise.EntityFramework.Models
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Required, StringLength(100)]
        public string TypeName { get; set; }

        public virtual ICollection<HotelService> HotelServices { get; set; }
    }
}
