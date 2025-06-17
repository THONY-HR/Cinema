using Function;
using ClassTable;
using ClassView;

namespace ClassPage
{
    public class GestionPlace
    {
        public int Colonne { get; private set; }
        public int[] Hauteur { get; private set; }
        public int[] PlacesParLigne { get; private set; }
        public string IdSalle { get; private set; }
        public PlacesDiffusion[] PlacesDiffusions { get; private set; }

        public GestionPlace(){}
        public GestionPlace(PlacesDiffusion[] placesDiffusions)
        {
            if (placesDiffusions == null)
            {
                throw new ArgumentNullException(nameof(placesDiffusions));
            }

            this.PlacesDiffusions = placesDiffusions;
            this.IdSalle = this.PlacesDiffusions[0].IdSalle;
            this.setColHautPlace();
        }
        public void setColHautPlace(){
            ConfigurationColonnes config = new ConfigurationColonnes();
            config.SetNomTable("ConfigurationColonnes WHERE IdSalle = '" + this.IdSalle + "'");
            ConfigurationColonnes[] configs = config.GetObject<ConfigurationColonnes>();
            this.Colonne = configs.Length;
            this.Hauteur = new int[Colonne];
            this.PlacesParLigne = new int[Colonne];
            for (int i = 0; i < configs.Length; i++)
            {
                this.Hauteur[i] = configs[i].Hauteur;
                this.PlacesParLigne[i] = configs[i].PlacesParLigne;
            }
        }
    }
}