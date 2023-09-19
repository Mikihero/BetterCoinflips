using System;
using System.Collections.Generic;
using BetterCoinflips.Configs;
using Exiled.API.Features.Pickups;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace BetterCoinflips.Types
{
    public class CoinFlipEffect
    {
        private static readonly Config _cfg = Plugin.Instance.Config;
        private static readonly Configs.Translations _tr = Plugin.Instance.Translation;
        private static readonly System.Random _rd = new();
        
        private Action<Player> Execute { get; set; }
        private string Message { get; set; }

        public CoinFlipEffect(Action<Player> execute, string message)
        {
            Execute = execute;
            Message = message;
        }

        private static bool flag1 = _cfg.RedCardChance > _rd.Next(1, 101);
        
        public static List<CoinFlipEffect> GoodEffects = new()
        {
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(flag1 ? ItemType.KeycardFacilityManager : ItemType.KeycardContainmentEngineer, player.Position, new Quaternion());
            }, flag1 ? _tr.RedCardMessage : _tr.ContainmentEngineerCardMessage),
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.Medkit, player.Position, new Quaternion());
                Pickup.CreateAndSpawn(ItemType.Painkillers, player.Position, new Quaternion());
            }, _tr.MediKitMessage),
            new CoinFlipEffect()
        };
    }
}