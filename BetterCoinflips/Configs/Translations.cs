using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BetterCoinflips.Configs
{
    public class Translations : ITranslation
    {
        [Description("This is added to the effect message if the coin breaks.")]
        public string CoinBreaksMessage { get; set; } = "\nAlso your coin was used too much and it broke down.";
        
        [Description("The broadcast message when a coin is registered with no uses.")]
        public string CoinNoUsesMessage { get; set; } = "Your coin had no uses to begin with!";

        public List<string> HintMessages { get; set; } = new()
        {
            "Your coin landed on tails.",
            "Your coin landed on heads."
        };
        
        [Description("Here you can set the message for each of these good coin effects.")]
        public string TossOnCooldownMessage { get; set; } = "You can't throw the coin yet.";
        public string RedCardMessage { get; set; } = "You acquired a Facility Manager keycard!";
        public string ContainmentEngineerCardMessage { get; set; } = "You acquired a Containment Engineer keycard!";
        public string MediKitMessage { get; set; } = "You received a Medical Kit!";
        public string TpToEscapeMessage { get; set; } = "You can now escape! That's what you wanted right?";
        public string MagicHealMessage { get; set; } = "You've been magically healed!";
        public string HealthIncreaseMessage { get; set; } = "You received 10% more hp!";
        public string NeatHatMessage { get; set; } = "You got a neat hat!";
        public string RandomGoodEffectMessage { get; set; } = "You got a random effect.";
        public string OneAmmoLogicerMessage { get; set; } = "You got gun.";
        public string LightbulbMessage { get; set; } = "You got a shiny lightbulb!";
        public string PinkCandyMessage { get; set; } = "You got a pretty candy!";
        public string BadRevoMessage { get; set; } = "What is this abomination!?";
        public string EmptyHidMessage { get; set; } = "DID YOU JUST GET A MICRO HID!?";
        public string ForceRespawnMessage { get; set; } = "Someone respawned... probably.";
        public string SizeChangeMessage { get; set; } = "You got gnomed.";
        public string RandomItemMessage { get; set; } = "You got a random item!";


        
        [Description("Here you can set the message for each of these bad coin effects.")]
        public string HpReductionMessage { get; set; } = "Your hp got reduced by 30%.";
        public string TpToClassDCellsMessage { get; set; } = "You got teleported to Class D cells.";
        public string TpToClassDCellsAfterWarheadMessage { get; set; } = "You were teleported into a radioactive zone.";
        public string RandomBadEffectMessage { get; set; } = "You got a random effect.";
        public string WarheadStopMessage { get; set; } = "The warhead has been stopped.";
        public string WarheadStartMessage { get; set; } = "The warhead has been started.";
        public string LightsOutMessage { get; set; } = "Lights out.";
        public string LiveGrenadeMessage { get; set; } = "Watch your head!";
        public string TrollFlashMessage { get; set; } = "You heard something?";
        public string TpToRandomScpMessage { get; set; } = "You were teleported to an SCP.";
        public string SmallDamageMessage { get; set; } = "You've lost 15hp.";
        public string HugeDamageMessage { get; set; } = "You've lost a lot of hp";
        public string PrimedVaseMessage { get; set; } = "Your grandma paid you a visit!";
        public string ShitPantsMessage { get; set; } = "You just shit your pants.";
        public string FakeScpKillMessage { get; set; } = "Did you just kill an SCP?!";
        public string TurnIntoScpMessage { get; set; } = "Get SCP-fied LOL!";
        public string InventoryResetMessage { get; set; } = "You lost your stuff.";
        public string ClassSwapMessage { get; set; } = "That's what I call an UNO reverse card!";
        public string InstantExplosionMessage { get; set; } = "You got smoked.";
        public string PlayerSwapMessage { get; set; } = "Your inventory was swapped with a random player.";
        public string PlayerSwapIfOneAliveMessage { get; set; } = "You were supposed to switch places with someone but no one else is alive!";
        public string KickMessage { get; set; } = "Bye!";
        public string SpectSwapPlayerMessage { get; set; } = "You just made someone's round better!";
        public string SpectSwapSpectMessage { get; set; } = "You were chosen as a random spectator to replace this player!";
        public string SpectSwapNoSpectsMessage { get; set; } = "You got lucky cause there are no spectators to take your place.";
        public string TeslaTpMessage { get; set; } = "So you're a fan of electricity?";
        public string TeslaTpAfterWarheadMessage { get; set; } = "You were teleported into a radioactive zone.";
        
        [Description("This message will be broadcast to both players.")]
        public string InventorySwapMessage { get; set; } = "Your inventory was swapped with a random player.";
        public string InventorySwapOnePlayerMessage { get; set; } = "You can't swap with anyone so you're losing health instead.";
        public string RandomTeleportMessage { get; set; } = "You were randomly teleported.";
        public string RandomTeleportWarheadDetonatedMessage { get; set; } = "Warhead is detonated so you only got a candy.";
        public string HandcuffMessage { get; set; } = "You were arrested for uhh commiting war crimes... or something.";
    }
}