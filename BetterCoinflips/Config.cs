using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace BetterCoinflips
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin should be enabled. Default: true")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug logs should be shown. Default: false")]
        public bool Debug { get; set; } = false;

        [Description("Whether or not the default coins should spawn (eg. in lockers). Default: false")]
        public bool SpawnDefaultCoins { get; set; } = false;

        [Description("The ItemType of the item to be replaced with a coin, the item is supposed to be something found in SCP pedestals.")]
        public ItemType ItemToReplace { get; set; } = ItemType.SCP1853;
        
        [Description("Whether or not the coin should be removed from a players inventory after it's thrown. Default: false.")]
        public bool RemoveCoinOnThrow { get; set; } = false;

        [Description("The duration of the broadcast informing you about your 'reward'. Default: 3")]
        public ushort BroadcastTime { get; set; } = 3;

        [Description("The duration of the map blackout. Default: 10")]
        public float MapBlackoutTime { get; set; } = 10;

        [Description("The fuse time of the grenade falling on your head. Default: 3.25")]
        public double LiveGrenadeFuseTime { get; set; } = 3.25;

        [Description("List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html")]
        public HashSet<EffectType> BadEffects { get; set; } = new HashSet<EffectType>
        {
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

        [Description("The chance of these effects happening. It's a proportional chance not a % chance.")]
        public int KeycardEffectChance { get; set; } = 20;
        public int MedicalKitEffectChance { get; set; } = 35;
        public int TPToEscapeEffectChance { get; set; } = 5;
        public int HealEffectChance { get; set; } = 10;
        public int MoreHPEffectChance { get; set; } = 10;
        public int HatEffectChance { get; set; } = 10;
        public int RandomGoodEffectChance { get; set; } = 30;
        public int OneAmmoLogicerEffectChance { get; set; } = 1;
        public int LightbulbEffectChance { get; set; } = 15;
        public int PinkCandyEffectChance { get; set; } = 10;

        [Description("The chance of these effects happening. It's a proportional chance not a % chance.")]
        public int HpReductionEffectChance { get; set; } = 20;
        public int TPToClassDCellsEffectChance { get; set; } = 5;
        public int RandomBadEffectChance { get; set; } = 20;
        public int WarheadEffectChance { get; set; } = 10;
        public int LightsOutEffectChance { get; set; } = 20;
        public int LiveHEEffectChance { get; set; } = 30;
        public int TrollGunEffectChance { get; set; } = 50;
        public int TrollFlashEffectChance { get; set; } = 50;
        public int SCPTpEffectChance { get; set; } = 20;
        public int OneHPLeftEffectChance { get; set; } = 15;
        public int PrimedVaseEffectChance { get; set; } = 20;
        public int ShitPantsEffectChance { get; set; } = 40;
        public int FakeCassieEffectChance { get; set; } = 50;
    }
}
