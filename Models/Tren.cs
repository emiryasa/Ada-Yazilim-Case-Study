using System.Collections.Generic;

namespace CaseStudy.Models;

public sealed class Tren
{
    public string Ad { get; init; } = string.Empty;
    public IReadOnlyCollection<Vagon> Vagonlar { get; init; } = new List<Vagon>();
}
