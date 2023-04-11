using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using System.Linq;
using Exiled.API.Features.Pickups;
using Exiled.API.Features.Pickups.Projectiles;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables;
using PlayerRoles;
using UnityEngine;

namespace BetterCoinflips
{

    public class EventHandlers
    {
        private Config _cfg = Plugin.Instance.Config;
        System.Random rd = new System.Random();
        private int test = 0;

        public void SpawnCoin(Vector3 pos)
        {
            Pickup.CreateAndSpawn(ItemType.Coin, pos, new Quaternion(0,0,0,0));
        }

        public void SendBroadcast(Player pl, string message)
        {
            pl.Broadcast(_cfg.BroadcastTime, message);
        }

        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            Log.Debug($"Is tails: {ev.IsTails}");
            if (!ev.IsTails)
            {
                Dictionary<int, int> effectChances = new Dictionary<int, int>
                {
                    {1, _cfg.KeycardEffectChance},
                    {2, _cfg.MedicalKitEffectChance},
                    {3, _cfg.TPToEscapeEffectChance},
                    {4, _cfg.HealEffectChance},
                    {5, _cfg.MoreHPEffectChance},
                    {6, _cfg.HatEffectChance},
                    {7, _cfg.RandomGoodEffectChance},
                    {8, _cfg.OneAmmoLogicerEffectChance},
                    {9, _cfg.LightbulbEffectChance}
                };
                int totalChance = effectChances.Values.Sum();
                int randomNum = rd.Next(1, totalChance + 1);
                int headsEvent = 1; // Set a default value for headsEvent

                foreach (KeyValuePair<int, int> kvp in effectChances)
                {
                    if (randomNum <= kvp.Value)
                    {
                        headsEvent = kvp.Key;
                        break;
                    }
                    randomNum -= kvp.Value;
                }
                Log.Debug($"headsEvent = {headsEvent}");

                switch (headsEvent) //TODO: Add effects: spawning 173 shit, 
                {
                    case 1:
                        if(_cfg.RedCardChance > rd.Next(1, 101))
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardFacilityManager, ev.Player.Position, new Quaternion(0,0,0,0));
                            SendBroadcast(ev.Player, _cfg.RedCardMessage);
                        } 
                        else
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardContainmentEngineer, ev.Player.Position, new Quaternion(0,0,0,0));
                            SendBroadcast(ev.Player, _cfg.ContainmentEngineerCardMessage);
                        }
                        
                        break;
                    case 2:
                        Pickup.CreateAndSpawn(ItemType.Medkit, ev.Player.Position, new Quaternion(0,0,0,0));
                        Pickup.CreateAndSpawn(ItemType.Painkillers, ev.Player.Position, new Quaternion(0,0,0,0));
                        SendBroadcast(ev.Player, _cfg.MediKitMessage);
                        break;
                    case 3:
                        ev.Player.Teleport(Door.Get(DoorType.EscapeSecondary));
                        SendBroadcast(ev.Player, _cfg.TpToEscapeMessage);
                        break;
                    case 4:
                        ev.Player.Heal(25);
                        SendBroadcast(ev.Player, _cfg.MagicHealMessage);
                        break;
                    case 5:
                        ev.Player.Health *= 1.1f;
                        SendBroadcast(ev.Player, _cfg.HealthIncreaseMessage);
                        break;
                    case 6:
                        Pickup.CreateAndSpawn(ItemType.SCP268, ev.Player.Position, new Quaternion(0,0,0,0));
                        SendBroadcast(ev.Player, _cfg.NeatHatMessage);
                        break;
                    case 7:
                        ev.Player.EnableEffect(_cfg.GoodEffects.ToList().RandomItem(), 5, true);
                        SendBroadcast(ev.Player, _cfg.RandomGoodEffectMessage);
                        break;
                    case 8:
                        Item gun = Item.Create(ItemType.GunLogicer);
                        Firearm f = gun as Firearm;
                        f.Ammo = 1;
                        f.CreatePickup(ev.Player.Position);
                        SendBroadcast(ev.Player, _cfg.OneAmmoLogicerMessage);
                        break;
                    default:
                        Pickup.CreateAndSpawn(ItemType.SCP2176, ev.Player.Position, new Quaternion(0,0,0,0)); //generates an error for some reason
                        SendBroadcast(ev.Player, _cfg.LightbulbMessage);
                        break;
                        /*case 9:
                            Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                            candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                            candy.DropCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink, false, false, true, InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                            SendBroadcast(ev.Player, "test");
                            break;*/
                }
            }
            if (ev.IsTails)
            {
                Dictionary<int, int> effectChances = new Dictionary<int, int>
                {
                    {1, _cfg.HpReductionEffectChance},
                    {2, _cfg.TPToClassDCellsEffectChance},
                    {3, _cfg.RandomBadEffectChance},
                    {4, _cfg.WarheadEffectChance},
                    {5, _cfg.LightsOutEffectChance},
                    {6, _cfg.LiveHEEffectChance},
                    {7, _cfg.TrollGunEffectChance},
                    {8, _cfg.TrollFlashEffectChance},
                    {9, _cfg.SCPTpEffectChance},
                    {10, _cfg.OneHPLeftEffectChance},
                    {11, _cfg.FakeCassieEffectChance}
                };
                int totalChance = effectChances.Values.Sum();
                int randomNum = rd.Next(1, totalChance + 1);
                int tailsEvent = 1; // Set a default value for headsEvent

                foreach (KeyValuePair<int, int> kvp in effectChances)
                {
                    if (randomNum <= kvp.Value)
                    {
                        tailsEvent = kvp.Key;
                        break;
                    }
                    randomNum -= kvp.Value;
                }
                Log.Debug($"tailsEvent = {tailsEvent}");
                
                switch (tailsEvent)
                {
                    case 1:
                        ev.Player.Health *= 0.7f;
                        SendBroadcast(ev.Player, _cfg.HPReductionMessage);
                        break;
                    case 2:
                        ev.Player.Teleport(Door.Get(DoorType.PrisonDoor));
                        SendBroadcast(ev.Player, _cfg.TPToClassDCellsMessage);
                        break;
                    case 3:
                        ev.Player.EnableEffect(_cfg.BadEffects.ToList().RandomItem(), 5, true);
                        SendBroadcast(ev.Player, _cfg.RandomBadEffectMessage);
                        break;
                    case 4:
                        if (!Warhead.IsDetonated)
                        {
                            if (Warhead.IsInProgress)
                            {
                                Warhead.Stop();
                                SendBroadcast(ev.Player, _cfg.WarheadStopMessage);
                            }
                            else
                            {
                                Warhead.Start();
                                SendBroadcast(ev.Player, _cfg.WarheadStartMessage);
                            }
                        }
                        else
                        {
                            Warhead.Start();
                            SendBroadcast(ev.Player, _cfg.WarheadStartMessage);
                        }
                        break;
                    case 5:
                        Map.TurnOffAllLights(_cfg.MapBlackoutTime);
                        SendBroadcast(ev.Player, _cfg.LightsOutMessage);
                        break;
                    case 6:
                        ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                        grenade.FuseTime = (float)_cfg.LiveGrenadeFuseTime;
                        grenade.SpawnActive(ev.Player.Position + Vector3.up);
                        SendBroadcast(ev.Player, _cfg.LiveGrenadeMessage);
                        
                        
                        break;
                    case 7:
                        Item gun2 = Item.Create(ItemType.ParticleDisruptor);
                        Firearm f2 = gun2 as Firearm;
                        f2.Ammo = 0;
                        f2.CreatePickup(ev.Player.Position);
                        SendBroadcast(ev.Player, _cfg.TrollGunMessage);
                        break;
                    case 8:
                        FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                        flash.FuseTime = 1f;
                        flash.SpawnActive(ev.Player.Position);
                        SendBroadcast(ev.Player, _cfg.TrollFlashMessage);
                        break;
                    case 9:
                        if(Player.Get(Side.Scp).Any())
                        {
                            Player scpPlayer = Player.Get(Side.Scp).Where(p => p.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
                            ev.Player.Position = scpPlayer.Position;
                            SendBroadcast(ev.Player, _cfg.TPToRandomSCPMessage);
                        }
                        else
                        {
                            ev.Player.Health -= 15;
                            if (ev.Player.Health < 0) ev.Player.Kill(DamageType.Unknown);
                            SendBroadcast(ev.Player, _cfg.SmallDamageMessage);
                        }
                        break;
                    case 10:
                        ev.Player.Hurt(ev.Player.Health - 1);
                        SendBroadcast(ev.Player, _cfg.HugeDamageMessage);
                        break;
                    default:
                        Cassie.MessageTranslated("scp 1 7 3 successfully terminated by automatic security system", "SCP-173 successfully terminated by Automatic Security System.");
                        SendBroadcast(ev.Player, _cfg.FakeSCPKillMessage);
                        break;
                    /*case 13:
                        Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                        vase.Primed = true;
                        vase.Spawn(ev.Player.Position);
                        break;*/
                }
            }
            if(_cfg.RemoveCoinOnThrow)
            {
                ev.Player.RemoveHeldItem();
            }
        }
        
        public void OnSpawningItem(SpawningItemEventArgs ev)
        {
            Log.Info("event fired");
            Log.Info(ev.Pickup.Type);
            /*
            if (ev.Pickup.Type == ItemType.Coin/* && !_cfg.SpawnDefaultCoins#1#)
            {
                Log.Info(ev.Pickup.Type);
            }
            if (ev.Pickup.Type == ItemType.SCP1853/* && _cfg.Replace1853#1#)
            {
                Log.Info(ev.Pickup.Type);
                // SpawnCoin(ev.Pickup.GameObject.transform.position);
            }

            if (ev.Pickup.Type == ItemType.SCP500/* && test == 0#1#)
            {
                // SpawnCoin(ev.Pickup.GameObject.transform.position);
                Log.Info(ev.Pickup.Type);
            }
            */
            
        }

        /*public void OnRoundStart()
        {
            foreach (var pickup in Pickup.List)
            {
                if (pickup.Type == ItemType.Coin)
                {
                    pickup.Destroy();
                    Log.Info("ratio coin");
                }
            }

            foreach (var pickup in Pickup.List)
            {
                if (pickup.Type == ItemType.Coin)
                {
                    Log.Info(pickup.Room + " how in the fuck");
                }
            }
        }*/
    }
}