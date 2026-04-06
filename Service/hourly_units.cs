namespace Fallah_App.Service
{
    public class hourly_units
    {
        public string time {get;set;}
        public string temperature_2m { get; set; }
        public string relativehumidity_2m { get; set; }
        public string dewpoint_2m { get; set; }
        public string precipitation_probability { get; set; }
        public string rain { get; set; }
        public string snowfall { get; set; }
        public string weathercode { get; set; }
        public string cloudcover { get; set; }
        public string windspeed_10m { get; set; }
        public string windspeed_80m { get; set; }
        
        public string windspeed_180m { get; set; }
        public string winddirection_10m { get; set; }
        public string winddirection_80m { get; set; }
        public string windgusts_10m { get; set; }
        public string temperature_80m { get; set; }
        public string temperature_120m { get; set; }
        public string soil_temperature_0cm { get; set; }
        public string soil_temperature_6cm { get; set; }
        public string soil_temperature_18cm { get; set; }
        public string soil_moisture_0_1cm { get; set; }
        public string soil_moisture_1_3cm { get; set; }
        public string soil_moisture_3_9cm { get; set; }
        public string soil_moisture_9_27cm { get; set; }
    }
}
