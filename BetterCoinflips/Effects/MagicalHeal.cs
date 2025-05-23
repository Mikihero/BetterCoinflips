using System;
using System.Collections.Generic;
using BetterCoinflips.Types;
using Exiled.API.Features;
using Random = UnityEngine.Random;

namespace BetterCoinflips.Effects
{
    public class MagicalHeal : CoinEffect
    {
        private static bool toggle = true;
        
        public override List<(Func<bool>, string)> ConditionalMessages { get; set; } = new()
        {
            (() => toggle, Plugin.Instance.Translation.Heal25Message),
            (() => !toggle, Plugin.Instance.Translation.HealthIncreaseMessage)
        };

        public override EffectType EffectType { get; set; } = EffectType.Positive;

        public override void OnExecute(Player player)
        {
            if (Random.value > 0.5) 
                toggle = !toggle;

            if (toggle)
                player.Heal(25);
            else
                player.Health *= 1.1f;
        }
    }
}