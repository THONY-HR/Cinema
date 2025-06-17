using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using ClassTable;
namespace Cinema.Controllers;
using Newtonsoft.Json;
public class RequeteController : Controller
{
    private readonly ILogger<RequeteController> _logger;

    public RequeteController(ILogger<RequeteController> logger)
    {
        _logger = logger;
    }

    public IActionResult Authenticate(string email, string password)
    {
        // Affichage des valeurs dans le terminal
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Password: {password}");

        Utilisateur user = new Utilisateur(email, password);
        if (user.Login() != null)
        {
            Utilisateur newUser = user.Login();
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(newUser));
            return RedirectToAction("Accueil", "Home");
        }
        else
        {
            TempData["Message"] = "Email ou Password Incorrect";
            return RedirectToAction("Login", "Home");
        }
    }

    public IActionResult Register(string fullName, string email, string password, DateTime birthdate)
    {
        // Affichage des valeurs dans le terminal
        Console.WriteLine($"fullName: {fullName}");
        Console.WriteLine($"email: {email}");
        Console.WriteLine($"password: {password}");
        Console.WriteLine($"birthdate: {birthdate}");

        Utilisateur user = new(fullName,email,password,birthdate);
        user.SetNomTable("Utilisateur");
        if(user.Inserer() ){
            return RedirectToAction("Login","Home");
        }else{
            TempData["Message"] = "Duplicate Email:" + email + " ou Autre";
            return RedirectToAction("SignIn","Home");
        }
    }
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Home");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
