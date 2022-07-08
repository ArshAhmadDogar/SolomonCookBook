using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SolomonCookBook.Models;
using SolomonCookBook.Data;

namespace SolomonCookBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IWebHostEnvironment env;
    private TheDbContext db;

    public HomeController(ILogger<HomeController> logger, TheDbContext context, IWebHostEnvironment environment)
    {
        _logger = logger;
        db = context;
        env = environment;
    }

    public IActionResult Index(string data)
    {
        @ViewBag.Name = data;
        return View();
    }


    // SignIn
    [HttpGet]
    public ViewResult SignIn()
    {
        return View();
    }

    [HttpPost]
    public ActionResult SignIn(User usr)
    {
        var obj = db.Users.Single(user => user.Email == usr.Email && user.Password == usr.Password); 
        if (obj != null)
        {
            return RedirectToAction("MainPage");
        }else{
            return View();
        }
        
    }

    public ViewResult MainPage()
    {
        return View();
    }

    public ViewResult Recepies()
    {
        var data = db.Recepies.ToList();
        return View(data);
    }


    [HttpGet]
    public ViewResult Signup()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Signup(User usr)
    {   
        var data = usr;
        db.Users.Add(usr);
        db.SaveChanges();
        var msg = "User Created Succeddfully !";
        return RedirectToAction("test","Home",new {message = msg });
    }

    [HttpGet]
    public ViewResult test(string message,string email, string Name)
    {
        
        ViewBag.email = email;
        ViewBag.name = Name;
        return View();
    }

    [HttpPost]
    public IActionResult test(Image img)
    {
        string a = img.Img.FileName;
        return RedirectToAction("test", new{Name = a });
    }

    [HttpGet]
    public ViewResult CreateRecepie()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateRecepie(RecepieImg model)
    {
        var path =  env.WebRootPath;
        var filepath = "Images/"+ model.MyImage.Img.FileName;
        var fullpath = Path.Combine(path,filepath);
        UploadImage(model.MyImage.Img,fullpath);
        string imgPath = filepath; 
        var data = new Recepies{
            Recepie_Name = model.MyRecepie.Recepie_Name,
            Category = model.MyRecepie.Category,
            video_url = model.MyRecepie.video_url,
            image_url = imgPath,
            Ingredients = model.MyRecepie.Ingredients,
            Comments = model.MyRecepie.Comments

        };

        db.Recepies.Add(data);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    public void UploadImage(IFormFile file, string path)
    {
        FileStream stream =  new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
    }
   
}
