using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class HotelService
    {
        [Key]
        public int HotelServiceId { get; set; }

        public int HotelId { get; set; }

        public int RoomTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ServicePrice { get; set; }

        public bool Active { get; set; }

        public virtual Hotel Hotel { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual ICollection<PromoOfferHotelService> PromoOffers { get; set; }

        public virtual ICollection<OfferHotelService> Offers { get; set; }
    }
}
