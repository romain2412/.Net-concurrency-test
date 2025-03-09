using System;
using System.Windows.Forms;

namespace WinFormsApp
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
            btnClickMe.Click += (sender, e) => MessageBox.Show("Bonjour, tu as cliqué sur le bouton !");

            // Ajout du bouton à la fenêtre
            this.Controls.Add(btnClickMe);
        }
    }
}
