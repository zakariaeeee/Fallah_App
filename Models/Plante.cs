using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fallah_App.Models
{
    public class Plante
    {        
        public int ?  Id { get; set; }
        [Required (ErrorMessage="ce champ est obligatoire")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public string type { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public int Debut_Period { get; set; }
        [Required(ErrorMessage = "ce champ est obligatoire")]
        public int Fin_Date { get; set; }
       
        public List<Notification> ? notifications { get; set; }
        
        public List<Terre> ? terres { get; set; }
         
        public List<ConseilPlante> ? conseilPlantes { get; set; }
        
    }
}
