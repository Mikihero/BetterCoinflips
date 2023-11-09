using System;
using System.Collections.Generic;
using Exiled.API.Features;
using System.Linq;
using BetterCoinflips.Configs;
using BetterCoinflips.Types;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace BetterCoinflips
{
    public class EventHandlers
    {
        private static readonly Config Cfg = Plugin.Instance.Config;
        private readonly Configs.Translations _tr = Plugin.Instance.Translation;
        private readonly System.Random _rd = new();
        
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public static Dictionary<ushort, int> CoinUses = new();

        //Dict of all good coin effect chances with an index
        private readonly Dictionary<int, int> _goodEffectChances = new()
        {
            { 0, Cfg.KeycardChance },
            { 1, Cfg.MedicalKitChance },
            { 2, Cfg.TpToEscapeChance },
            { 3, Cfg.HealChance },
            { 4, Cfg.MoreHpChance },
            { 5, Cfg.HatChance },
            { 6, Cfg.RandomGoodEffectChance },
            { 7, Cfg.OneAmmoLogicerChance },
            { 8, Cfg.LightbulbChance },
            { 9, Cfg.PinkCandyChance },
            { 10, Cfg.BadRevoChance },
            { 11, Cfg.EmptyHidChance },
            { 12, Cfg.ForceRespawnChance },
            { 13, Cfg.SizeChangeChance },
            { 14, Cfg.RandomItemChance }
        };

        //Dict of all bad coin effect chances with an index
        private readonly Dictionary<int, int> _badEffectChances = new()
        {
            { 0, Cfg.HpReductionChance },
            { 1, Cfg.TpToClassDCellsChance },
            { 2, Cfg.RandomBadEffectChance },
            { 3, Cfg.WarheadChance },
            { 4, Cfg.LightsOutChance },
            { 5, Cfg.LiveHeChance },
            { 6, Cfg.TrollFlashChance },
            { 7, Cfg.ScpTpChance },
            { 8, Cfg.OneHpLeftChance },
            { 9, Cfg.PrimedVaseChance },
            { 10, Cfg.ShitPantsChance },
            { 11, Cfg.FakeCassieChance },
            { 12, Cfg.TurnIntoScpChance },
            { 13, Cfg.InventoryResetChance },
            { 14, Cfg.ClassSwapChance },
            { 15, Cfg.InstantExplosionChance },
            { 16, Cfg.PlayerSwapChance },
            { 17, Cfg.KickChance },
            { 18, Cfg.SpectSwapChance },
            { 19, Cfg.TeslaTpChance },
            { 20, Cfg.InventorySwapChance },
            { 21, Cfg.RandomTeleportChance },
            { 22, Cfg.HandcuffChance },
        };

        private readonly Dictionary<string, DateTime> _cooldownDict = new();

        //helper method
        public static void SendBroadcast(Player pl, string message) => pl.Broadcast(new Exiled.API.Features.Broadcast(message, Cfg.BroadcastTime),true);

        //main plugin logic
        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            //broadcast message
            string message = ""; 
            //used to remove the coin if uses run out, since they are checked before executing the effect
            bool helper = false; 
            //check if player is on cooldown
            bool flag = _cooldownDict.ContainsKey(ev.Player.RawUserId) 
                        && (DateTime.UtcNow - _cooldownDict[ev.Player.RawUserId]).TotalSeconds < Cfg.CoinCooldown;
            if (flag)
            {
                ev.IsAllowed = false;
                SendBroadcast(ev.Player, _tr.TossOnCooldownMessage);
                Log.Debug($"{ev.Player.Nickname} tried to throw a coin on cooldown.");
                return;
            }
            
            //set cooldown for player
            _cooldownDict[ev.Player.RawUserId] = DateTime.UtcNow;
            
            //check if coin has registered uses
            if (!CoinUses.ContainsKey(ev.Player.CurrentItem.Serial))
            {
                CoinUses.Add(ev.Player.CurrentItem.Serial, _rd.Next(Cfg.MinMaxDefaultCoins[0], Cfg.MinMaxDefaultCoins[1]));
                Log.Debug($"Registered a coin, Uses Left: {CoinUses[ev.Player.CurrentItem.Serial]}");
                //check if the newly registered coin has no uses
                if (CoinUses[ev.Player.CurrentItem.Serial] < 1)
                {
                    //remove the coin from the uses list
                    CoinUses.Remove(ev.Player.CurrentItem.Serial);
                    Log.Debug("Removed the coin");
                    if (ev.Player.CurrentItem != null)
                    {
                        ev.Player.RemoveHeldItem();
                    }
                    SendBroadcast(ev.Player, _tr.CoinNoUsesMessage);
                    return;
                }
            }
            
            //decrement coin uses
            CoinUses[ev.Player.CurrentItem.Serial]--;
            Log.Debug($"Uses Left: {CoinUses[ev.Player.CurrentItem.Serial]}");

            //check if uses that were already registered have been set to 0 to remove the coin after executing the effect
            if (CoinUses[ev.Player.CurrentItem.Serial] < 1)
            {
                helper = true;
            }

            Log.Debug($"Is tails: {ev.IsTails}");
            
            if (!ev.IsTails)
            {
                int totalChance = _goodEffectChances.Values.Sum();
                int randomNum = _rd.Next(1, totalChance + 1);
                // Set a default value for headsEvent
                int headsEvent = 2;

                //magic loop to determine headsevent, takes into account chances for each item in the enumerated Dict
                foreach (KeyValuePair<int, int> kvp in _goodEffectChances)
                {
                    if (randomNum <= kvp.Value)
                    {
                        headsEvent = kvp.Key;
                        break;
                    }

                    randomNum -= kvp.Value;
                }
                
                Log.Debug($"headsEvent = {headsEvent}");

                //use headsevent to choose the effect and execute it
                var effect = CoinFlipEffect.GoodEffects[headsEvent];
                effect.Execute(ev.Player);
                message = effect.Message;
            }
            if (ev.IsTails)
            {
                int totalChance = _badEffectChances.Values.Sum();
                int randomNum = _rd.Next(1, totalChance + 1);
                // Set a default value for headsEvent
                int tailsEvent = 13;

                //magic loop to determine headsevent, takes into account chances for each item in the enumerated Dict
                foreach (KeyValuePair<int, int> kvp in _badEffectChances)
                {
                    if (randomNum <= kvp.Value)
                    {
                        tailsEvent = kvp.Key;
                        break;
                    }

                    randomNum -= kvp.Value;
                }
                
                Log.Debug($"tailsEvent = {tailsEvent}");

                //use tailsevent to choose the effect and execute it
                var effect = CoinFlipEffect.BadEffects[tailsEvent];
                effect.Execute(ev.Player);
                message = effect.Message;
            }

            //if the coin has 0 uses remove it
            if (helper)
            {
                if (ev.Player.CurrentItem != null)
                {
                    ev.Player.RemoveHeldItem();
                }
                message += _tr.CoinBreaksMessage;
            }

            if (message != null)
            {
                SendBroadcast(ev.Player, message);
            }
        }
        
        //removing default coins
        public void OnSpawningItem(SpawningItemEventArgs ev)
        {
            if (Cfg.DefaultCoinsAmount != 0 && ev.Pickup.Type == ItemType.Coin)
            {
                Log.Debug($"Removed a coin, coins left to remove {Cfg.DefaultCoinsAmount}");
                ev.IsAllowed = false;
                Cfg.DefaultCoinsAmount--;
            }
        }

        //removing locker spawning coins and replacing the chosen item in SCP pedestals
        public void OnFillingLocker(FillingLockerEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.Coin && Cfg.DefaultCoinsAmount != 0)
            {
                Log.Debug($"Removed a locker coin, coins left to remove {Cfg.DefaultCoinsAmount}");
                ev.IsAllowed = false;
                Cfg.DefaultCoinsAmount--;
            }
            else if (ev.Pickup.Type == Cfg.ItemToReplace.ElementAt(0).Key
                     && Cfg.ItemToReplace.ElementAt(0).Value != 0)
            {
                Log.Debug($"Placed a coin, coins left to place: {Cfg.ItemToReplace.ElementAt(0).Value}. Replaced item: {ev.Pickup.Type}");
                ev.IsAllowed = false;
                Pickup.CreateAndSpawn(ItemType.Coin, ev.Pickup.Position, new Quaternion());
                Cfg.ItemToReplace[Cfg.ItemToReplace.ElementAt(0).Key]--;
            }
        }
    }
}