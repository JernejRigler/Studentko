using Studentko.Models;
using Microsoft.AspNetCore.Mvc;

namespace Studentko.Controllers;

public class StudentController: Controller{
    [HttpPost]
    public IActionResult Login(string username, string pwd){
              System.Console.WriteLine("halo tukaj nek bakcend naredi");
              return Content("Logiral si se, sam nimamo baze :(");
    }
    public IActionResult Register(string username, string pwd, string email){
        System.Console.WriteLine(username);
        return Content($"Račun {username} je bil uspešno narejen");       
    }    
}