using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolomonCookBook.Models{
    public class Admin
    {
        [Key]
        public int AdminId {get;set;}

        public string Name{get;set;}
        public string Password{get;set;}
    }

}