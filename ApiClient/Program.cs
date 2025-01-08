using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7291/api/");

            while (true)
            {
                Console.WriteLine("API Test Client Başladı!");
                Console.WriteLine("1. Öğrencileri Listele");
                Console.WriteLine("2. Yeni Öğrenci Ekle");
                Console.WriteLine("3. Notları Listele");
                Console.WriteLine("Çıkış için 'q' tuşlayın.");
                Console.Write("Bir seçim yapın: ");

                string choice = Console.ReadLine();

                if (choice == "q")
                {
                    Console.WriteLine("Programdan çıkılıyor...");
                    break; // Döngüden çıkar
                }

                switch (choice)
                {
                    case "1":
                        await GetStudentsAsync(client);
                        break;
                    case "2":
                        await AddStudentAsync(client);
                        break;
                    case "3":
                        await GetGradesAsync(client);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }
    }

    static async Task GetStudentsAsync(HttpClient client)
    {
        HttpResponseMessage response = await client.GetAsync("students");
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        else
        {
            Console.WriteLine($"Hata: {response.StatusCode}");
        }
    }

    static async Task AddStudentAsync(HttpClient client)
    {
        var student = new
        {
            Name = "Ahmet",
            Role = "Kokpit"
        };

        HttpResponseMessage response = await client.PostAsJsonAsync("students", student);
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Yeni öğrenci eklendi: {responseBody}");
        }
        else
        {
            Console.WriteLine($"Hata: {response.StatusCode}");
        }
    }

    static async Task GetGradesAsync(HttpClient client)
    {
        HttpResponseMessage response = await client.GetAsync("grades");
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        else
        {
            Console.WriteLine($"Hata: {response.StatusCode}");
        }
    }
}
