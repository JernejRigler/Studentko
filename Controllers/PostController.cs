using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Studentko.Controllers;

public class PostController : Controller
{
    private readonly StudentkoContext _context;
    public PostController(StudentkoContext context){
        _context = context;
    }
     [HttpGet]
    public IActionResult FormPost(){
        Console.WriteLine("Na objavi forum");
        return View();
    }
    public async Task<IActionResult> PostDetails(int id){
        var post = await _context.Posts
            .Where(p => p.PostID == id)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync();
        if(post == null){
            Console.WriteLine("Iskana objava ni bila najdena");
            return RedirectToAction("Index", "Home");
        }
        return View(post);
    }
    [HttpPost]
    public async Task<IActionResult> PublishPost(Post newPost, IFormFile postAttachment){
        if(ModelState.IsValid){
            if(postAttachment != null){
                //nek file hanadling
            }
            newPost.date = DateTime.Now;
            _context.Posts.Add(newPost);

            await _context.SaveChangesAsync();

            Console.WriteLine("objava je bila uspešno objavlena");
            return RedirectToAction("Index","Home");
        }
        //errors
        foreach (var error in ModelState)
        {
        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
        }
        return View("FormPost");
        
    }
}