using System;
using System.Collections.Generic;
using BetterCoinflips.Types;
using Exiled.API.Features;

namespace BetterCoinflips.Effects
{
    public class RandomGoodEffect : CoinEffect
    {
        public override List<(Func<bool>, string)> ConditionalMessages { get; set; } = new()
        {
            
        };
        
        public override EffectType EffectType { get; set; }
        
        public override void OnExecute(Player player)
        {
            throw new NotImplementedException();
        }
    }
}