using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using Newtonsoft.Json;
using ClassTable;   
using ClassPage;
using Org.BouncyCastle.Utilities;
namespace Cinema.Controllers;

public class ReservationItem
{
    public int IdUser { get; set; }
    public int IdDiffusion { get; set; }
    public string IdDetailSalle { get; set; }
}
public class HomeController : Controller
{
    AccueilPage pageAccueil = new AccueilPage();
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Login");
    }
    public IActionResult RedirectSignIn()
    {
        return RedirectToAction("SignIn");
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult SignIn()
    {
        return View();
    }
    public IActionResult Reservation(string donner)
    {
        List<ReservationItem> reservations = JsonConvert.DeserializeObject<List<ReservationItem>>(donner);
        Reservation[] newReservationArray = new Reservation[reservations.Count];

        for (int i = 0; i < reservations.Count; i++)
        {
            Reservation reservation = new Reservation();
            reservation.SetReservation(reservations[i].IdUser, reservations[i].IdDiffusion, reservations[i].IdDetailSalle);
            newReservationArray[i] = reservation;
            Console.WriteLine(reservation.IdDetailSalle);
        }
        foreach (Reservation res in newReservationArray)
        {
            res.SetNomTable("Reservation");
            res.IdStatut = 2;
            if(res.Inserer()){
                Console.WriteLine("Reussi");
            }else{
                continue;
            }
        }
        return RedirectToAction("ListeReservation");
    }
   public IActionResult Payement(string donner, string idMethodePayement)
    {
        // Désérialisation de la chaîne JSON en une liste d'entiers
        List<int> reservations = JsonConvert.DeserializeObject<List<int>>(donner);

        // Conversion de la méthode de paiement en entier
        int idMethodePayements = int.Parse(idMethodePayement);
        // Console.WriteLine(reservations[0]);
        // Console.WriteLine(idMethodePayements);

        // // Boucle sur la liste des réservations
        for (int i = 0; i < reservations.Count; i++)
        {
            pageAccueil.Payer(reservations[i], idMethodePayements);
        }

        // Redirection après paiement
        return RedirectToAction("ListeReservation");
    }

    public IActionResult AnnulerPayement(int idReservation)
    {
        pageAccueil.Annuler(idReservation);
        return RedirectToAction("ListeReservation");
    }
    public IActionResult PlacesDiffusion(int idDiffusion)
    {
        try
        {
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonConvert.DeserializeObject<Utilisateur>(userJson);
                ViewBag.User = user;
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            GestionPlace gestionPlace = new GestionPlace(pageAccueil.PlacesDiffusion(idDiffusion));
            var placesDiffusions = gestionPlace;
            ViewBag.GestionPlace = placesDiffusions;
            return View();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
    public IActionResult DetailleFilm(int idDiffusion)
    {
        try
        {
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonConvert.DeserializeObject<Utilisateur>(userJson);
                ViewBag.User = user;
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            for(int i = 0; i<pageAccueil.GetDiffusions().Length; i++){
                if(pageAccueil.GetDiffusions()[i].IdDiffusion == idDiffusion){
                    var diffusion = pageAccueil.GetDiffusions()[i];
                    HttpContext.Session.SetString("Diffusion", JsonConvert.SerializeObject(pageAccueil.GetDiffusions()[i]));
                    ViewBag.Diffusion = diffusion;
                }
            }
            return View();
        }
        catch (System.Exception)
        {
            ViewBag.Error = "Une erreur est survenue lors de la récupération du diffusion.";
            return View();
        }
    }

    public IActionResult Accueil()
    {
        try
        {
            // Récupérer l'objet utilisateur de la session
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson))
            {
                // Si l'objet utilisateur existe, vous pouvez le désérialiser
                var user = JsonConvert.DeserializeObject<Utilisateur>(userJson);

                // Vous pouvez maintenant utiliser l'objet 'user' pour afficher des informations dans la vue
                ViewBag.User = user;
            }
            else
            {
                // Si l'utilisateur n'est pas connecté ou la session est vide, redirigez vers la page de login
                return RedirectToAction("Login", "Home");
            }

            // Récupérer les détails des films
            var diffusions = pageAccueil.GetDiffusions();
            var salles = pageAccueil.GetSalle();
            var categories = pageAccueil.GetCategorie();

            ViewBag.Diffusions = diffusions;
            ViewBag.Salles = salles;
            ViewBag.Categories = categories;

            return View();
        }
        catch (Exception ex)
        {
            // Gérer les exceptions, afficher un message d'erreur ou rediriger vers une page d'erreur
            ViewBag.Error = "Une erreur est survenue lors de la récupération des films.";
            return View();
        }
    }

    public IActionResult ListeReservation()
    {
        try
        {
            // Récupérer l'objet utilisateur de la session
            var userJson = HttpContext.Session.GetString("User");
            if (!string.IsNullOrEmpty(userJson))
            {
                // Si l'objet utilisateur existe, vous pouvez le désérialiser
                var user = JsonConvert.DeserializeObject<Utilisateur>(userJson);
                // Vous pouvez maintenant utiliser l'objet 'user' pour afficher des informations dans la vue
                ViewBag.User = user;
            }
            else
            {
                // Si l'utilisateur n'est pas connecté ou la session est vide, redirigez vers la page de login
                return RedirectToAction("Login", "Home");
            }
            var methodePayement = pageAccueil.GetMethodePayements();
            var listeReservation = pageAccueil.GetReservationById(ViewBag.User.IdUser);
            ViewBag.ListeReservations = listeReservation;
            ViewBag.ListeMethodePayement = methodePayement;
            return View();
        }
        catch (System.Exception)
        {        
            throw;
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
