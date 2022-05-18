using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BetterCoinflips
{
    public sealed class Config : IConfig
    {
        [Description("Whether or not the plugin should be enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Amount of time effect broadcasts last.")]
        public int BroadcastTime { get; set; } = 2;

        [Description("Amount of time the entire map is in total blackout.")]
        public int MapBlackoutTime { get; set; } = 10;

        [Description("Amount of time an random effect lasts.")]
        public int RandomEffectTime { get; set; } = 10;
    }
}
