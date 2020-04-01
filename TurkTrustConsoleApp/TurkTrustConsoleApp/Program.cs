using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace TurkTrustConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(" 1950-1965 yaş arasındakiler\n");
            YasaGoreSirali();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("İhracat yapilan ülkeler.\n");
            IhracatYapilanUlkeler();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("Almanyadaki Müşteri sayisi\n");
            AlmanyadakiMusteri();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("L ile başlıyan fiyati en yüksek ürün\n");
            FiyatiYuksekLileBasliyan();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("Ürün ve Tedarikçi Adi.\n");
            UrunVeTedarikci();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("Kimin kime bağlı çalıştığı.\n");
            KimKimeBagli();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("Son 10 satis.\n");
            Son10Satis();
            Console.WriteLine("--------------------\n");

            Console.WriteLine("%5 indirim.\n");
            Indirim();
            Console.WriteLine("--------------------\n");
        }

        private static void Indirim()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            
            new SqlCommand(@"update Urunler set BirimFiyati = BirimFiyati-(BirimFiyati*5/100) where KategoriID = 1 and BirimFiyati >15", conn);

            Console.WriteLine(" İndirim yapılmıştır. ");
   
            conn.Close();
        }

        private static void Son10Satis()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select top 10 M.MusteriAdi as MusAdi,P.Adi as PerAdi from [dbo].[Satislar] S 
            inner join Musteriler M on M.MusteriAdi=S.MusteriID 
            inner join Personeller P on P.PersonelID=S.PersonelID order by SatisTarihi desc", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} | {1} ", reader.GetString(0), reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void KimKimeBagli()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select P1.Adi as çPerAd,P1.SoyAdi as çPerSoyad,P2.Adi as yPerAd,P2.SoyAdi as yPerSoyad from Personeller P1 inner join Personeller P2 on P1.PersonelID=P2.BagliCalistigiKisi
            ", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} | {1} | {2} | {3}", reader.GetString(0), reader.GetString(1), reader.GetString(3), reader.GetString(4));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void UrunVeTedarikci()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select U.UrunAdi,T.SirketAdi from Urunler U inner join Tedarikciler T on T.TedarikciID=U.TedarikciID", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} | {1}", reader.GetString(0), reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void FiyatiYuksekLileBasliyan()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select top 1 UrunAdi,BirimFiyati from Urunler where UrunAdi like 'L%' order by BirimFiyati desc", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} | {1}", reader.GetString(0), reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void AlmanyadakiMusteri()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"SELECT COUNT (Ulke) as total FROM musteriler WHERE Ulke like 'Germany'", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} ", reader.GetString(0));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void IhracatYapilanUlkeler()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select DISTINCT  Ulke from Musteriler", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0} ", reader.GetString(0));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }

        private static void YasaGoreSirali()
        {
            string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Samsung\source\repos\TurkTrustConsoleApp\TurkTrustConsoleApp\Database1.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlDataReader reader;
            reader = new SqlCommand(@"select Adi,SoyAdi,DogumTarihi from Personeller
            WHERE       DogumTarihi BETWEEN '1950-01-01' AND '1965-01-01' order by DogumTarihi", conn).ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(" {0}  |   {1}  |   {2}", reader.GetString(0),
                    reader.GetString(1), reader.GetString(2));
                }
            }
            else
            {
                Console.WriteLine("Bulunamadı.");
            }
            reader.Close();
            conn.Close();
        }
    }
}
