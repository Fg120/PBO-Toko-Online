using System;

namespace TokoOnline
{
    public static class Bantuan
    {
        public static void TampilkanError(string pesan)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(pesan);
            Console.ResetColor();
        }

        public static void TampilkanSukses(string pesan)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(pesan);
            Console.ResetColor();
        }

        public static void TampilkanHeader(string judul)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"=== {judul} ===");
            Console.ResetColor();
        }

        public static void TekanEnterUntukLanjut()
        {
            Console.WriteLine("\nTekan Enter untuk melanjutkan...");
            Console.ReadLine();
        }

        public static bool DapatkanInputYaTidak(string prompt)
        {
            Console.Write($"{prompt} (y/n): ");
            return Console.ReadLine().ToLower() == "y";
        }

        public static int DapatkanInputInt(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= minValue && value <= maxValue)
                    return value;
                TampilkanError($"Input tidak valid. Masukkan angka antara {minValue} dan {maxValue}.");
            }
        }

        public static decimal DapatkanInputDecimal(string prompt, decimal minValue = decimal.MinValue)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out value) && value >= minValue)
                    return value;
                TampilkanError($"Input tidak valid. Masukkan angka minimal {minValue}.");
            }
        }

        public static double DapatkanInputDouble(string prompt, double minValue = double.MinValue)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out value) && value >= minValue)
                    return value;
                TampilkanError($"Input tidak valid. Masukkan angka minimal {minValue}.");
            }
        }

        public static string DapatkanInputString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }

    public class ViewProduk
    {
        private readonly LayananProduk _layananProduk;

        public ViewProduk(LayananProduk layananProduk)
        {
            _layananProduk = layananProduk;
        }

        public void TampilkanProduk()
        {
            Bantuan.TampilkanHeader("Daftar Produk");
            var produk = _layananProduk.AmbilSemuaProduk();
            if (produk.Count == 0)
            {
                Console.WriteLine("Tidak ada produk.");
                return;
            }
            for (int i = 0; i < produk.Count; i++)
                Console.WriteLine($"{i + 1}. {produk[i].DapatkanInfoTampilan()}");
        }

        public void TambahProduk()
        {
            Console.Clear();
            Bantuan.TampilkanHeader("Tambah Produk");
            Console.WriteLine("Pilih kategori produk:");
            Console.WriteLine("1. Elektronik");
            Console.WriteLine("2. Pakaian");
            Console.WriteLine("3. Buku");

            int kategori = Bantuan.DapatkanInputInt("Pilihan: ", 1, 3);
            string nama = Bantuan.DapatkanInputString("Masukkan nama produk: ");
            decimal harga = Bantuan.DapatkanInputDecimal("Masukkan harga produk: ", 0);

            Produk produk;
            switch (kategori)
            {
                case 1:
                    double berat = Bantuan.DapatkanInputDouble("Masukkan berat (kg): ", 0);
                    produk = new Elektronik(nama, harga, berat);
                    break;
                case 2:
                    produk = new Pakaian(nama, harga);
                    break;
                case 3:
                    produk = new Buku(nama, harga);
                    break;
                default:
                    return;
            }
            _layananProduk.TambahProduk(produk);
            Bantuan.TampilkanSukses("Produk berhasil ditambahkan.");
        }

        public void PerbaruiProduk()
        {
            Console.Clear();
            Bantuan.TampilkanHeader("Perbarui Produk");
            TampilkanProduk();
            if (_layananProduk.JumlahProduk == 0) return;

            int index = Bantuan.DapatkanInputInt("Masukkan nomor produk untuk diperbarui: ", 1, _layananProduk.JumlahProduk) - 1;
            Produk produk = _layananProduk.AmbilProduk(index);
            if (produk == null)
            {
                Bantuan.TampilkanError("Produk tidak valid.");
                return;
            }

            if (Bantuan.DapatkanInputYaTidak($"Nama saat ini: {produk.Nama}. Ubah nama?"))
                produk.Nama = Bantuan.DapatkanInputString("Masukkan nama baru: ");
            if (Bantuan.DapatkanInputYaTidak($"Harga saat ini: {produk.Harga}. Ubah harga?"))
                produk.Harga = Bantuan.DapatkanInputDecimal("Masukkan harga baru: ", 0);
            if (produk is Elektronik elektronik)
            {
                if (Bantuan.DapatkanInputYaTidak($"Berat saat ini: {elektronik.Berat} kg. Ubah berat?"))
                    elektronik.Berat = Bantuan.DapatkanInputDouble("Masukkan berat baru (kg): ", 0);
            }
            Bantuan.TampilkanSukses("Produk berhasil diperbarui.");
        }

        public void HapusProduk()
        {
            Console.Clear();
            Bantuan.TampilkanHeader("Hapus Produk");
            TampilkanProduk();
            if (_layananProduk.JumlahProduk == 0) return;

            int index = Bantuan.DapatkanInputInt("Masukkan nomor produk yang akan dihapus (0 untuk batal): ", 0, _layananProduk.JumlahProduk);
            if (index == 0)
            {
                Bantuan.TampilkanSukses("Penghapusan dibatalkan.");
                return;
            }

            index -= 1;
            if (_layananProduk.HapusProduk(index))
                Bantuan.TampilkanSukses("Produk berhasil dihapus.");
            else
                Bantuan.TampilkanError("Gagal menghapus produk.");
        }


        public void TampilkanMenuManajemenProduk()
        {
            bool keluar = false;
            while (!keluar)
            {
                Console.Clear();
                Bantuan.TampilkanHeader("Manajemen Produk");
                Console.WriteLine("1. Tambah Produk");
                Console.WriteLine("2. Perbarui Produk");
                Console.WriteLine("3. Hapus Produk");
                Console.WriteLine("4. Kembali ke Menu Utama");

                int pilihan = Bantuan.DapatkanInputInt("Pilihan: ", 1, 4);
                switch (pilihan)
                {
                    case 1: TambahProduk(); break;
                    case 2: PerbaruiProduk(); break;
                    case 3: HapusProduk(); break;
                    case 4: keluar = true; break;
                }
                if (!keluar) Bantuan.TekanEnterUntukLanjut();
            }
        }
    }

    public class ViewKeranjangBelanja
    {
        private readonly LayananKeranjangBelanja _layananKeranjang;
        private readonly LayananProduk _layananProduk;

        public ViewKeranjangBelanja(LayananKeranjangBelanja layananKeranjang, LayananProduk layananProduk)
        {
            _layananKeranjang = layananKeranjang;
            _layananProduk = layananProduk;
        }

        public void TampilkanKeranjang()
        {
            Console.Clear();
            Bantuan.TampilkanHeader("Keranjang Belanja");
            var items = _layananKeranjang.Item; // Pastikan properti Items sudah disediakan di LayananKeranjangBelanja
            if (items.Count == 0)
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                Console.WriteLine($"{i + 1}. {item.Produk.DapatkanInfoTampilan()} | Jumlah: {item.Jumlah}");
                Console.WriteLine($"   Ongkos kirim per item: {item.Produk.HitungOngkosKirim():C}");
            }
            Console.WriteLine($"\nTotal Produk: {_layananKeranjang.HitungTotalProduk():C}");
            Console.WriteLine($"Total Ongkos Kirim: {_layananKeranjang.HitungTotalOngkosKirim():C}");
            Console.WriteLine($"Grand Total: {_layananKeranjang.HitungGrandTotal():C}");
        }

        public void TambahKeKeranjang()
        {
            Console.Clear();
            var antarmukaProduk = new ViewProduk(_layananProduk);
            antarmukaProduk.TampilkanProduk();
            if (_layananProduk.JumlahProduk == 0) return;

            int index = Bantuan.DapatkanInputInt("Masukkan nomor produk untuk ditambahkan: ", 1, _layananProduk.JumlahProduk) - 1;
            Produk produk = _layananProduk.AmbilProduk(index);
            if (produk == null)
            {
                Bantuan.TampilkanError("Produk tidak valid.");
                return;
            }
            int jumlah = Bantuan.DapatkanInputInt("Masukkan jumlah: ", 1);
            _layananKeranjang.TambahAtauPerbaruiItem(produk, jumlah);
            Bantuan.TampilkanSukses("Produk berhasil ditambahkan ke keranjang.");
        }

        public void PerbaruiItemKeranjang()
        {
            TampilkanKeranjang();
            if (_layananKeranjang.JumlahItem == 0) return;

            int index = Bantuan.DapatkanInputInt("Masukkan nomor item untuk diperbarui: ", 1, _layananKeranjang.JumlahItem) - 1;
            int jumlah = Bantuan.DapatkanInputInt("Masukkan jumlah baru: ", 1);
            if (_layananKeranjang.PerbaruiJumlahItem(index, jumlah))
                Bantuan.TampilkanSukses("Jumlah item berhasil diperbarui.");
            else
                Bantuan.TampilkanError("Gagal memperbarui item.");
        }

        public void HapusDariKeranjang()
        {
            TampilkanKeranjang();
            if (_layananKeranjang.JumlahItem == 0) return;

            int index = Bantuan.DapatkanInputInt("Masukkan nomor item untuk dihapus (0 untuk batal): ", 0, _layananKeranjang.JumlahItem);
            if (index == 0)
            {
                Bantuan.TampilkanSukses("Penghapusan dibatalkan.");
                return;
            }

            index -= 1;
            if (_layananKeranjang.HapusItem(index))
                Bantuan.TampilkanSukses("Item berhasil dihapus.");
            else
                Bantuan.TampilkanError("Gagal menghapus item.");
        }


        public void Checkout()
        {
            TampilkanKeranjang();
            if (_layananKeranjang.JumlahItem == 0)
            {
                Bantuan.TampilkanError("Keranjang kosong, tidak bisa checkout.");
                return;
            }
            decimal total = _layananKeranjang.HitungGrandTotal();
            Console.WriteLine($"Total pembayaran: {total:C}");
            decimal bayar = Bantuan.DapatkanInputDecimal("Masukkan jumlah pembayaran: ", 0);
            if (bayar >= total)
            {
                decimal kembalian = bayar - total;
                Bantuan.TampilkanSukses($"Pembayaran berhasil! Kembalian: {kembalian:C}");
                _layananKeranjang.Bersihkan();
            }
            else
            {
                Bantuan.TampilkanError("Pembayaran tidak mencukupi.");
            }
        }

        public void TampilkanMenuKeranjang()
        {
            bool keluar = false;
            while (!keluar)
            {
                TampilkanKeranjang();
                Console.WriteLine("\n=== Menu Keranjang ===");
                Console.WriteLine("1. Tambah Produk ke Keranjang");
                Console.WriteLine("2. Perbarui Jumlah Produk");
                Console.WriteLine("3. Hapus Produk dari Keranjang");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Kembali ke Menu Utama");

                int pilihan = Bantuan.DapatkanInputInt("Pilihan: ", 1, 5);
                switch (pilihan)
                {
                    case 1: TambahKeKeranjang(); break;
                    case 2: PerbaruiItemKeranjang(); break;
                    case 3: HapusDariKeranjang(); break;
                    case 4: Checkout(); break;
                    case 5: keluar = true; break;
                }
                if (!keluar) Bantuan.TekanEnterUntukLanjut();
            }
        }
    }

    public class ViewPengaturan
    {
        public void TampilkanMenuPengaturan()
        {
            bool keluar = false;
            while (!keluar)
            {
                Console.Clear();
                Bantuan.TampilkanHeader("Pengaturan");
                Console.WriteLine($"1. Ubah Ongkos Kirim Tetap Pakaian (Saat ini: {Pengaturan.BiayaKirimPakaian:C})");
                Console.WriteLine($"2. Ubah Ongkos Kirim Tetap Buku (Saat ini: {Pengaturan.BiayaKirimBuku:C})");
                Console.WriteLine($"3. Ubah Ongkos Kirim per Kg Elektronik (Saat ini: {Pengaturan.BiayaKirimElektronik:C})");
                Console.WriteLine("4. Kembali ke Menu Utama");

                int pilihan = Bantuan.DapatkanInputInt("Pilihan: ", 1, 4);
                switch (pilihan)
                {
                    case 1:
                        Pengaturan.BiayaKirimPakaian = Bantuan.DapatkanInputDecimal("Masukkan ongkos kirim baru untuk pakaian: ", 0);
                        Bantuan.TampilkanSukses("Ongkos kirim pakaian diperbarui.");
                        break;
                    case 2:
                        Pengaturan.BiayaKirimBuku = Bantuan.DapatkanInputDecimal("Masukkan ongkos kirim baru untuk buku: ", 0);
                        Bantuan.TampilkanSukses("Ongkos kirim buku diperbarui.");
                        break;
                    case 3:
                        Pengaturan.BiayaKirimElektronik = Bantuan.DapatkanInputDecimal("Masukkan ongkos kirim per Kg baru untuk elektronik: ", 0);
                        Bantuan.TampilkanSukses("Ongkos kirim elektronik diperbarui.");
                        break;
                    case 4:
                        keluar = true;
                        break;
                }
                if (!keluar) Bantuan.TekanEnterUntukLanjut();
            }
        }
    }
}
