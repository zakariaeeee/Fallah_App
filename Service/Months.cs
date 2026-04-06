namespace Fallah_App.Service
{
    public class Months
    {
        public string monthName { get; set; }
     
        public static List<Months> getMonthNames()
        {
            List<Months> monthNames = new List<Months>();
            for(int i=1;i<=12;i++)
            {        
            DateTime dateObj = new DateTime(2000, i, 1);
            string monthName = dateObj.ToString("MMMM");
            Months date = new Months();
            date.monthName = monthName;
            monthNames.Add(date);
            }
            return monthNames;
        }
    }
}
