﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class OfferTransportService
    {
        [Key]
        public int OfferCode { get; set; }

        public int TransportServiceId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int DiscountPercent { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalServicePrice { get; set; }

        public string Description { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual TransportService TransportService { get; set; }
    }
}
