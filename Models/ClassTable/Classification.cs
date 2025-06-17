using System;
using Function;
namespace ClassTable
{
    public class Classification: Fonction
    {
        public int IdClassification { get; set; } // Correspond à la colonne idClassification (clé primaire)
        public int AgeClassification { get; set; } // Correspond à la colonne ageClassification

        // Constructeur avec paramètres
        public Classification(int idClassification, int ageClassification)
        {
            IdClassification = idClassification;
            AgeClassification = ageClassification;
        }

        // Constructeur sans paramètres
        public Classification() { }
    }
}
