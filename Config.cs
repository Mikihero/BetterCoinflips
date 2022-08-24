using System.ComponentModel;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using Exiled.API.Enums;

namespace BetterCoinflips
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin should be enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not the default coins should spawn (eg. in lockers). Default: false")]
        public bool SpawnDefaultCoins { get; set; } = false;

        [Description("The duration of the broadcast informing you about your 'reward'. Default: 5")]
        public ushort BroadcastTime { get; set; } = 5;

        [Description("The duration of the map blackout. Default: 10")]
        public float MapBlackoutTime { get; set; } = 10;

        [Description("List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html")]
        public HashSet<EffectType> BadEffects { get; set; } = new HashSet<EffectType>
        {
            EffectType.Amnesia,
            EffectType.Asphyxiated,
            EffectType.Blinded,
            EffectType.Burned,
            EffectType.Concussed,
            EffectType.Deafened,
            EffectType.Disabled,
            EffectType.Ensnared,
            EffectType.Exhausted,
            EffectType.Flashed,
            EffectType.Hemorrhage,
            EffectType.SeveredHands,
            EffectType.SinkHole,
            EffectType.Stained,
            EffectType.Visual173Blink,
        };

        [Description("List of good effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html")]
        public HashSet<EffectType> GoodEffects { get; set; } = new HashSet<EffectType>
        {
            EffectType.BodyshotReduction,
            EffectType.DamageReduction,
            EffectType.Invigorated,
            EffectType.Invisible,
            EffectType.MovementBoost,
            EffectType.RainbowTaste,
            EffectType.Scp207,
            EffectType.Vitality,
        };

        [Description("The % chance of receiving a Facility Manager keycard instead of a Containment Engineer keycard when that effect is chosen. Default: 15")]
        public int RedCardChance { get; set; } = 15;
    }
}
