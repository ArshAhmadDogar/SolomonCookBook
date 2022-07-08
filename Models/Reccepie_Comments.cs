using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolomonCookBook.Models{
    public class Recepie_Comments{
        [Key]
        public int R_Comment_ID{get;set;}

        public User? User {get;set;}
        public Recepies Recepie{get;set;}
         
        public DateTime Date{get;set;} 

        public string Comment{get;set;}

    }
}