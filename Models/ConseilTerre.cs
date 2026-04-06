using System.ComponentModel.DataAnnotations.Schema;

namespace Fallah_App.Models
{
    public class ConseilTerre
    {
        public int Id { get; set; }
        public string Text_Arabe { get; set; }
        public string Text_Francais { get; set; }
        public List<CategoryTerre> CategoryTerres { get; set; }
        public String?  Audio { get; set; }
        [NotMapped]
        public IFormFile  File { get; set; }
        public WebMaster? webMaster { get; set; }

        [ForeignKey(nameof(webMaster))]
        public int Id_WebMaster { get; set; }
        public List<Resultat> resultats { get; set; }

    }
}
