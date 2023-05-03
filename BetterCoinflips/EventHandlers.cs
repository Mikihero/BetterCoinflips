using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;
using BetterCoinflips.Configs;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using PlayerRoles;
using UnityEngine;
using Firearm = Exiled.API.Features.Items.Firearm;

namespace BetterCoinflips
{
    public class EventHandlers
    {
        private readonly Config _cfg = Plugin.Instance.Config;
        private readonly Configs.Translations _tr = Plugin.Instance.Translation;
        private readonly System.Random _rd = new();
        private readonly Dictionary<string, string> _scpNames = new()
        {
            { "1 7 3", "SCP-173"},
            { "9 3 9", "SCP-939"},
            { "0 9 6", "SCP-096"},
            { "0 7 9", "SCP-079"},
            { "0 4 9", "SCP-049"},
            { "1 0 6", "SCP-106"}
        };
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public static Dictionary<ushort, int> CoinUses = new();
        
        private void SendBroadcast(Player pl, string message) => pl.Broadcast(_cfg.BroadcastTime, message);

        public void OnCoinFlip(FlippingCoinEventArgs ev)
        {
            string message = "";
            Log.Debug($"Is tails: {ev.IsTails}");
            if (!ev.IsTails)
            {
                Dictionary<int, int> effectChances = new Dictionary<int, int>
                {
                    { 1, _cfg.KeycardEffectChance },
                    { 2, _cfg.MedicalKitEffectChance },
                    { 3, _cfg.TpToEscapeEffectChance },
                    { 4, _cfg.HealEffectChance },
                    { 5, _cfg.MoreHpEffectChance },
                    { 6, _cfg.HatEffectChance },
                    { 7, _cfg.RandomGoodEffectChance },
                    { 8, _cfg.OneAmmoLogicerEffectChance },
                    { 9, _cfg.LightbulbEffectChance },
                    { 10, _cfg.PinkCandyEffectChance },
                    { 11, _cfg.EmptyHidEffectChance },
                    { 12, _cfg.BadRevoEffectChance },
                    
                };
                int totalChance = effectChances.Values.Sum();
                int randomNum = _rd.Next(1, totalChance + 1);
                int headsEvent = 2; // Set a default value for headsEvent

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

                switch (headsEvent)
                {
                    case 1:
                        if (_cfg.RedCardChance > _rd.Next(1, 101))
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardFacilityManager, ev.Player.Position, new Quaternion());
                            message = _tr.RedCardMessage;
                        }
                        else
                        {
                            Pickup.CreateAndSpawn(ItemType.KeycardContainmentEngineer, ev.Player.Position, new Quaternion());
                            message = _tr.ContainmentEngineerCardMessage;
                        }
                        break;
                    case 2:
                        Pickup.CreateAndSpawn(ItemType.Medkit, ev.Player.Position, new Quaternion());
                        Pickup.CreateAndSpawn(ItemType.Painkillers, ev.Player.Position, new Quaternion());
                        message = _tr.MediKitMessage;
                        break;
                    case 3:
                        ev.Player.Teleport(Door.Get(DoorType.EscapeSecondary));
                        message = _tr.TpToEscapeMessage;
                        break;
                    case 4:
                        ev.Player.Heal(25);
                        message = _tr.MagicHealMessage;
                        break;
                    case 5:
                        ev.Player.Health *= 1.1f;
                        message = _tr.HealthIncreaseMessage;
                        break;
                    case 6:
                        Pickup.CreateAndSpawn(ItemType.SCP268, ev.Player.Position, new Quaternion());
                        message = _tr.NeatHatMessage;
                        break;
                    case 7:
                        var effect = _cfg.GoodEffects.ToList().RandomItem();
                        ev.Player.EnableEffect(effect, 5, true);
                        Log.Debug($"Chosen random effect: {effect}");
                        message = _tr.RandomGoodEffectMessage;
                        break;
                    case 8:
                        Firearm gun = (Firearm)Item.Create(ItemType.GunLogicer);
                        gun.Ammo = 1;
                        gun.CreatePickup(ev.Player.Position);
                        message = _tr.OneAmmoLogicerMessage;
                        break;
                    case 9:
                        Pickup.CreateAndSpawn(ItemType.SCP2176, ev.Player.Position, new Quaternion());
                        message = _tr.LightbulbMessage;
                        break;
                    case 10:
                        Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                        candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                        candy.CreatePickup(ev.Player.Position);
                        message = _tr.PinkCandyMessage;
                        break;
                    case 11:
                        MicroHid hid = (MicroHid)Item.Create(ItemType.MicroHID);
                        hid.Energy = 0;
                        hid.CreatePickup(ev.Player.Position);
                        message = _tr.EmptyHidMessage;
                        break;    
                    case 12:
                        Firearm revo = (Firearm)Item.Create(ItemType.GunRevolver);
                        revo.AddAttachment(AttachmentName.CylinderMag8);
                        revo.AddAttachment(AttachmentName.ShortBarrel);
                        revo.AddAttachment(AttachmentName.ScopeSight);
                        revo.CreatePickup(ev.Player.Position);
                        message = _tr.BadRevoMessage;
                        break;
                }
            }
            if (ev.IsTails)
            {
                Dictionary<int, int> effectChances = new Dictionary<int, int>
                {
                    { 1, _cfg.HpReductionEffectChance },
                    { 2, _cfg.TpToClassDCellsEffectChance },
                    { 3, _cfg.RandomBadEffectChance },
                    { 4, _cfg.WarheadEffectChance },
                    { 5, _cfg.LightsOutEffectChance },
                    { 6, _cfg.LiveHeEffectChance },
                    { 7, _cfg.TrollGunEffectChance },
                    { 8, _cfg.TrollFlashEffectChance },
                    { 9, _cfg.ScpTpEffectChance },
                    { 10, _cfg.OneHpLeftEffectChance },
                    { 11, _cfg.PrimedVaseEffectChance},
                    { 12, _cfg.ShitPantsEffectChance },
                    { 13, _cfg.FakeCassieEffectChance },
                    { 14, _cfg.ZombieFcEffectChance },
                    { 15, _cfg.InventoryResetEffectChance },
                    { 16, _cfg.ClassSwapEffectChance },
                };
                int totalChance = effectChances.Values.Sum();
                int randomNum = _rd.Next(1, totalChance + 1);
                int tailsEvent = 13; // Set a default value for headsEvent

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
                        if ((int)ev.Player.Health == 1)
                            ev.Player.Kill(DamageType.CardiacArrest);
                        else
                            ev.Player.Health *= 0.7f;
                        message = _tr.HpReductionMessage;
                        break;
                    case 2:
                        ev.Player.Teleport(Door.Get(DoorType.PrisonDoor));
                        message = _tr.TpToClassDCellsMessage;
                        break;
                    case 3:
                        var effect = _cfg.BadEffects.ToList().RandomItem();
                        ev.Player.EnableEffect(effect, 5, true);
                        Log.Debug($"Chosen random effect: {effect}");
                        message = _tr.RandomBadEffectMessage;
                        break;
                    case 4:
                        if (!Warhead.IsDetonated)
                        {
                            if (Warhead.IsInProgress)
                            {
                                Warhead.Stop();
                                message = _tr.WarheadStopMessage;
                            }
                            else
                            {
                                Warhead.Start();
                                message = _tr.WarheadStartMessage;
                            }
                        }
                        else
                        {
                            Warhead.Start();
                            message = _tr.WarheadStartMessage;
                        }
                        break;
                    case 5:
                        Map.TurnOffAllLights(_cfg.MapBlackoutTime);
                        message = _tr.LightsOutMessage;
                        break;
                    case 6:
                        ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                        grenade.FuseTime = (float)_cfg.LiveGrenadeFuseTime;
                        grenade.SpawnActive(ev.Player.Position + Vector3.up);
                        message = _tr.LiveGrenadeMessage;
                        break;
                    case 7:
                        Firearm gun = (Firearm)Item.Create(ItemType.ParticleDisruptor);
                        gun.Ammo = 0;
                        gun.CreatePickup(ev.Player.Position);
                        message = _tr.TrollGunMessage;
                        break;
                    case 8:
                        FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                        flash.FuseTime = 1f;
                        flash.SpawnActive(ev.Player.Position);
                        message = _tr.TrollFlashMessage;
                        break;
                    case 9:
                        if (Player.Get(Side.Scp).Any())
                        {
                            Player scpPlayer = Player.Get(Side.Scp).Where(p => p.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
                            ev.Player.Position = scpPlayer.Position;
                            message = _tr.TpToRandomScpMessage;
                        }
                        else
                        {
                            ev.Player.Health -= 15;
                            if (ev.Player.Health < 0) ev.Player.Kill(DamageType.Unknown);
                            message = _tr.SmallDamageMessage;
                        }
                        break;
                    case 10:
                        if ((int)ev.Player.Health == 1)
                            ev.Player.Kill(DamageType.CardiacArrest);
                        else
                            ev.Player.Health = 1;
                        message = _tr.HugeDamageMessage;
                        break;
                    case 11:
                        Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                        vase.Primed = true;
                        vase.CreatePickup(ev.Player.Position);
                        message = _tr.PrimedVaseMessage;
                        break;
                    case 12:
                        ev.Player.PlaceTantrum();
                        message = _tr.ShitPantsMessage;
                        break;
                    case 13:
                        var scpName = _scpNames.ToList().RandomItem();
                        Cassie.MessageTranslated($"scp {scpName.Key} successfully terminated by automatic security system",$"{scpName.Value} successfully terminated by Automatic Security System.");
                        message = _tr.FakeScpKillMessage;
                        break;
                    case 14:
                        ev.Player.Role.Set(RoleTypeId.Scp0492, RoleSpawnFlags.None);
                        message = _tr.ZombieFcMessage;
                        break;
                    case 15:
                        ev.Player.DropHeldItem();
                        ev.Player.ResetInventory(new ItemType[] {});
                        message = _tr.InventoryResetMessage;
                        break;
                    case 16:
                        ev.Player.DropHeldItem();
                        switch (ev.Player.Role.Type)
                        {
                            case RoleTypeId.Scientist:
                                ev.Player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.ClassD:
                                ev.Player.Role.Set(RoleTypeId.Scientist, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.ChaosConscript:
                            case RoleTypeId.ChaosRifleman:
                                ev.Player.Role.Set(RoleTypeId.NtfSergeant, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.ChaosMarauder:
                            case RoleTypeId.ChaosRepressor:
                                ev.Player.Role.Set(RoleTypeId.NtfCaptain, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.FacilityGuard:
                                ev.Player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.NtfPrivate:
                            case RoleTypeId.NtfSergeant:
                            case RoleTypeId.NtfSpecialist:
                                ev.Player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                                break;
                            case RoleTypeId.NtfCaptain:
                                List<RoleTypeId> roles = new List<RoleTypeId>
                                {
                                    RoleTypeId.ChaosMarauder,
                                    RoleTypeId.ChaosRepressor
                                };
                                ev.Player.Role.Set(roles.RandomItem(), RoleSpawnFlags.AssignInventory);
                                break;
                        }
                        message = _tr.ClassSwapMessage;
                        break;
                }
            }
            
            if (!CoinUses.ContainsKey(ev.Player.CurrentItem.Serial))
            {
                CoinUses.Add(ev.Player.CurrentItem.Serial, _rd.Next(_cfg.MinMaxDefaultCoins[0], _cfg.MinMaxDefaultCoins[1]));
                Log.Debug($"Added a coin, Uses Left: {CoinUses[ev.Player.CurrentItem.Serial]}");
            }
            else
            {
                CoinUses[ev.Player.CurrentItem.Serial]--;
                Log.Debug($"Uses Left: {CoinUses[ev.Player.CurrentItem.Serial]}");
            }
            if (CoinUses[ev.Player.CurrentItem.Serial] < 1)
            {
                CoinUses.Remove(ev.Player.CurrentItem.Serial);
                ev.Player.RemoveHeldItem();
                message += _tr.CoinBreaksMessage;
                Log.Debug("Removed the coin");
            }
            SendBroadcast(ev.Player, message);
        }
        

        public void OnSpawningItem(SpawningItemEventArgs ev)
        {
            if (_cfg.DefaultCoinsAmount != 0 && ev.Pickup.Type == ItemType.Coin)
            {
                ev.IsAllowed = false;
                _cfg.DefaultCoinsAmount--;
            }
        }

        public void OnInteractingDoorEventArgs(InteractingDoorEventArgs ev)
        {
            foreach (Pickup pickup in ev.Door.Room.Pickups)
            {
                if (pickup == null)
                {
                    return;
                }
                if (pickup.IsLocked && pickup.Type == _cfg.ItemToReplace.ElementAt(0).Key && _cfg.ItemToReplace.ElementAt(0).Key != ItemType.None && pickup.Type == _cfg.ItemToReplace.ElementAt(0).Key && _cfg.ItemToReplace.ElementAt(0).Value != 0)
                {
                    pickup.Destroy();
                    Pickup.CreateAndSpawn(ItemType.Coin, pickup.RelativePosition.Position, new Quaternion());
                    _cfg.ItemToReplace[_cfg.ItemToReplace.ElementAt(0).Key]--;
                }
            }
        }
    }
}