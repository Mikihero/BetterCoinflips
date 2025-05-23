using System;
using System.Collections.Generic;
using BetterCoinflips.Types;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;

namespace BetterCoinflips.Effects
{
    public class NeatHat : CoinEffect
    {
        public override List<(Func<bool>, string)> ConditionalMessages { get; set; } = new()
        {
            (() => true, Plugin.Instance.Translation.NeatHatMessage)
        };

        public override EffectType EffectType { get; set; } = EffectType.Positive;
        
        public override void OnExecute(Player player)
        {
            Pickup.CreateAndSpawn(ItemType.SCP268, player.Position);
        }
    }
}