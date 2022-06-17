using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoiWeb.Models;

namespace RoiWeb.Controllers;

public class HomeController : Controller
{
    public static bool LoggedIn = false;
    public static bool LoggedInAdmin = false;

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(UserModel userExist)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = userExist;

        return View();
    }
    
    public IActionResult Privacy(UserModel userExist)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = userExist;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult SignUpPost(UserModel user)
    {
        using(var db = new UserContext())
        {
            var checkuser = db.Users.Where(x => x.Username == user.Username).FirstOrDefault();
            
            if(checkuser == null)
            {
                if(user != null && ModelState.IsValid){

                    db.Add(user);
                    db.SaveChanges();
                    LoggedIn = true;
                    return RedirectToAction("Index", new { userExist = user });
                }
            }
            ViewBag.Error = "Username already exists";
            return View("SignUp");
        }
            
    }

    public IActionResult SignUp()
    {
        ViewBag.LoggedIn = LoggedIn = false;
        ViewBag.LoggedInAdmin = LoggedInAdmin = false;

        return View();
    }

    public IActionResult Manage(UserModel user)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = user;

        List<UserModel> users = new List<UserModel>();
        using(var db = new UserContext())
        {
            users = db.Users.ToList();
        }

        ViewBag.Users = users;
        return View();
    }

    [HttpPost]
    public IActionResult deleteUser(UserModel user)
    {
        //db Users Delete(user)
        
        using(var db = new UserContext())
        {
            var deleteUser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();

            if(deleteUser != null)
            {
                db.Remove(deleteUser);
                db.SaveChanges();
            }
        }
        return RedirectToAction("Manage");
    }

    [HttpPost]
    public IActionResult UpdateUserManager(UserModel user)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = user;

        using(var db = new UserContext()) {
            var userEdit = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            TempData["userEdit"] = userEdit;
        }
        return View();
    }     
    
    public IActionResult UpdateUserManagerForm(UserModel user)
    {
        using(var db = new UserContext()) {
            
            var Edituser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();

            if(Edituser != null) {
                Edituser.Username = user.Username;
                Edituser.Password = user.Password;
                Edituser.Email = user.Email;
                db.SaveChanges();
            }
            
        }
        return RedirectToAction("Manage");
    
    }

    public IActionResult UpdateUser(UserModel user)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = user;

        using(var db = new UserContext()) {
            var userEdit = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            TempData["userEdit"] = userEdit;
        }
        return View();
    }     
    
  
    public IActionResult UpdateUserForm(UserModel user)
    {
        using(var db = new UserContext()) {
            
            var Edituser = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            
            if(Edituser != null) {
                Edituser.Username = user.Username;
                Edituser.Password = user.Password;
                Edituser.Email = user.Email;
                db.SaveChanges();
            }
            
        }
        return RedirectToAction("Manage");
    
    }

    public IActionResult LogIn()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult LoginGet(UserModel user)
    {
        using(var db = new UserContext()){
            var userExist = db.Users.Where(x => x.Username == user.Username  && x.Password == user.Password).FirstOrDefault();

            if(userExist != null)
            {
                if(userExist.Id == 2){
                    LoggedIn = true;
                    LoggedInAdmin =true;
                    return RedirectToAction("Index", new { userExist = userExist });
                }
                LoggedIn = true;
                return RedirectToAction("Index", new { userExist = userExist });
            }
            else
            {
                LoggedIn = false;
                ViewBag.Error = "Username or Password are incorrect";
                return RedirectToAction("LogIn");
            }
        }
    }

    public IActionResult LogOut()
    {
        LoggedIn = false;
        LoggedInAdmin = false;
        return RedirectToAction("Index");
    }
    
    public IActionResult gallery(UserModel user)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = user;

        return View();
    }

    public IActionResult UpdateUserCreateUser(UserModel user)
    {
        ViewBag.LoggedIn = LoggedIn;
        ViewBag.LoggedInAdmin = LoggedInAdmin;
        ViewBag.user = user;

        using(var db = new UserContext()) {
            var userEdit = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
        }

        return RedirectToAction("UpdateUser");
    }

    public IActionResult PromoteUser(UserModel user)
    {
       user.Role = "Admin";

             return View();

  }

    public IActionResult DemoteUser(UserModel user)
    {
        user.Role = "User";

        return View();

    }

    public IActionResult checkAdmin(UserModel user)
    {
        if(user.Role == "Admin")
        {
            return RedirectToAction("PromoteUser");
        }
        else
        {
            return RedirectToAction("DemoteUser");
        }

    }

    



