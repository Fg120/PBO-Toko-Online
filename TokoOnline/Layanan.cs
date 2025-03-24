using System;
using System.Collections.Generic;

namespace TokoOnline
{
    public class LayananProduk
    {
        private readonly List<Produk> _produk = new List<Produk>();

        public LayananProduk()
        {
            _produk.Add(new Elektronik("Laptop Gaming", 15000000, 2.5));
            _produk.Add(new Pakaian("Kaos Polos", 100000));
            _produk.Add(new Buku("Novel Fiksi", 75000));
        }

        public IReadOnlyList<Produk> AmbilSemuaProduk() => _produk.AsReadOnly();

        public void TambahProduk(Produk produk)
        {
            _produk.Add(produk);
        }

        public bool PerbaruiProduk(int index, Produk produkBaru)
        {
            if (index < 0 || index >= _produk.Count)
                return false;

            _produk[index] = produkBaru;
            return true;
        }

        public bool HapusProduk(int index)
        {
            if (index < 0 || index >= _produk.Count)
                return false;

            _produk.RemoveAt(index);
            return true;
        }

        public Produk AmbilProduk(int index)
        {
            if (index < 0 || index >= _produk.Count)
                return null;

            return _produk[index];
        }

        public int JumlahProduk => _produk.Count;
    }

    public class LayananKeranjangBelanja
    {
        private readonly List<ItemKeranjang> _item = new List<ItemKeranjang>();

        public IReadOnlyList<ItemKeranjang> Item => _item.AsReadOnly();

        public void TambahAtauPerbaruiItem(Produk produk, int jumlah)
        {
            var itemAda = _item.Find(i => i.Produk.Nama.Equals(produk.Nama, StringComparison.OrdinalIgnoreCase));

            if (itemAda != null)
            {
                itemAda.Jumlah = jumlah;
            }
            else
            {
                _item.Add(new ItemKeranjang(produk, jumlah));
            }
        }

        public bool PerbaruiJumlahItem(int index, int jumlah)
        {
            if (index < 0 || index >= _item.Count || jumlah < 1)
                return false;

            _item[index].Jumlah = jumlah;
            return true;
        }

        public bool HapusItem(int index)
        {
            if (index < 0 || index >= _item.Count)
                return false;

            _item.RemoveAt(index);
            return true;
        }

        public decimal HitungTotalProduk()
        {
            decimal total = 0;
            foreach (var i in _item)
                total += i.TotalHarga;
            return total;
        }

        public decimal HitungTotalOngkosKirim()
        {
            decimal total = 0;
            foreach (var i in _item)
                total += i.TotalOngkosKirim;
            return total;
        }

        public decimal HitungGrandTotal() => HitungTotalProduk() + HitungTotalOngkosKirim();

        public void Bersihkan()
        {
            _item.Clear();
        }

        public int JumlahItem => _item.Count;
    }
}
