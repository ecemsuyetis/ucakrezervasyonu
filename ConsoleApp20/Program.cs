using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Uçak sınıfı
public class Aircraft
{
    // Uçak ID'si
    [Key]
    public int Id { get; set; }

    // Uçak modeli
    public string Model { get; set; }

    // Uçak markası
    public string Brand { get; set; }

    // Seri numarası
    public string SerialNumber { get; set; }

    // Koltuk kapasitesi
    public int SeatCapacity { get; set; }
}

// Lokasyon sınıfı
public class Location
{
    // Lokasyon ID'si
    [Key]
    public int Id { get; set; }

    // Ülke
    public string Country { get; set; }

    // Şehir
    public string City { get; set; }

    // Havaalanı
    public string Airport { get; set; }

    // Aktiflik durumu
    public bool IsActive { get; set; }
}

// Rezervasyon sınıfı
public class Reservation
{
    // Rezervasyon ID'si
    [Key]
    public int Id { get; set; }

    // Hangi uçağa yapıldığı
    public int AircraftId { get; set; }

    // Koltuk numarası
    public int SeatNumber { get; set; }

    // Müşteri adı
    public string CustomerName { get; set; }

    // Rezervasyon tarihi
    public DateTime ReservationDate { get; set; }

    // Satıldı mı?
    public bool IsSold { get; set; }

    // Uçak ilişkisi
    [ForeignKey("AircraftId")]
    public Aircraft Aircraft { get; set; }
}

// Veritabanı bağlantı sınıfı
public class FlightDbContext : DbContext
{
    // Uçaklar tablosu
    public DbSet<Aircraft> Aircrafts { get; set; }

    // Lokasyonlar tablosu
    public DbSet<Location> Locations { get; set; }

    // Rezervasyonlar tablosu
    public DbSet<Reservation> Reservations { get; set; }

    // Veritabanı bağlantısının yapılandırılması
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=flights.db");
    }
}

// Rezervasyon işlemlerini gerçekleştirecek sınıf
public class ReservationManager
{
    private readonly FlightDbContext _dbContext;

    public ReservationManager()
    {
        _dbContext = new FlightDbContext();
    }

    // Koltuk rezervasyonu yapma
    public void ReserveSeat(int aircraftId, int seatNumber, string customerName)
    {
        var reservation = new Reservation
        {
            AircraftId = aircraftId,
            SeatNumber = seatNumber,
            CustomerName = customerName,
            ReservationDate = DateTime.Now,
            IsSold = true
        };

        _dbContext.Reservations.Add(reservation);
        object value = _dbContext.SaveChanges();
    }

    // Bir uçağa ait rezervasyonları getirme
    public List<Reservation> GetReservationsByAircraft(int aircraftId)
    {
        return _dbContext.Reservations.Where(r => r.AircraftId == aircraftId).ToList();
    }
}

// Ana sınıf
class Program
{
    static void Main(string[] args)
    {
        // Test etmek için kullanılabilir
    }
}

