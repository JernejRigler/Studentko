using Studentko.Models;
using Microsoft.AspNetCore.Mvc;

namespace Studentko.Controllers;

public class StudentController: Controller{
    [HttpPost]
    public IActionResult Login(string usr, string pwd){
              System.Console.WriteLine("halo tukaj nek bakcend naredi");
              return Content("Logiral si se, sam nimamo baze :(");
    }

}