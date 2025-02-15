using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripWise.EntityFramework.Models
{
    public class TicketType
    {
        [Key]
        public int TicketTypeId { get; set; }

        [Required, StringLength(100)]
        public string TypeName { get; set; }

        public virtual ICollection<TransportService> TransportServices { get; set; }
    }
}
