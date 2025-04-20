using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TripWise.Domain.Entities;

public class HotelService
{
    [Key]
    public int HotelServiceId { get; set; }

    public int HotelId { get; set; }

    public int RoomTypeId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ServicePrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal FinalServicePrice { get; set; }

    public bool Active { get; set; }

    public virtual Hotel Hotel { get; set; }

    public virtual RoomType RoomType { get; set; }

    public virtual ICollection<PromoOfferHotelService> PromoOffers { get; set; }

    public virtual ICollection<Offer> Offers { get; set; } // One-to-many: HotelService can be part of many Offers
}