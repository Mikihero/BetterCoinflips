using System;
using System.Collections.Generic;
using BetterCoinflips.Types;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;

namespace BetterCoinflips.Effects
{
    public class Medikit : CoinEffect
    {
        public override List<(Func<bool>, string)> ConditionalMessages { get; set; } = new()
        {
            (() => true, Plugin.Instance.Translation.MediKitMessage)
        };

        public override EffectType EffectType { get; set; } = EffectType.Positive;

        public override void OnExecute(Player player)
        {
            Pickup.CreateAndSpawn(ItemType.Medkit, player.Position);
            Pickup.CreateAndSpawn(ItemType.Painkillers, player.Position);
        }
    }
}