using System.Collections.Generic;

namespace CaseStudy.Contracts;

public sealed class ReservationResponse
{
    public bool RezervasyonYapilabilir { get; init; }
    public IReadOnlyCollection<YerlesimAyrinti> YerlesimAyrinti { get; init; } = new List<YerlesimAyrinti>();
}

public sealed class YerlesimAyrinti
{
    public string VagonAdi { get; init; } = string.Empty;
    public int KisiSayisi { get; init; }
}
