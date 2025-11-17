using System.Collections.Generic;
using System.Linq;
using CaseStudy.Contracts;

namespace CaseStudy.Services;

public static class ReservationRequestValidator
{
    public static IDictionary<string, string[]> Validate(ReservationRequest? request)
    {
        var errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);

        if (request is null)
        {
            errors.Add(string.Empty, new[] { "Gecersiz veya eksik istek govdesi" });
            return errors;
        }

        if (request.RezervasyonYapilacakKisiSayisi <= 0)
        {
            errors.Add(nameof(request.RezervasyonYapilacakKisiSayisi), new[] { "Kisi sayisi 0'dan buyuk olmalidir" });
        }

        if (request.KisilerFarkliVagonlaraYerlestirilebilir is null)
        {
            errors.Add(nameof(request.KisilerFarkliVagonlaraYerlestirilebilir), new[] { "Kisilerin farkli vagonlara yerlestirilip yerlestirilemeyecegi belirtilmelidir" });
        }

        if (request.Tren is null)
        {
            errors.Add(nameof(request.Tren), new[] { "Tren bilgisi zorunludur" });
            return errors;
        }

        if (string.IsNullOrWhiteSpace(request.Tren.Ad))
        {
            errors.Add("Tren.Ad", new[] { "Tren adi bos olamaz" });
        }

        if (request.Tren.Vagonlar is null || !request.Tren.Vagonlar.Any())
        {
            errors.Add("Tren.Vagonlar", new[] { "En az bir vagon tanimlanmalidir" });
        }
        else
        {
            var vagonIndex = 0;
            foreach (var vagon in request.Tren.Vagonlar)
            {
                if (string.IsNullOrWhiteSpace(vagon.Ad))
                {
                    errors.Add($"Tren.Vagonlar[{vagonIndex}].Ad", new[] { "Vagon adi bos olamaz" });
                }

                if (vagon.Kapasite <= 0)
                {
                    errors.Add($"Tren.Vagonlar[{vagonIndex}].Kapasite", new[] { "Kapasite 0'dan buyuk olmalidir" });
                }

                if (vagon.DoluKoltukAdet < 0)
                {
                    errors.Add($"Tren.Vagonlar[{vagonIndex}].DoluKoltukAdet", new[] { "Dolu koltuk adedi negatif olamaz" });
                }
                else if (vagon.DoluKoltukAdet > vagon.Kapasite)
                {
                    errors.Add($"Tren.Vagonlar[{vagonIndex}].DoluKoltukAdet", new[] { "Dolu koltuk adedi kapasiteden fazla olamaz" });
                }

                vagonIndex++;
            }
        }

        return errors;
    }
}
