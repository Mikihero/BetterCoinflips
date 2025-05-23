using System;
using System.Collections.Generic;
using BetterCoinflips.Types;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;

namespace BetterCoinflips.Effects
{
    public class TpToEscape : CoinEffect
    {
        public override List<(Func<bool>, string)> ConditionalMessages { get; set; } = new()
        {
            (() => true, Plugin.Instance.Translation.TpToEscapeMessage)
        };

        public override EffectType EffectType { get; set; } = EffectType.Neutral;

        public override void OnExecute(Player player)
        {
            player.Teleport(Door.Get(DoorType.EscapeSecondary).Position);
        }
    }
}