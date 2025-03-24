using System;

namespace TokoOnline
{
    public static class Pengaturan
    {
        public static decimal BiayaKirimPakaian { get; set; } = 10000;
        public static decimal BiayaKirimBuku { get; set; } = 5000;
        public static decimal BiayaKirimElektronik { get; set; } = 2000;
    }

    public abstract class Produk
    {
        public string Nama { get; set; }
        public decimal Harga { get; set; }

        protected Produk(string nama, decimal harga)
        {
            Nama = nama;
            Harga = harga;
        }

        public abstract decimal HitungOngkosKirim();
        public abstract string DapatkanInfoTampilan();

        public override string ToString() => DapatkanInfoTampilan();
    }

    public class Elektronik : Produk
    {
        public double Berat { get; set; }

        public Elektronik(string nama, decimal harga, double berat)
            : base(nama, harga)
        {
            Berat = berat;
        }

        public override decimal HitungOngkosKirim()
        {
            return (decimal)Berat * Pengaturan.BiayaKirimElektronik;
        }

        public override string DapatkanInfoTampilan()
        {
            return $"[Elektronik] {Nama} - Harga: {Harga:C}";
        }
    }

    public class Pakaian : Produk
    {
        public Pakaian(string nama, decimal harga)
            : base(nama, harga)
        {
        }

        public override decimal HitungOngkosKirim()
        {
            return Pengaturan.BiayaKirimPakaian;
        }

        public override string DapatkanInfoTampilan()
        {
            return $"[Pakaian] {Nama} - Harga: {Harga:C}";
        }
    }

    public class Buku : Produk
    {
        public Buku(string nama, decimal harga)
            : base(nama, harga)
        {
        }

        public override decimal HitungOngkosKirim()
        {
            return Pengaturan.BiayaKirimBuku;
        }

        public override string DapatkanInfoTampilan()
        {
            return $"[Buku] {Nama} - Harga: {Harga:C}";
        }
    }

    public class ItemKeranjang
    {
        public Produk Produk { get; }
        public int Jumlah { get; set; }

        public ItemKeranjang(Produk produk, int jumlah)
        {
            Produk = produk;
            Jumlah = jumlah;
        }

        public decimal TotalHarga => Produk.Harga * Jumlah;
        public decimal TotalOngkosKirim => Produk.HitungOngkosKirim() * Jumlah;
    }
}
