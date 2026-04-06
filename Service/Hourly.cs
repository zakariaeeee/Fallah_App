namespace Fallah_App.Service
{
    public class Hourly
    {
        public List<DateTime>time { get; set; }
        public List<float> temperature_2m { get; set; }
        public List<int> relativehumidity_2m { get; set;}
        public List<float> dewpoint_2m { get; set; }

        public List<float> precipitation_probability { get; set; }
        public List<float> rain { get; set; }
        public List<float> snowfall { get; set; }

        public List<int> weathercode { get; set; }
        public List<int> cloudcover { get; set; }

        public List<float> windspeed_10m { get; set; }
        public List<float> windspeed_80m { get; set; }
        public List<float> windspeed_180m { get; set; }

        public List<int> winddirection_10m { get; set; }
        public List<int> winddirection_80m { get; set; }

        public List<float> windgusts_10m { get; set; }
        public List<float> temperature_80m { get; set; }
        public List<float> temperature_120m { get; set; }

        public List<float> soil_temperature_0cm { get; set; }
        public List<float> soil_temperature_6cm { get; set; }
        public List<float> soil_temperature_18cm { get; set; }
        public List<float> soil_moisture_0_1cm { get; set; }
        public List<float> soil_moisture_1_3cm { get; set; }
        public List<float> soil_moisture_3_9cm { get; set; }
        public List<float> soil_moisture_9_27cm { get; set; }

    }
}
