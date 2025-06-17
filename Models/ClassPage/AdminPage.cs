using Function;
using ClassTable;
using ClassView;

namespace ClassPage
{
    public class AdminPage: Fonction
    {
        public AccueilPage accueilPage = new AccueilPage();
        public AdminPage() { }
        public ReservationsParJour[] GetReservationsParJour(){
            try
            {
                this.SetNomTable("ReservationsParJour");
                ReservationsParJour[] reservationsParJour = this.GetObject<ReservationsParJour>();
                return reservationsParJour;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Film[] GetFilm(){
            try
            {
                this.SetNomTable("Film");
                Film[] Film = this.GetObject<Film>();
                return Film;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public FilmBenefices[] GetFilmBenefices(){
            try
            {
                this.SetNomTable("FilmBenefices");
                FilmBenefices[] FilmBenefices = this.GetObject<FilmBenefices>();
                return FilmBenefices;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
