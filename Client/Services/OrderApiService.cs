using Newtonsoft.Json.Linq;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class OrderService
    {
        const string PORT = "7051";
        const string HOST = $"https://localhost:{PORT}";
        public static async Task<String> GetOrdersAsync()
        {
            const string url = $"{HOST}/api/Orders/GetOrders";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(url))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            return data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "[]";
            }
        }

        public static async Task<String> GetOrderByIdAsync(int id)
        {
            const string url = $"{HOST}/api/Orders/";
            
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(url+id))
                    {
                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            return data;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "[]";
            }
        }

        public static async Task<String> CreateOrderAsync(string jsonData)
        {
            const string url = $"{HOST}/api/Orders";


            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error Details: " + errorContent);
                    return "{}";
                }
            }

        }

        public static async Task<String> UpdateOrderAsync(string id,string jsonData)
        {
            const string url = $"{HOST}/api/Orders/";


            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url+id, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error Details: " + errorContent);
                    return "{}";
                }
            }

        }

        public static async void DeleteOrderAsync(string id)
        {
            const string url = $"{HOST}/api/Orders/";


            using (HttpClient client = new HttpClient())
            {

                try
                {
                    HttpResponseMessage response = await client.DeleteAsync(url + id);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Error Details: " + errorContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }
    }
}
