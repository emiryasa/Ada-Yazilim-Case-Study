using System;

namespace CaseStudy.Models;

public sealed class Vagon
{
    public string Ad { get; init; } = string.Empty;
    public int Kapasite { get; init; }
    public int DoluKoltukAdet { get; init; }

    public int OnlineMaksimumKapasite => (int)Math.Floor(Kapasite * 0.7);

    public int MusaitKoltuk => Math.Max(0, OnlineMaksimumKapasite - DoluKoltukAdet);

    public bool MusaitMi(int kisiSayisi) => kisiSayisi <= MusaitKoltuk;
}
