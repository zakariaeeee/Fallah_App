namespace Fallah_App.Service
{
    public class daily
    {
        public List<DateOnly> time { get; set; }
        public List<int> weathercode { get; set; }
        public List<float> temperature_2m_max { get; set; }
        public List<float> temperature_2m_min { get; set; }
        public List<DateTime> sunrise { get; set; }
        public List<DateTime> sunset { get; set; }
        public List<float> uv_index_max { get; set; }
        public List<float> uv_index_clear_sky_max { get; set; }
        public List<float> precipitation_sum { get; set; }
        public List<float> rain_sum { get; set; }
        public List<float> snowfall_sum { get; set; }
        public List<float> precipitation_hours { get; set; }
        public List<float> precipitation_probability_max { get; set; }
        public List<float> windspeed_10m_max { get; set; }
        public List<float> windgusts_10m_max { get; set; }



    }
}
