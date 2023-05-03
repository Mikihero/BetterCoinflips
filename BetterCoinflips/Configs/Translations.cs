using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BetterCoinflips.Configs
{
    public class Translations : ITranslation
    {
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

        [Description("The message broadcast to a player when they receive an empty micro hid from the coin.")]
        public string EmptyHidMessage { get; set; } = "You got a MICRO HID?!";

        [Description("The message broadcast to a player when they receive the worst revolver possible from the coin.")]
        public string BadRevoMessage { get; set; } = "What is this abomination!?";
        


        [Description("The message broadcast to a player when they get their hp reduced by 30% by the coin.")]
        public string HpReductionMessage { get; set; } = "Your hp got reduced by 30%.";

        [Description("The message broadcast to a player when they get teleported to Class D cells by the coin.")]
        public string TpToClassDCellsMessage { get; set; } = "You got teleported to Class D cells.";

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
        public string TpToRandomScpMessage { get; set; } = "You were teleported to an SCP.";

        [Description("The message broadcast to a player when they are dealth 15 damage by the coin.")]
        public string SmallDamageMessage { get; set; } = "You've lost 15hp.";

        [Description("The message broadcast to a player when they are left on 1 hp by the coin.")]
        public string HugeDamageMessage { get; set; } = "You've lost a lot of hp";

        [Description("The message broadcast to a player when they a primed vase is spawned on their head.")]
        public string PrimedVaseMessage { get; set; } = "Your grandma paid you a visit!";

        [Description("The message broadcast to a player when an SCP-173 tantrum is spawned beneath their feet.")]
        public string ShitPantsMessage { get; set; } = "You just shit your pants.";
        
        [Description("The message broadcast to a player when the coin fakes a cassie of an SCP dying.")]
        public string FakeScpKillMessage { get; set; } = "Did you just kill an SCP?!";

        [Description("The message to be added the the chosen effect broadcast if the coin breaks after that throw.")]
        public string CoinBreaksMessage { get; set; } = "\nAlso your coin was used too much and it broke down.";
    }
}