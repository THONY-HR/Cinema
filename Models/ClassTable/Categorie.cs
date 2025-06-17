using System;
using Function;
namespace ClassTable
{
    public class Categorie: Fonction
    {
        public int IdCategorie { get; set; } // Correspond à la colonne idCategorie (clé primaire)
        public string TypeCategorie { get; set; } // Correspond à la colonne typeCategorie

        // Constructeur avec paramètres
        public Categorie(int idCategorie, string typeCategorie)
        {
            IdCategorie = idCategorie;
            TypeCategorie = typeCategorie;
        }

        // Constructeur sans paramètres
        public Categorie() { }
    }
}
