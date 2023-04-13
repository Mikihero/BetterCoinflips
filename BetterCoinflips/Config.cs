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

        [Description("The message broadcast to a player when they receive a facility manager keycard (the red one) from the coin.")]
        public string RedCardMessage { get; set; } = "You acquired a Facility Manager keycard!";

        [Description("The message broadcast to a player when they receive a containment engineer keycard (the useless one) from the coin.")]
        public string ContainmentEngineerCardMessage { get; set; } = "You acquired a Containment Engineer keycard!";

        [Description("The message broadcast to a player when they receive a medi-kit from the coin.")]
        public string MediKitMessage { get; set; } = "You received a Medical Kit!";

        [Description("The message broadcast to a player when they get teleported to the escape area by the coin.")]
        public string TpToEscapeMessage { get; set; } = "You can now escape! That's what you wanted right?";

        [Description("The message broadcast to a player when they get magically healed by the coin.")]
        public string MagicHealMessage { get; set; } = "You've been magically healed!";

        [Description("The message broadcast to a player when they get their hp increased by 10% by the coin.")]
        public string HealthIncreaseMessage { get; set; } = "You received 10% more hp!";

        [Description("The message broadcast to a player when they receive an SCP-268 from the coin.")]
        public string NeatHatMessage { get; set; } = "You got a neat hat!";

        [Description("The message broadcast to a player when they receive a random good effect from the coin.")]
        public string RandomGoodEffectMessage { get; set; } = "You got a random effect.";

        [Description("The message broadcast to a player when they receive a logicer with 1 ammo from the coin.")]
        public string OneAmmoLogicerMessage { get; set; } = "You got gun.";

        [Description("The message broadcast to a player when they receive an SCP-2176 from the coin.")]
        public string LightbulbMessage { get; set; } = "You got a shiny lightbulb!";

        [Description("The message broadcast to a player when they receive a pink candy from the coin.")]
        public string PinkCandyMessage { get; set; } = "You got a pretty candy!";



        [Description("The message broadcast to a player when they get their hp reduced by 30% by the coin.")]
        public string HPReductionMessage { get; set; } = "Your hp got reduced by 30%.";

        [Description("The message broadcast to a player when they get teleported to Class D cells by the coin.")]
        public string TPToClassDCellsMessage { get; set; } = "You got teleported to Class D cells.";

        [Description("The message broadcast to a player when they receive a random bad effect from the coin.")]
        public string RandomBadEffectMessage { get; set; } = "You got a random effect.";

        [Description("The message broadcast to a player when the warhead has been stopped by the coin.")]
        public string WarheadStopMessage { get; set; } = "The warhead has been stopped.";

        [Description("The message broadcast to a player when the warhead has been started by the coin.")]
        public string WarheadStartMessage { get; set; } = "The warhead has been started.";

        [Description("The message broadcast to a player when the lights have been turned off by the coin.")]
        public string LightsOutMessage { get; set; } = "Lights out.";

        [Description("The message broadcast to a player when a grenade has been dropped on their head by the coin.")]
        public string LiveGrenadeMessage { get; set; } = "Watch your head!";

        [Description("The message broadcast to a player when they receive a troll particle disruptor from the coin.")]
        public string TrollGunMessage { get; set; } = "YOU GOT A WHAT!?";

        [Description("The message broadcast to a player when a flash that can't blind them is dropped on their head by the coin.")]
        public string TrollFlashMessage { get; set; } = "You heard something?";

        [Description("The message broadcast to a player when they are teleported to a random SCP by the coin.")]
        public string TPToRandomSCPMessage { get; set; } = "You were teleported to an SCP.";

        [Description("The message broadcast to a player when they are dealth 15 damage by the coin.")]
        public string SmallDamageMessage { get; set; } = "You've lost 15hp.";

        [Description("The message broadcast to a player when they are left on 1 hp by the coin.")]
        public string HugeDamageMessage { get; set; } = "You've lost a lot of hp";

        [Description("The message broadcast to a player when they a primed vase is spawned on their head.")]
        public string PrimedVaseMessage { get; set; } = "Your grandma payed you a visit!";

        [Description("The message broadcast to a player when an SCP-173 tantrum is spawned beneath their feet.")]
        public string ShitPantsMessage { get; set; } = "You just shit your pants.";
        
        [Description("The message broadcast to a player when the coin fakes a cassie of an SCP dying.")]
        public string FakeSCPKillMessage { get; set; } = "Did you just kill an SCP?!";
    }
}
