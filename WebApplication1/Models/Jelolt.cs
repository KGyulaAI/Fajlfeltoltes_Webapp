﻿namespace WebApplication1.Models
{
    public class Jelolt
    {
        public int Id { get; set; }
        public string Nev { get; set; }
        public int Kerulet { get; set; }
        public int SzavazatokSzama { get; set; }
        public string PartRovidNev { get; set; }
        public Part Part { get; set; } //Idegen kulcs
    }
}
