using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.Core.Models;

namespace TripWise.EntityFramework.Models
{
    public class PromoOfferHotelService
    {
        [Key]
        public int PromoOfferId { get; set; }

        public int HotelServiceId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int DiscountPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalServicePrice { get; set; }

        public string Description { get; set; }

        public virtual PromoOffer PromoOffer { get; set; }

        public virtual HotelService HotelService { get; set; }
    }
}
