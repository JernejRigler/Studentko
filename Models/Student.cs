
namespace Studentko.Models
{
    public class Student
    {
        public int ID {get; set; }
        public required string Username {get; set; }
        //tip računa, ki ga bo imel student (ali je admin ali je navadni uporabnik)
        public required string Account_type {get; set;}
        public required string Password {get; set;}

    }
}