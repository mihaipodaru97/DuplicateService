using System;

namespace MagazinAuto.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public Caroserie Caroserie { get; set; }
        public Cutie Cutie { get; set; }
        public Transmisie Transmisie { get; set; }
        public NormaPoluare NormaPoluare { get; set; }
        public Combustibil Combustibil { get; set; }
        public int CP { get; set; }
        public int CapacitateCilindrica { get; set; }
        public int Km { get; set; }
        public int AnFabricatie { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public string Descriere { get; set; }
        public int Pret { get; set; }
        public byte[] Poza { get; set; }
    }
}
