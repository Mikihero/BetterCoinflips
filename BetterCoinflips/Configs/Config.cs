using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;

namespace BetterCoinflips.Configs
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin should be enabled. Default: true")]
        public bool IsEnabled { get; set; } = true;

        [Description("Whether or not debug logs should be shown. Default: false")]
        public bool Debug { get; set; } = false;

        [Description("The amount of base game spawned coins that should be removed. Default: 4")]
        public int DefaultCoinsAmount { get; set; } = 4;

        [Description("The ItemType of the item to be replaced with a coin and the amount to be replaced, the item is supposed to be something found in SCP pedestals.")]
        public Dictionary<ItemType, int> ItemToReplace { get; set; } = new()
        {
            { ItemType.SCP500, 2 }
        };

        [Description("The boundaries of the random range of throws each coin will have before it breaks. The upper bound is exclusive.")]
        public List<int> MinMaxDefaultCoins { get; set; } = new()
        {
            1, 
            4
        };

        [Description("The duration of the broadcast informing you about your 'reward'. Default: 3")]
        public ushort BroadcastTime { get; set; } = 3;

        [Description("The duration of the map blackout. Default: 10")]
        public float MapBlackoutTime { get; set; } = 10;

        [Description("The fuse time of the grenade falling on your head. Default: 3.25")]
        public double LiveGrenadeFuseTime { get; set; } = 3.25;

        [Description("List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html")]
        public HashSet<EffectType> BadEffects { get; set; } = new()
        {
            EffectType.Asphyxiated,
            EffectType.Bleeding,
            EffectType.Blinded,
            EffectType.Burned,
            EffectType.Concussed,
            EffectType.Corroding,
            EffectType.CardiacArrest,
            EffectType.Deafened,
            EffectType.Decontaminating,
            EffectType.Disabled,
            EffectType.Ensnared,
            EffectType.Exhausted,
            EffectType.Flashed,
            EffectType.Hemorrhage,
            EffectType.Hypothermia,
            EffectType.InsufficientLighting,
            EffectType.Poisoned,
            EffectType.SeveredHands,
            EffectType.SinkHole,
            EffectType.Stained,
        };
        
        [Description("List of good effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html")]
        public HashSet<EffectType> GoodEffects { get; set; } = new()
        {
            EffectType.BodyshotReduction,
            EffectType.DamageReduction,
            EffectType.Invigorated,
            EffectType.Invisible,
            EffectType.MovementBoost,
            EffectType.RainbowTaste,
            EffectType.Scp1853,
            EffectType.Scp207,
            EffectType.Vitality
        };

        [Description("The % chance of receiving a Facility Manager keycard instead of a Containment Engineer keycard when that effect is chosen. Default: 15")]
        public int RedCardChance { get; set; } = 15;

        [Description("The chance of these good effects happening. It's a proportional chance not a % chance.")]
        public int KeycardEffectChance { get; set; } = 20;
        public int MedicalKitEffectChance { get; set; } = 35;
        public int TpToEscapeEffectChance { get; set; } = 5;
        public int HealEffectChance { get; set; } = 10;
        public int MoreHpEffectChance { get; set; } = 10;
        public int HatEffectChance { get; set; } = 10;
        public int RandomGoodEffectChance { get; set; } = 30;
        public int OneAmmoLogicerEffectChance { get; set; } = 1;
        public int LightbulbEffectChance { get; set; } = 15;
        public int PinkCandyEffectChance { get; set; } = 10;
        public int BadRevoEffectChance { get; set; } = 5;
        public int EmptyHidEffectChance { get; set; } = 5;

        [Description("The chance of these bad effects happening. It's a proportional chance not a % chance.")]
        public int HpReductionEffectChance { get; set; } = 20;
        public int TpToClassDCellsEffectChance { get; set; } = 5;
        public int RandomBadEffectChance { get; set; } = 20;
        public int WarheadEffectChance { get; set; } = 10;
        public int LightsOutEffectChance { get; set; } = 20;
        public int LiveHeEffectChance { get; set; } = 30;
        public int TrollGunEffectChance { get; set; } = 50;
        public int TrollFlashEffectChance { get; set; } = 50;
        public int ScpTpEffectChance { get; set; } = 20;
        public int OneHpLeftEffectChance { get; set; } = 15;
        public int PrimedVaseEffectChance { get; set; } = 20;
        public int ShitPantsEffectChance { get; set; } = 40;
        public int FakeCassieEffectChance { get; set; } = 50;
        public int ZombieFcEffectChance { get; set; } = 30;
        public int InventoryResetEffectChance { get; set; } = 20;
        public int ClassSwapEffectChance { get; set; } = 10;
        public int InstantExplosionEffectChance { get; set; } = 10;
    }
}
