using System;
using System.Threading;
using Newtonsoft.Json;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"entrée du main : {Thread.CurrentThread.ManagedThreadId}");
        var toto =  ChuckNorrisAsync();
        Console.WriteLine($"waiting end of ChuckNorrisAsync to be completed on thread : {Thread.CurrentThread.ManagedThreadId}");
        toto.Wait();
        Console.WriteLine($"sortie du main : {Thread.CurrentThread.ManagedThreadId}");
        Console.ReadLine();
    }

    public static async Task<string> ChuckNorrisAsync()
        {
            Console.WriteLine($"thread dans ChuckNorrisAsync avant premier async: {Thread.CurrentThread.ManagedThreadId}");
            string url = "https://api.chucknorris.io/jokes/random";
            var joke = "";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    Console.WriteLine($"thread dans ChuckNorrisAsync apès premier await : {Thread.CurrentThread.ManagedThreadId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Console.WriteLine($"thread dans ChuckNorrisAsync après deuxième await: {Thread.CurrentThread.ManagedThreadId}");
                        // Parse le JSON et extrait la blague
                        // Définition du modèle anonyme
                        var modele = new { value = ""};
                        Console.WriteLine($"thread dans ChuckNorrisAsync après troisième await: {Thread.CurrentThread.ManagedThreadId}");
                        // Désérialisation
                        joke = JsonConvert.DeserializeAnonymousType(jsonResponse, modele).value;
                        
                        Console.WriteLine($"Blague : {joke}");
                    }
                    else
                    {
                        Console.WriteLine($"Erreur : {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception : {ex.Message}");
                }
            }
            Console.WriteLine($"thread dans ChuckNorrisAsync before Chuck talks: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Chuck Norris said: {joke}");
            return joke;
        }
}
