using System;
using Function;

namespace ClassTable
{
    public class Utilisateur : Fonction
    {
        public int IdUser { get; set; } // Correspond à la colonne idUser
        public string Nom { get; set; } // Correspond à la colonne nom
        public string Email { get; set; } // Correspond à la colonne email
        public string Pwd { get; set; }
        public DateTime Dtn { get; set; } // Correspond à la colonne dtn (Date de naissance)

        // Constructeur avec paramètres
        public Utilisateur(int idUser, string nom, string email,string pwd, DateTime dtn)
        {
            IdUser = idUser;
            Nom = nom;
            Email = email;
            Pwd = pwd;
            Dtn = dtn;
        }

        public Utilisateur(string nom, string email,string pwd, DateTime dtn)
        {
            Nom = nom;
            Email = email;
            Pwd = pwd;
            Dtn = dtn;
        }
        public Utilisateur(string email,string pwd)
        {
            Email = email;
            Pwd = pwd;
        }
        // Constructeur sans paramètres
        public Utilisateur() { }

        public Utilisateur Login()
        {
            try
            {
                this.SetNomTable("Utilisateur");
                Utilisateur[] users = this.GetObject<Utilisateur>();
                foreach (Utilisateur user in users)
                {
                    if (user.Email == this.Email && user.Pwd == this.Pwd)
                    {
                        return user;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la tentative de connexion : " + ex.Message);
                return null;
            }
        }

    }
}
