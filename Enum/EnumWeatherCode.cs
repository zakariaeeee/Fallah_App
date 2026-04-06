using System.ComponentModel;

namespace Fallah_App.Enum
{
    public enum EnumWeatherCode
    {

        [Description("Clear sky")]
        ClearSky = 0,

        [Description("Mainly clear")]
        MainlyClear = 1,

        [Description("Partly cloudy")]
        PartlyCloudy = 2,

        [Description("Overcast")]
        Overcast = 3,

        [Description("Fog and depositing rime fog")]
        FogAndDepositingRimeFog = 45,

        [Description("Drizzle: Light intensity")]
        DrizzleLight = 51,

        [Description("Drizzle: Moderate intensity")]
        DrizzleModerate = 53,

        [Description("Drizzle: Dense intensity")]
        DrizzleDense = 55,

        [Description("Freezing Drizzle: Light intensity")]
        FreezingDrizzleLight = 56,

        [Description("Freezing Drizzle: Dense intensity")]
        FreezingDrizzleDense = 57,

        [Description("Rain: Slight intensity")]
        RainSlight = 61,

        [Description("Rain: Moderate intensity")]
        RainModerate = 63,

        [Description("Rain: Heavy intensity")]
        RainHeavy = 65,

        [Description("Freezing Rain: Light intensity")]
        FreezingRainLight = 66,

        [Description("Freezing Rain: Heavy intensity")]
        FreezingRainHeavy = 67,

        [Description("Snowfall: Slight intensity")]
        SnowfallSlight = 71,

        [Description("Snowfall: Moderate intensity")]
        SnowfallModerate = 73,

        [Description("Snowfall: Heavy intensity")]
        SnowfallHeavy = 75,

        [Description("Snow grains")]
        SnowGrains = 77,

        [Description("Rain showers: Slight intensity")]
        RainShowersSlight = 80,

        [Description("Rain showers: Moderate intensity")]
        RainShowersModerate = 81,

        [Description("Rain showers: Violent intensity")]
        RainShowersViolent = 82,

        [Description("Snow showers: Slight intensity")]
        SnowShowersSlight = 85,

        [Description("Snow showers: Heavy intensity")]
        SnowShowersHeavy = 86,

        [Description("Thunderstorm: Slight or moderate")]
        ThunderstormSlightOrModerate = 95,

        [Description("Thunderstorm with slight hail")]
        ThunderstormWithSlightHail = 96,

        [Description("Thunderstorm with heavy hail")]
        ThunderstormWithHeavyHail = 99
    }
}
