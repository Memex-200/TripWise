using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.EntityFramework.Models
{
    public class PromoOffer
    {
        [Key]
        public int PromoOfferCode { get; set; }

        [Required, StringLength(100)]
        public string PromoOfferName { get; set; }

        [Required]
        public DateTime ActiveFrom { get; set; }

        [Required]
        public DateTime ActiveTo { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<PromoOfferHotelService> PromoOfferHotelServices { get; set; }

        public virtual ICollection<PromoOfferTransportService> PromoOfferTransportServices { get; set; }

        [NotMapped]
        public object TransportService { get; internal set; }
    }
}
