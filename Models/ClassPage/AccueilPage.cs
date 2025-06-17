using Function;
using ClassTable;
using ClassView;

namespace ClassPage
{
    public class AccueilPage: Fonction
    {
        public AccueilPage() { }
        public FilmDetails[] GetFilmDetails(){
            try
            {
                this.SetNomTable("FilmDetails");
                FilmDetails[] filmDetails = this.GetObject<FilmDetails>();
                return filmDetails;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public LesDiffusions[] GetDiffusions(){
            try
            {   
                this.SetNomTable("LesDiffusions");
                LesDiffusions[] diffusion = this.GetObject<LesDiffusions>();
                return diffusion;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public Classification[] GetClassifications(){
            try
            {   
                this.SetNomTable("Classification");
                Classification[] classification = this.GetObject<Classification>();
                return classification;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public Categorie[] GetCategorie(){
            try
            {
                this.SetNomTable("Categorie");
                Categorie[] categories = this.GetObject<Categorie>();
                return categories;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public Salle[] GetSalle(){
            try
            {
                this.SetNomTable("Salle");
                Salle[] salles = this.GetObject<Salle>();
                return salles;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public MethodePayement[] GetMethodePayements(){
            try
            {
                this.SetNomTable("MethodePayement");
                MethodePayement[] methodePayements = this.GetObject<MethodePayement>();
                return methodePayements;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public ReservationDetail[] GetReservationById(int idUser){
            try
            {
                this.SetNomTable("ReservationDetails WHERE idUser="+idUser+" ORDER BY idStatut");
                ReservationDetail[] reservationDetails = this.GetObject<ReservationDetail>();
                return reservationDetails;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public void Payer(int idReservation,int idMethodePayement){
            try
            {
                Reservation reservation = new Reservation();
                reservation.SetNomTable("Reservation WHERE idReservation="+idReservation);
                Reservation[] reservations = reservation.GetObject<Reservation>();
                reservation.SetNomTable("Diffusion WHERE idDiffusion="+reservations[0].IdDiffusion);
                Diffusion[] diff = reservation.GetObject<Diffusion>();
                Payement payement = new Payement(diff[0].IdDiffusion,reservations[0].IdReservation, idMethodePayement, diff[0].Paf);
                Reservation aUpdater = reservations[0];
                aUpdater.IdStatut = 3;
                // Console.WriteLine("IdReservation:"+aUpdater.IdReservation);
                // Console.WriteLine("IdUser:"+aUpdater.IdUser);
                // Console.WriteLine("IdDiffusion:"+aUpdater.IdDiffusion);
                // Console.WriteLine("IdDetailSalle:"+aUpdater.IdDetailSalle);
                // Console.WriteLine("IdStatut:"+aUpdater.IdStatut);
                // Console.WriteLine("DatyReservation:"+aUpdater.DatyReservation);

                aUpdater.SetNomTable("Reservation");
                aUpdater.SetKey("idReservation");
                payement.SetNomTable("Payement");
                payement.Inserer();
                Console.WriteLine("DatyReservation:"+aUpdater.GetKey());
                aUpdater.Update();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Erreur lors du Payement");
            }
        }
        public void Annuler(int idReservation){
            try
            {
                Reservation reservation = new Reservation();
                reservation.SetNomTable("Reservation WHERE idReservation="+idReservation);
                Reservation[] reservations = reservation.GetObject<Reservation>();
                Reservation aUpdater = reservations[0];
                aUpdater.IdStatut = 4;

                aUpdater.SetNomTable("Reservation");
                aUpdater.SetKey("idReservation");
                
                Console.WriteLine("DatyReservation:"+aUpdater.GetKey());
                aUpdater.Update();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Erreur lors du Payement");
            }
        }

        public PlacesDiffusion[] PlacesDiffusion(int idDiffusion){
            try
            {
                PlacesDiffusion placesDiffusion = new PlacesDiffusion();
                placesDiffusion.SetNomTable("Vue_Places_Diffusion WHERE idDiffusion="+idDiffusion+" ORDER BY colonne");
                return placesDiffusion.GetObject<PlacesDiffusion>();
            }
            catch (System.Exception)
            {
                Console.WriteLine("Erreur lors du Payement");
                throw;
            }
        }
    }
}
