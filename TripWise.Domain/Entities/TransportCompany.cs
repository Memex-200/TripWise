using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TripWise.Domain.Entities;

public class TransportCompany
{
    [Key]
    public int CompanyId { get; set; }

    [Required, StringLength(100)]
    public string CompanyName { get; set; }

    [Required, StringLength(255)]
    public string HQAddress { get; set; }

    public string Description { get; set; }

    public bool IsPartner { get; set; }

    public bool Active { get; set; }

    [NotMapped]
    [StringLength(255)]
    public string Image { get; set; }
  
    [Column(TypeName = "decimal(18,2)")]
    public decimal TransportServicePrice { get; set; }

    public int CityId { get; set; }

    public int CompanyTypeId { get; set; }

    public virtual City City { get; set; }

    public virtual CompanyType CompanyType { get; set; }

    public virtual ICollection<Offer> Offers { get; set; }

    public virtual ICollection<TransportService> TransportServices { get; set; }
}