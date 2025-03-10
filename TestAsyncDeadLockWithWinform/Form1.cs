using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TestAsyncDeadLockWithWinform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Création d'un bouton
            Button btnClickMe = new Button
            {
                Text = "Clique-moi",
                Width = 100,
                Height = 40,
                Top = 50,
                Left = 50
            };

            // Ajout d'un événement au clic du bouton
            btnClickMe.Click += OnClick;

            // Ajout du bouton à la fenêtre
            this.Controls.Add(btnClickMe);
        }
        
        public void OnClick(object sender, EventArgs e)
        {
            Console.WriteLine($"thread dans OnClick avant launching ChuckNorrisAsync: {Thread.CurrentThread.ManagedThreadId}");
            var ChuckNorrisTask = ChuckNorrisAsync();
            Console.WriteLine($"thread dans OnClick, waiting ChuckNorrisAsync to be completed: {Thread.CurrentThread.ManagedThreadId}");
            var joke = ChuckNorrisTask.Result;
            Console.WriteLine($"thread dans OnClick, after ChuckNorrisAsync completed: {Thread.CurrentThread.ManagedThreadId}");
        }

        public async void OnClickAsync(object sender, EventArgs e)
        {
            Console.WriteLine($"thread dans OnClickAsync avant launching ChuckNorrisAsync: {Thread.CurrentThread.ManagedThreadId}");
            await ChuckNorrisAsync();
            Console.WriteLine($"thread dans OnClickAsync after ChuckNorrisAsync completed: {Thread.CurrentThread.ManagedThreadId}");
        }
    
        public async Task<string> ChuckNorrisAsync()
        {
            Console.WriteLine($"thread dans ChuckNorrisAsync avant premier await: {Thread.CurrentThread.ManagedThreadId}");
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
                        var jsonResponse = await response.Content.ReadAsStringAsync();
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
            Console.WriteLine($"thread dans ChuckNorrisAsync avant pop up: {Thread.CurrentThread.ManagedThreadId}");
            MessageBox.Show(joke);
            return joke;
        }
    }

}

