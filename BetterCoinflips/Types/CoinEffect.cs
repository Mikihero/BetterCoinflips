using System;
using System.Collections.Generic;
using System.Linq;
using BetterCoinflips.Configs;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using PlayerRoles;
using Respawning;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace BetterCoinflips.Types
{
    public class CoinFlipEffect
    {
        private static Config Cfg => Plugin.Instance.Config;
        private static Configs.Translations Translations => Plugin.Instance.Translation;
        private static readonly System.Random Rd = new();
        
        public Action<Player> Execute { get; set; }
        public string Message { get; set; }

        public CoinFlipEffect(Action<Player> execute, string message)
        {
            Execute = execute;
            Message = message;
        }

        private static readonly Dictionary<string, string> _scpNames = new()
        {
            { "1 7 3", "SCP-173"},
            { "9 3 9", "SCP-939"},
            { "0 9 6", "SCP-096"},
            { "0 7 9", "SCP-079"},
            { "0 4 9", "SCP-049"},
            { "1 0 6", "SCP-106"}
        };
        
        private static bool flag1 = Cfg.RedCardChance > Rd.Next(1, 101);

        public static List<CoinFlipEffect> GoodEffects = new()
        {
            //0
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(flag1 ? ItemType.KeycardFacilityManager : ItemType.KeycardContainmentEngineer,
                    player.Position, new Quaternion());
            }, flag1 ? Translations.RedCardMessage : Translations.ContainmentEngineerCardMessage),

            //1
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.Medkit, player.Position, new Quaternion());
                Pickup.CreateAndSpawn(ItemType.Painkillers, player.Position, new Quaternion());
            }, Translations.MediKitMessage),

            //2
            new CoinFlipEffect(player =>
            {
                player.Teleport(Door.Get(DoorType.EscapeSecondary));
            }, Translations.TpToEscapeMessage),

            //3
            new CoinFlipEffect(player =>
            {
                player.Heal(25);
            }, Translations.MagicHealMessage),

            //4
            new CoinFlipEffect(player =>
            {
                player.Health *= 1.1f;

            }, Translations.HealthIncreaseMessage),

            //5
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP268, player.Position, new Quaternion());
            }, Translations.NeatHatMessage),

            //6
            new CoinFlipEffect(player =>
            {
                var effect = Cfg.GoodEffects.ToList().RandomItem();
                player.EnableEffect(effect, 5, true);
                Log.Debug($"Chosen random effect: {effect}");
            }, Translations.RandomGoodEffectMessage),

            //7
            new CoinFlipEffect(player =>
            {
                Firearm gun = (Firearm)Item.Create(ItemType.GunLogicer);
                gun.Ammo = 1;
                gun.CreatePickup(player.Position);
            }, Translations.OneAmmoLogicerMessage),

            //8
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP2176, player.Position, new Quaternion());
            }, Translations.LightbulbMessage),

            //9
            new CoinFlipEffect(player =>
            {
                Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                candy.CreatePickup(player.Position);
            }, Translations.PinkCandyMessage),

            //10
            new CoinFlipEffect(player =>
            {
                Firearm revo = (Firearm)Item.Create(ItemType.GunRevolver);
                revo.AddAttachment(new[]
                    { AttachmentName.CylinderMag8, AttachmentName.ShortBarrel, AttachmentName.ScopeSight });
                revo.CreatePickup(player.Position);
            }, Translations.BadRevoMessage),

            //11
            new CoinFlipEffect(player =>
            {
                MicroHIDPickup item = (MicroHIDPickup)Pickup.Create(ItemType.MicroHID);
                item.Position = player.Position;
                item.Spawn();
                item.Energy = 0;
            }, Translations.EmptyHidMessage),
            
            //12
            new CoinFlipEffect(player =>
            {
                Respawn.ForceWave(Respawn.NextKnownTeam == SpawnableTeamType.NineTailedFox ? SpawnableTeamType.NineTailedFox : SpawnableTeamType.ChaosInsurgency, true);
            }, Translations.ForceRespawnMessage),
            
            //13
            new CoinFlipEffect(player =>
            {
                player.Scale = new Vector3(1.13f, 0.5f, 1.13f);
            }, Translations.SizeChangeMessage),
            
            //14
            new CoinFlipEffect(player =>
            {
                Item.Create(Cfg.ItemsToGive.ToList().RandomItem()).CreatePickup(player.Position);
            }, Translations.RandomItemMessage),
        };

        public static List<CoinFlipEffect> BadEffects = new()
        {
            //0
            new CoinFlipEffect(player =>
            {
                if ((int)player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health *= 0.7f;
            }, Translations.HpReductionMessage),
            
            //1
            new CoinFlipEffect(player =>
            {
                player.Teleport(Door.Get(DoorType.PrisonDoor));

            }, Translations.TpToClassDCellsMessage),
            
            //2
            new CoinFlipEffect(player =>
            {
                var effect = Cfg.BadEffects.ToList().RandomItem();
                if (effect == EffectType.PocketCorroding)
                    player.EnableEffect(EffectType.PocketCorroding);
                else
                    player.EnableEffect(effect, 5, true);
                Log.Debug($"Chosen random effect: {effect}");
            }, Translations.RandomBadEffectMessage),
            
            //3
            new CoinFlipEffect(player =>
            {
                if (Warhead.IsDetonated || !Warhead.IsInProgress)
                    Warhead.Start();
                else
                    Warhead.Stop();
            }, Warhead.IsDetonated || !Warhead.IsInProgress ? Translations.WarheadStartMessage : Translations.WarheadStopMessage),
            
            //4
            new CoinFlipEffect(player =>
            {
                Map.TurnOffAllLights(Cfg.MapBlackoutTime);
            }, Translations.LightsOutMessage),
            
            //5
            new CoinFlipEffect(player =>
            {
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = (float)Cfg.LiveGrenadeFuseTime;
                grenade.SpawnActive(player.Position + Vector3.up, player);
            }, Translations.LiveGrenadeMessage),
            
            //6
            new CoinFlipEffect(player =>
            {
                FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                flash.FuseTime = 1f;
                flash.SpawnActive(player.Position);
            }, Translations.TrollFlashMessage),
            
            //7
            new CoinFlipEffect(player =>
            {
                if (Player.Get(Side.Scp).Any(x => x.Role.Type != RoleTypeId.Scp079))
                {
                    Player scpPlayer = Player.Get(Side.Scp).ToList().RandomItem();
                    player.Position = scpPlayer.Position;
                }
                else
                {
                    player.Health -= 15;
                    if (player.Health < 0) 
                        player.Kill(DamageType.Unknown);
                }
            }, Player.Get(Side.Scp).Any(x => x.Role.Type != RoleTypeId.Scp079) ? Translations.TpToRandomScpMessage : Translations.SmallDamageMessage),
            
            //8
            new CoinFlipEffect(player =>
            {
                if ((int)player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health = 1;
            }, Translations.HugeDamageMessage),
            
            //9
            new CoinFlipEffect(player =>
            {
                Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                vase.Primed = true;
                vase.CreatePickup(player.Position);
            }, Translations.PrimedVaseMessage),
            
            //10
            new CoinFlipEffect(player =>
            {
                player.PlaceTantrum();
            }, Translations.ShitPantsMessage),
            
            //11
            new CoinFlipEffect(player =>
            {
                var scpName = _scpNames.ToList().RandomItem();
                Cassie.MessageTranslated($"scp {scpName.Key} successfully terminated by automatic security system",$"{scpName.Value} successfully terminated by Automatic Security System.");
            }, Translations.FakeScpKillMessage),
            
            //12
            new CoinFlipEffect(player =>
            {
                player.DropItems();
                player.Scale = new(1, 1, 1);
                var randomScp = Cfg.ValidScps.ToList().RandomItem();
                player.Role.Set(randomScp, RoleSpawnFlags.AssignInventory);
                if (player.CurrentRoom.Type == RoomType.Pocket)
                    player.EnableEffect(EffectType.PocketCorroding);   
            }, Translations.TurnIntoScpMessage),
            
            //13
            new CoinFlipEffect(player =>
            {
                player.DropHeldItem();
                player.ClearInventory();
            }, Translations.InventoryResetMessage),
            
            //14
            new CoinFlipEffect(player =>
            {
                player.DropItems();
                switch (player.Role.Type)
                {
                    case RoleTypeId.Scientist:
                        player.Role.Set(RoleTypeId.ClassD, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.ClassD:
                        player.Role.Set(RoleTypeId.Scientist, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.ChaosConscript:
                    case RoleTypeId.ChaosRifleman:
                        player.Role.Set(RoleTypeId.NtfSergeant, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.ChaosMarauder:
                    case RoleTypeId.ChaosRepressor:
                        player.Role.Set(RoleTypeId.NtfCaptain, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.FacilityGuard:
                        player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.NtfPrivate:
                    case RoleTypeId.NtfSergeant:
                    case RoleTypeId.NtfSpecialist:
                        player.Role.Set(RoleTypeId.ChaosRifleman, RoleSpawnFlags.AssignInventory);
                        break;
                    case RoleTypeId.NtfCaptain:
                        List<RoleTypeId> roles = new List<RoleTypeId>
                        {
                            RoleTypeId.ChaosMarauder,
                            RoleTypeId.ChaosRepressor
                        };
                        player.Role.Set(roles.RandomItem(), RoleSpawnFlags.AssignInventory);
                        break;
                }

                if (player.CurrentRoom.Type == RoomType.Pocket)
                {
                    player.EnableEffect(EffectType.PocketCorroding);
                }
            }, Translations.ClassSwapMessage),
            
            //15
            new CoinFlipEffect(player =>
            {
                ExplosiveGrenade instaBoom = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                instaBoom.FuseTime = 0.1f;
                instaBoom.SpawnActive(player.Position, player);
            }, Translations.InstantExplosionMessage),
            
            //16
            new CoinFlipEffect(player =>
            {
                var playerList = Player.List.Where(x => !Cfg.IgnoredRoles.Contains(x.Role.Type)).ToList();
                playerList.Remove(player);
                if (playerList.IsEmpty())
                {
                    return;
                }
                var targetPlayer = playerList.RandomItem();
                var pos = targetPlayer.Position;
                targetPlayer.Teleport(player.Position);
                player.Teleport(pos);
            }, Player.List.Where(x => x.Role.Type != RoleTypeId.Spectator).IsEmpty() ? Translations.PlayerSwapIfOneAliveMessage : Translations.PlayerSwapMessage),
            
            //17
            new CoinFlipEffect(player =>
            {
                Timing.CallDelayed(1f, () => player.Kick(Cfg.KickReason));
            }, Translations.KickMessage),
            
            //18
            new CoinFlipEffect(player =>
            {
                var spectList = Player.List.Where(x => x.Role.Type == RoleTypeId.Spectator).ToList();
                if (spectList.IsEmpty())
                {
                    return;
                }
                var spect = spectList.RandomItem();
                spect.Role.Set(player.Role.Type, RoleSpawnFlags.None);
                spect.Teleport(player);
                spect.Health = player.Health;
                List<ItemType> playerItems = player.Items.Select(item => item.Type).ToList();

                foreach (var item in playerItems)
                {
                    spect.AddItem(item);
                }

                for (int i = 0; i < player.Ammo.Count; i++)
                {
                    spect.AddAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), player.Ammo.ElementAt(i).Value);
                    player.SetAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), 0);
                }

                player.ClearInventory();
                player.Role.Set(RoleTypeId.Spectator);
                
                EventHandlers.SendBroadcast(spect, Translations.SpectSwapSpectMessage);
            }, Player.List.Where(x => x.Role.Type == RoleTypeId.Spectator).IsEmpty() ? Translations.SpectSwapNoSpectsMessage : Translations.SpectSwapPlayerMessage),
            
            //19
            new CoinFlipEffect(player =>
            {
                player.DropHeldItem();
                player.Teleport(Exiled.API.Features.TeslaGate.List.ToList().RandomItem());
            }, Translations.TeslaTpMessage),
            
            //20
            new CoinFlipEffect(player =>
            {
                var target = Player.List.Where(x => x != player).ToList().RandomItem();
                
                //saving items
                List<ItemType> items1 = new();
                List<ItemType> items2 = new();
                foreach (var item in player.Items)
                {
                    items1.Add(item.Type);
                }
                foreach (var item in target.Items)
                {
                    items2.Add(item.Type);
                }
                
                //saving ammo
                Dictionary<AmmoType, ushort> ammo1 = new();
                Dictionary<AmmoType, ushort> ammo2 = new();
                for (int i = 0; i < player.Ammo.Count; i++)
                {
                    ammo1.Add(player.Ammo.ElementAt(i).Key.GetAmmoType(), player.Ammo.ElementAt(i).Value);
                    player.SetAmmo(ammo1.ElementAt(i).Key, 0);
                }
                for (int i = 0; i < target.Ammo.Count; i++)
                {
                    ammo2.Add(target.Ammo.ElementAt(i).Key.GetAmmoType(), target.Ammo.ElementAt(i).Value);
                    target.SetAmmo(ammo2.ElementAt(i).Key, 0);
                }

                //giving items
                target.ResetInventory(items1);
                player.ResetInventory(items2);
                
                //giving ammo
                foreach (var ammo in ammo2)
                {
                    player.SetAmmo(ammo.Key, ammo.Value);
                }
                foreach (var ammo in ammo1)
                {
                    target.SetAmmo(ammo.Key, ammo.Value);
                }
                
                EventHandlers.SendBroadcast(target, Translations.InventorySwapMessage);
            }, Translations.InventorySwapMessage),
            
            //21
            new CoinFlipEffect(player =>
            {
                if (Warhead.IsDetonated)
                {
                    Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                    candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Red);
                    candy.CreatePickup(player.Position);
                }
                else
                {
                    player.RandomTeleport<Room>();
                }
                
            }, Warhead.IsDetonated ? Translations.RandomTeleportWarheadDetonatedMessage : Translations.RandomTeleportMessage),
            
            //22
            new CoinFlipEffect(player =>
            {
                player.Handcuff();
                player.DropItems();
            }, Translations.HandcuffMessage),
        };
    }
}