using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using System.Linq;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using UnityEngine;

namespace BetterCoinflips
{

    public class EventHandlers : Config
    {
        System.Random rd = new System.Random();
        
        public void SpawnCoin(Vector3 pos)
        {
            Pickup.CreateAndSpawn(ItemType.Coin, pos, new Quaternion(0,0,0,0));
        }

        public void SendBroadcast(Player pl, string message)
        {
            pl.Broadcast(BroadcastTime, message);
        }

        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            int headsEvent = 0;
            int tailsEvent = 0;
            if (!ev.IsTails)
            {
                if (KeycardEffectChance > rd.Next(1, 100)) headsEvent = 1;
                else if (MedicalKitEffectChance > rd.Next(1, 100)) headsEvent = 2;
                else if (TPToEscapeEffectChance > rd.Next(1, 100)) headsEvent = 3;
                else if (HealEffectChance > rd.Next(1, 100)) headsEvent = 4;
                else if (MoreHPEffectChance > rd.Next(1, 100)) headsEvent = 5;
                else if (HatEffectChance > rd.Next(1, 100)) headsEvent = 6;
                else if (RandomGoodEffectChance > rd.Next(1, 100)) headsEvent = 7;
                else if (OneAmmoLogicerEffectChance > rd.Next(1, 100)) headsEvent = 8; 
                else if (LightbulbEffectChance > rd.Next(1, 100)) headsEvent = 9; // doesn't have to exist for now, it is here if I want to expand the effects

                switch (headsEvent)
                {
                    case 1:
                        if(RedCardChance > rd.Next(1, 101))
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardFacilityManager, ev.Player.Position, new Quaternion(0,0,0,0));
                            SendBroadcast(ev.Player, RedCardMessage);
                        } 
                        else
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardContainmentEngineer, ev.Player.Position, new Quaternion(0,0,0,0));
                            SendBroadcast(ev.Player, ContainmentEngineerCardMessage);
                        }
                        
                        break;
                    case 2:
                        Pickup.CreateAndSpawn(ItemType.Medkit, ev.Player.Position, new Quaternion(0,0,0,0));
                        Pickup.CreateAndSpawn(ItemType.Painkillers, ev.Player.Position, new Quaternion(0,0,0,0));
                        SendBroadcast(ev.Player, MediKitMessage);
                        break;
                    case 3:
                        ev.Player.Teleport(Door.Get(DoorType.EscapeSecondary));
                        SendBroadcast(ev.Player, TpToEscapeMessage);
                        break;
                    case 4:
                        ev.Player.Heal(25);
                        SendBroadcast(ev.Player, MagicHealMessage);
                        break;
                    case 5:
                        ev.Player.Health *= 1.1f;
                        SendBroadcast(ev.Player, HealthIncreaseMessage);
                        break;
                    case 6:
                        Pickup.CreateAndSpawn(ItemType.SCP268, ev.Player.Position, new Quaternion(0,0,0,0));
                        SendBroadcast(ev.Player, NeatHatMessage);
                        break;
                    case 7:
                        ev.Player.EnableEffect(GoodEffects.ToList().RandomItem(), 5, true);
                        SendBroadcast(ev.Player, RandomGoodEffectMessage);
                        break;
                    case 8:
                        Item gun = Item.Create(ItemType.GunLogicer);
                        Firearm f = gun as Firearm;
                        f.Ammo = 1;
                        f.CreatePickup(ev.Player.Position);
                        SendBroadcast(ev.Player, OneAmmoLogicerMessage);
                        break;
                    default:
                        Pickup.CreateAndSpawn(ItemType.SCP2176, ev.Player.Position, new Quaternion(0,0,0,0));
                        SendBroadcast(ev.Player, LightbulbMessage);
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
                if (HpReductionEffectChance > rd.Next(1, 100)) tailsEvent = 1;
                else if (TPToClassDCellsEffectChance > rd.Next(1, 100)) tailsEvent = 2;
                else if (RandomBadEffectChance > rd.Next(1, 100)) tailsEvent = 3;
                else if (WarheadEffectChance > rd.Next(1, 100)) tailsEvent = 4;
                else if (LightsOutEffectChance > rd.Next(1, 100)) tailsEvent = 5;
                else if (LiveHEEffectChance > rd.Next(1, 100)) tailsEvent = 6;
                else if (TrollGunEffectChance > rd.Next(1, 100)) tailsEvent = 7;
                else if (TrollFlashEffectChance > rd.Next(1, 100)) tailsEvent = 8;
                else if (SCPTpEffectChance > rd.Next(1, 100)) tailsEvent = 9;
                else if (OneHPLeftEffectChance > rd.Next(1, 100)) tailsEvent = 10;
                else if (FakeCassieEffectChance > rd.Next(1, 100)) tailsEvent = 11;  // doesn't have to exist for now, it is here if I want to expand the effects

                switch (tailsEvent)
                {
                    case 1:
                        ev.Player.Health *= 0.7f;
                        SendBroadcast(ev.Player, HPReductionMessage);
                        break;
                    case 2:
                        ev.Player.Teleport(Door.Get(DoorType.PrisonDoor));
                        SendBroadcast(ev.Player, TPToClassDCellsMessage);
                        break;
                    case 3:
                        ev.Player.EnableEffect(BadEffects.ToList().RandomItem(), 5, true);
                        SendBroadcast(ev.Player, RandomBadEffectMessage);
                        break;
                    case 4:
                        if (!Warhead.IsDetonated)
                        {
                            if (Warhead.IsInProgress)
                            {
                                Warhead.Stop();
                                SendBroadcast(ev.Player, WarheadStopMessage);
                            }
                            else
                            {
                                Warhead.Start();
                                SendBroadcast(ev.Player, WarheadStartMessage);
                            }
                        }
                        else
                        {
                            Warhead.Start();
                            SendBroadcast(ev.Player, WarheadStartMessage);
                        }
                        break;
                    case 5:
                        Map.TurnOffAllLights(MapBlackoutTime);
                        SendBroadcast(ev.Player, LightsOutMessage);
                        break;
                    case 6:
                        ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                        grenade.FuseTime = LiveGrenadeFuseTime;
                        grenade.SpawnActive(ev.Player.Position + Vector3.up);
                        SendBroadcast(ev.Player, LiveGrenadeMessage);
                        break;
                    case 7:
                        Item gun2 = Item.Create(ItemType.ParticleDisruptor);
                        Firearm f2 = gun2 as Firearm;
                        f2.Ammo = 0;
                        f2.CreatePickup(ev.Player.Position);
                        SendBroadcast(ev.Player, TrollGunMessage);
                        break;
                    case 8:
                        FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                        flash.FuseTime = 1f;
                        flash.SpawnActive(ev.Player.Position);
                        SendBroadcast(ev.Player, TrollFlashMessage);
                        break;
                    case 9:
                        if(Player.Get(Side.Scp).Any())
                        {
                            Player scpPlayer = Player.Get(Side.Scp).Where(p => p.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
                            ev.Player.Position = scpPlayer.Position;
                            SendBroadcast(ev.Player, TPToRandomSCPMessage);
                        }
                        else
                        {
                            ev.Player.Health -= 15;
                            if (ev.Player.Health < 0) ev.Player.Kill(DamageType.Unknown);
                            SendBroadcast(ev.Player, SmallDamageMessage);
                        }
                        break;
                    case 10:
                        ev.Player.Hurt(ev.Player.Health - 1);
                        SendBroadcast(ev.Player, HugeDamageMessage);
                        break;
                    default:
                        Cassie.MessageTranslated("scp 1 7 3 successfully terminated by automatic security system", "SCP-173 successfully terminated by Automatic Security System.");
                        SendBroadcast(ev.Player, FakeSCPKillMessage);
                        break;
                    /*case 13:
                        Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                        vase.Primed = true;
                        vase.Spawn(ev.Player.Position);
                        break;*/
                }
            }
            if(RemoveCoinOnThrow)
            {
                ev.Player.RemoveHeldItem();
            }
        }

        public void OnSpawningItem(SpawningItemEventArgs ev)
        {
            if (ev.Pickup.Type == ItemType.Coin)
            {
                if(!SpawnDefaultCoins) ev.IsAllowed = false;
            }
            if (ev.Pickup.Type == ItemType.SCP1853)
            {
                if(Replace1853)
                {
                    ev.IsAllowed = false;
                    SpawnCoin(ev.Pickup.GameObject.transform.position);
                }
            }
        }
    }
}