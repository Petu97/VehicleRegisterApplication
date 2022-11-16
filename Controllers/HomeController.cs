using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VehicleRegisterApplication.Models;

namespace VehicleRegisterApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    //Todo: controller for home page. users should be able to navigate to vehicles or owners from this page. Vehicles and owners have their own controllers. 
    //by default users should be directed to this page


    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
