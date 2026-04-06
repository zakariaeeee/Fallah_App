namespace Fallah_App.Models
{
    public class Sol
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Couleur { get; set; }
        public double Acidite { get; set; }
        public List<Terre> terres { get; set; }

    }
}
