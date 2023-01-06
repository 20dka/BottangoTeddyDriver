using AudioPPM;

namespace BottangoTeddyDriver
{
    public static class StandardProfiles
    {
        public static PpmProfile ruxpin = new PpmProfile()
        {
            PauseDuration = 250,
            Polarity = PpmPolarity.HIGH,
            MinChannelDuration = 750 + 100,
            MaxChannelDuration = 1750,
            Period = 20000
        };

        public static PpmProfile goose = new PpmProfile()
        {
            PauseDuration = 250,
            Polarity = PpmPolarity.HIGH,
            MinChannelDuration = 750 + 200,
            MaxChannelDuration = 1750,
            Period = 30000,
            Suffix = new bool[] { true, false, false, false, true, true, true, true }
        };

        public static PpmProfile mickey = new PpmProfile()
        {
            PauseDuration = 250,
            Polarity = PpmPolarity.HIGH,
            MinChannelDuration = 750 + 200,
            MaxChannelDuration = 1750,
            Period = 30000,
            Suffix = new bool[] { true, true, false, false, true, true, true, true }
        };

    }
}