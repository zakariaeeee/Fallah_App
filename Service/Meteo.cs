using Fallah_App.Models;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace Fallah_App.Service
{
    public class Meteo
    {
        public double latitude { get; set; }

        public double longitude { get; set; }

       public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }

        public hourly_units hourly_units { get; set; }
        public Hourly hourly { get; set; }
        public daily daily { get; set; }
        public daily_units daily_units { get; set; }



        public static async Task<Meteo> getMeteo(string lat,string lon)
        {
            Meteo weatherData = null;
            int i = 0;
            string url = "https://api.open-meteo.com/v1/forecast?latitude="+lat+"&longitude="+lon+"&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation_probability,precipitation,rain,showers,snowfall,snow_depth,weathercode,pressure_msl,surface_pressure,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,visibility,evapotranspiration,et0_fao_evapotranspiration,vapor_pressure_deficit,windspeed_10m,windspeed_80m,windspeed_120m,windspeed_180m,winddirection_10m,winddirection_80m,winddirection_120m,winddirection_180m,windgusts_10m,temperature_80m,temperature_120m,temperature_180m,soil_temperature_0cm,soil_temperature_6cm,soil_temperature_18cm,soil_temperature_54cm,soil_moisture_0_1cm,soil_moisture_1_3cm,soil_moisture_3_9cm,soil_moisture_9_27cm,soil_moisture_27_81cm&daily=weathercode,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,sunrise,sunset,uv_index_max,uv_index_clear_sky_max,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,precipitation_probability_max,windspeed_10m_max,windgusts_10m_max,winddirection_10m_dominant,shortwave_radiation_sum,et0_fao_evapotranspiration&timezone=auto";
            while (i < 3 && weatherData == null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        //var response = await client.GetAsync("-1.91&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,precipitation_probability,rain,snowfall,weathercode,cloudcover,windspeed_10m,windspeed_80m,windspeed_180m,winddirection_10m,winddirection_80m,windgusts_10m,temperature_80m,temperature_120m,soil_temperature_0cm,soil_temperature_6cm,soil_temperature_18cm,soil_moisture_0_1cm,soil_moisture_1_3cm,soil_moisture_3_9cm,soil_moisture_9_27cm&daily=weathercode,temperature_2m_max,temperature_2m_min,sunrise,sunset,uv_index_max,uv_index_clear_sky_max,precipitation_sum,rain_sum,snowfall_sum,precipitation_hours,precipitation_probability_max,windspeed_10m_max,windgusts_10m_max&timezone=auto");
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();

                            weatherData = JsonConvert.DeserializeObject<Meteo>(data);


                        }
                        else
                        {
                            i++;
                            await Task.Delay(10000);
                        }
                    }
                    catch (Exception ex)
                    {
                        i++;
                        await Task.Delay(10000);
                    }
                }
           
            }
            return weatherData;

        }

    }
}
