using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using PlayerRoles;

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

        [Description("Time in seconds between coin toses.")]
        public double CoinCooldown { get; set; } = 5;

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
            EffectType.PocketCorroding,
            EffectType.SeveredHands,
            EffectType.SinkHole,
            EffectType.Stained,
            EffectType.Traumatized
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

        [Description("The % chance of receiving a Facility Manager keycard instead of a Containment Engineer one.")]
        public int RedCardChance { get; set; } = 15;

        [Description("The kick reason.")] 
        public string KickReason { get; set; } = "The coin kicked your ass.";

        [Description("The list of SCP's that you can turn into by using the coin.")]
        public HashSet<RoleTypeId> ValidScps { get; set; } = new()
        {
            RoleTypeId.Scp049,
            RoleTypeId.Scp096,
            RoleTypeId.Scp106,
            RoleTypeId.Scp173,
            RoleTypeId.Scp0492,
            RoleTypeId.Scp939,
        };

        [Description("List of ignored roles for the PlayerSwapEffect (#18)")]
        public HashSet<RoleTypeId> IgnoredRoles { get; set; } = new()
        {
            RoleTypeId.Spectator,
            RoleTypeId.Filmmaker,
            RoleTypeId.Overwatch,
        };

        public HashSet<ItemType> ItemsToGive { get; set; } = new()
        {
            ItemType.Adrenaline,
            ItemType.Coin,
            ItemType.Flashlight,
            ItemType.Jailbird,
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.Radio,
            ItemType.ArmorCombat,
            ItemType.ArmorHeavy,
            ItemType.ArmorLight,
            ItemType.GrenadeFlash,
            ItemType.GrenadeHE,
            ItemType.GunA7,
            ItemType.GunCom45,
            ItemType.GunCrossvec,
            ItemType.GunLogicer,
            ItemType.GunRevolver,
            ItemType.GunShotgun,
            ItemType.GunAK,
            ItemType.GunCOM15,
            ItemType.GunCOM18,
            ItemType.GunE11SR,
            ItemType.GunFSP9,
            ItemType.GunFRMG0,
        };
        
        [Description("The chance of these good effects happening. It's a proportional chance not a % chance.")]
        public int KeycardChance { get; set; } = 20;
        public int MedicalKitChance { get; set; } = 35;
        public int TpToEscapeChance { get; set; } = 5;
        public int HealChance { get; set; } = 10;
        public int MoreHpChance { get; set; } = 10;
        public int HatChance { get; set; } = 10;
        public int RandomGoodEffectChance { get; set; } = 30;
        public int OneAmmoLogicerChance { get; set; } = 1;
        public int LightbulbChance { get; set; } = 15;
        public int PinkCandyChance { get; set; } = 10;
        public int BadRevoChance { get; set; } = 5;
        public int EmptyHidChance { get; set; } = 5;
        public int ForceRespawnChance { get; set; } = 15;
        public int SizeChangeChance { get; set; } = 20;
        public int RandomItemChance { get; set; } = 35;

        [Description("The chance of these bad effects happening. It's a proportional chance not a % chance.")]
        public int HpReductionChance { get; set; } = 20;
        public int TpToClassDCellsChance { get; set; } = 5;
        public int RandomBadEffectChance { get; set; } = 20;
        public int WarheadChance { get; set; } = 10;
        public int LightsOutChance { get; set; } = 20;
        public int LiveHeChance { get; set; } = 30;
        public int TrollFlashChance { get; set; } = 50;
        public int ScpTpChance { get; set; } = 20;
        public int OneHpLeftChance { get; set; } = 15;
        public int PrimedVaseChance { get; set; } = 20;
        public int ShitPantsChance { get; set; } = 40;
        public int FakeCassieChance { get; set; } = 50;
        public int TurnIntoScpChance { get; set; } = 30;
        public int InventoryResetChance { get; set; } = 20;
        public int ClassSwapChance { get; set; } = 10;
        public int InstantExplosionChance { get; set; } = 10;
        public int PlayerSwapChance { get; set; } = 20;
        public int KickChance { get; set; } = 5;
        public int SpectSwapChance { get; set; } = 10;
        public int TeslaTpChance { get; set; } = 15;
        public int InventorySwapChance { get; set; } = 20;
        public int HandcuffChance { get; set; } = 10;
        public int RandomTeleportChance { get; set; } = 15;
    }
}
