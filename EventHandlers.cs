using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using System.Linq;
using UnityEngine;

namespace BetterCoinflips
{

    public class EventHandlers : Config
    {
        System.Random rd = new System.Random();
        public void SpawnCoin(Vector3 pos)
        {
            Item.Create(ItemType.Coin).Spawn(pos);
        }

        public void SendBroadcast(Player pl, string message)
        {
            pl.Broadcast(BroadcastTime, message);
        }

        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            int HeadsEvent = 0;
            int TailsEvent = 0;
            if (!ev.IsTails)
            {
                if (KeycardEffectChance > rd.Next(1, 100)) HeadsEvent = 1;
                else if (MedicalKitEffectChance > rd.Next(1, 100)) HeadsEvent = 2;
                else if (TPToEscapeEffectChance > rd.Next(1, 100)) HeadsEvent = 3;
                else if (HealEffectChance > rd.Next(1, 100)) HeadsEvent = 4;
                else if (MoreHPEffectChance > rd.Next(1, 100)) HeadsEvent = 5;
                else if (HatEffectChance > rd.Next(1, 100)) HeadsEvent = 6;
                else if (RandomGoodEffectChance > rd.Next(1, 100)) HeadsEvent = 7;
                else if (OneAmmoLogicerEffectChance > rd.Next(1, 100)) HeadsEvent = 8; 
                else if (LightbulbEffectChance > rd.Next(1, 100)) HeadsEvent = 9; // doesn't have to exist for now, it is here if I want to expand the effects

                switch (HeadsEvent)
                {
                    case 1:
                        if(RedCardChance > rd.Next(1, 101))
                        {
                            Item.Create(ItemType.KeycardFacilityManager).Spawn(ev.Player.Position);
                            SendBroadcast(ev.Player, RedCardMessage);
                        } 
                        else
                        {
                            Item.Create(ItemType.KeycardContainmentEngineer).Spawn(ev.Player.Position);
                            SendBroadcast(ev.Player, ContainmentEnginnerCardMessage);
                        }
                        
                        break;
                    case 2:
                        Item.Create(ItemType.Medkit).Spawn(ev.Player.Position);
                        Item.Create(ItemType.Painkillers).Spawn(ev.Player.Position);
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
                        Item.Create(ItemType.SCP268).Spawn(ev.Player.Position);
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
                        f.Spawn(ev.Player.Position);
                        SendBroadcast(ev.Player, OneAmmoLogicerMessage);
                        break;
                    case 9:
                    default:
                        Item.Create(ItemType.SCP2176).Spawn(ev.Player.Position);
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
                if (HpReductionEffectChance > rd.Next(1, 100)) TailsEvent = 1;
                else if (TPToClassDCellsEffectChance > rd.Next(1, 100)) TailsEvent = 2;
                else if (RandomBadEffectChance > rd.Next(1, 100)) TailsEvent = 3;
                else if (WarheadEffectChance > rd.Next(1, 100)) TailsEvent = 4;
                else if (LightsOutEffectChance > rd.Next(1, 100)) TailsEvent = 5;
                else if (LiveHEEffectChance > rd.Next(1, 100)) TailsEvent = 6;
                else if (TrollGunEffectChance > rd.Next(1, 100)) TailsEvent = 7;
                else if (TrollFlashEffectChance > rd.Next(1, 100)) TailsEvent = 8;
                else if (SCPTpEffectChance > rd.Next(1, 100)) TailsEvent = 9;
                else if (OneHPLeftEffectChance > rd.Next(1, 100)) TailsEvent = 10;
                else if (FakeCassieEffectChance > rd.Next(1, 100)) TailsEvent = 11;  // doesn't have to exist for now, it is here if I want to expand the effects

                switch (TailsEvent)
                {
                    case 1:
                        float hp = ev.Player.Health;
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
                        f2.Spawn(ev.Player.Position);
                        SendBroadcast(ev.Player, TrollGunMessage);
                        break;
                    case 8:
                        FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                        flash.FuseTime = 1f;
                        flash.SpawnActive(ev.Player.Position);
                        SendBroadcast(ev.Player, TrollFlashMessage);
                        break;
                    case 9:
                        if(Player.Get(Side.Scp).Count() > 0)
                        {
                            Player scpplayer = Player.Get(Side.Scp).Where(p => p.Role != RoleType.Scp079).ToList().RandomItem();
                            ev.Player.Position = scpplayer.Position;
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
                    case 11:
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
                ev.Player.RemoveHeldItem(true);
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