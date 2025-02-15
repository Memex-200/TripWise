using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripWise.EntityFramework.Models;

namespace TripWise.Core.Models
{
    public class Agent
    {
        [Key]
        public int AgentCode { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<PromoOffer> PromoOffers { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
