using CaseStudy.Models;

namespace CaseStudy.Contracts;

public sealed class ReservationRequest
{
    public Tren Tren { get; init; } = new();
    public int RezervasyonYapilacakKisiSayisi { get; init; }
    public bool? KisilerFarkliVagonlaraYerlestirilebilir { get; init; }
}
