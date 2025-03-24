using System;

namespace TokoOnline
{
    public class Program
    {
        static void Main()
        {
            // Inisialisasi layanan
            var layananProduk = new LayananProduk();
            var layananKeranjang = new LayananKeranjangBelanja();

            // Inisialisasi view pengguna
            var viewProduk = new ViewProduk(layananProduk);
            var viewKeranjang = new ViewKeranjangBelanja(layananKeranjang, layananProduk);
            var viewPengaturan = new ViewPengaturan();

            // Loop aplikasi utama
            bool keluar = false;
            while (!keluar)
            {
                Console.Clear();
                viewProduk.TampilkanProduk();

                Console.WriteLine("\n=== Menu Utama ===");
                Console.WriteLine("1. Manajemen Produk");
                Console.WriteLine("2. Keranjang Belanja");
                Console.WriteLine("3. Pengaturan");
                Console.WriteLine("4. Keluar");

                int pilihan = Bantuan.DapatkanInputInt("Pilihan: ", 1, 4);
                switch (pilihan)
                {
                    case 1:
                        viewProduk.TampilkanMenuManajemenProduk();
                        break;
                    case 2:
                        viewKeranjang.TampilkanMenuKeranjang();
                        break;
                    case 3:
                        viewPengaturan.TampilkanMenuPengaturan();
                        break;
                    case 4:
                        keluar = true;
                        break;
                }
            }

            Console.WriteLine("Terima kasih telah menggunakan aplikasi kami!");
        }
    }
}
