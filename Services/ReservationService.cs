using CaseStudy.Contracts;
using CaseStudy.Models;

namespace CaseStudy.Services;

public sealed class ReservationService : IReservationService
{
    public ReservationResponse CreateReservation(ReservationRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.RezervasyonYapilacakKisiSayisi <= 0)
        {
            return BasarisizSonuc();
        }

        var vagonlar = request.Tren?.Vagonlar ?? Array.Empty<Vagon>();
        var uygunVagonlar = vagonlar
            .Select(v => new { Vagon = v, MusaitKoltuk = v.MusaitKoltuk })
            .Where(x => x.MusaitKoltuk > 0)
            .ToList();

        if (!uygunVagonlar.Any())
        {
            return BasarisizSonuc();
        }

        var kisilerFarkliYerlestirilebilir = request.KisilerFarkliVagonlaraYerlestirilebilir ?? false;

        if (!kisilerFarkliYerlestirilebilir)
        {
            var tekVagon = uygunVagonlar.FirstOrDefault(x => x.MusaitKoltuk >= request.RezervasyonYapilacakKisiSayisi);
            if (tekVagon is null)
            {
                return BasarisizSonuc();
            }

            return new ReservationResponse
            {
                RezervasyonYapilabilir = true,
                YerlesimAyrinti = new[]
                {
                    new YerlesimAyrinti
                    {
                        VagonAdi = tekVagon.Vagon.Ad,
                        KisiSayisi = request.RezervasyonYapilacakKisiSayisi
                    }
                }
            };
        }

        var kalanKisiSayisi = request.RezervasyonYapilacakKisiSayisi;
        var yerlestirmeler = new List<YerlesimAyrinti>();

        foreach (var uygunVagon in uygunVagonlar)
        {
            if (kalanKisiSayisi <= 0)
            {
                break;
            }

            var yerlestirilecek = Math.Min(kalanKisiSayisi, uygunVagon.MusaitKoltuk);
            if (yerlestirilecek <= 0)
            {
                continue;
            }

            yerlestirmeler.Add(new YerlesimAyrinti
            {
                VagonAdi = uygunVagon.Vagon.Ad,
                KisiSayisi = yerlestirilecek
            });

            kalanKisiSayisi -= yerlestirilecek;
        }

        if (kalanKisiSayisi > 0)
        {
            return BasarisizSonuc();
        }

        return new ReservationResponse
        {
            RezervasyonYapilabilir = true,
            YerlesimAyrinti = yerlestirmeler
        };
    }

    private static ReservationResponse BasarisizSonuc() => new()
    {
        RezervasyonYapilabilir = false,
        YerlesimAyrinti = Array.Empty<YerlesimAyrinti>()
    };
}
