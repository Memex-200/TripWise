using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.Domain.Entities
{
    public class Customer : IdentityUser<int>
    {
       

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public string Details { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Mobile { get; set; }

        public DateTime CustomerFrom { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }


    }
    }


   