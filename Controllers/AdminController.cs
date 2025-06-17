using Microsoft.AspNetCore.Mvc;  
using ClassPage;
using ClassTable;
namespace Cinema.Controllers;

public class AdminController : Controller
{
    AdminPage pageAdmin = new AdminPage();
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }
    public IActionResult Admin()
    {
        var categorie = pageAdmin.accueilPage.GetCategorie();
        var classification = pageAdmin.accueilPage.GetClassifications();
        var reservationParJour = pageAdmin.GetReservationsParJour();
        var salle = pageAdmin.accueilPage.GetSalle();
        var film = pageAdmin.GetFilm();
        var filmBenefice = pageAdmin.GetFilmBenefices();

        ViewBag.FilmBenefices = filmBenefice;
        ViewBag.Films = film;
        ViewBag.Salles = salle;
        ViewBag.Categories = categorie;
        ViewBag.Classifications = classification;
        ViewBag.ReservationParJours = reservationParJour;

        return View();
    }
    public IActionResult AjouterFilm(string titre, int idClassification, int idCategorie,string synopsis, DateTime duree,double prixDiffusion)
    {
        // Affichage des valeurs dans le terminal
        // Console.WriteLine($"titre: {titre}");
        // Console.WriteLine($"idClassification: {idClassification}");
        // Console.WriteLine($"idCategorie: {idCategorie}");
        // Console.WriteLine($"synopsis: {synopsis}");
        // Console.WriteLine($"duree: {duree.TimeOfDay}");

        Film film = new Film(titre, idClassification, idCategorie, synopsis, duree.TimeOfDay,prixDiffusion);
            film.SetNomTable("Film");
            film.Inserer();
        return RedirectToAction("Admin"); 
    }

    public IActionResult AjouterSalle(string nomSalle, DateTime ouverture, DateTime fermeture)
    {
        // Affichage des valeurs dans le terminal
        // Console.WriteLine($"titre: {nomSalle}");
        // Console.WriteLine($"idClassification: {ouverture}");
        // Console.WriteLine($"idCategorie: {fermeture}");

        Salle salle = new Salle(nomSalle, ouverture.TimeOfDay, fermeture.TimeOfDay);
            salle.SetNomTable("Salle");
            salle.Inserer();
        return RedirectToAction("Admin"); 
    }

    public IActionResult ConfigurerSalle(string idSalle, int colonne, int hauteur, int placesParLigne)
    {
        // Affichage des valeurs dans le terminal
        // Console.WriteLine($"titre: {idSalle}");
        // Console.WriteLine($"idClassification: {colonne}");
        // Console.WriteLine($"idCategorie: {hauteur}");
        // Console.WriteLine($"idCategorie: {placesParLigne}");
        
        ConfigurationColonnes config = new ConfigurationColonnes(idSalle, colonne, hauteur, placesParLigne);
            config.DeleteAndGenerateSieges(idSalle,config);
        return RedirectToAction("Admin"); 
    }

    public IActionResult DiffuserFilm(string idSalle, int idFilm, DateTime heureDebut, double paf, DateTime daty)
    {
        
        // Console.WriteLine($"titre: idSalle");
        // Console.WriteLine($"titre: {idFilm}");
        // Console.WriteLine($"idClassification: {heureDebut}");
        // Console.WriteLine($"idCategorie: {paf}");
        // Console.WriteLine($"idCategorie: {daty}");
        
        Diffusion diffusion = new Diffusion(idSalle, idFilm, heureDebut.TimeOfDay, paf,daty);
            diffusion.SetNomTable("Diffusion");
            diffusion.Inserer();
        return RedirectToAction("Admin"); 
    }
    
}