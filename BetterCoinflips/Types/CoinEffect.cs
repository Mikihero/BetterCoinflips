using System;
using System.Collections.Generic;
using System.Linq;
using BetterCoinflips.Configs;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using InventorySystem.Items.Firearms.Attachments;
using PlayerRoles;
using Respawning;
using UnityEngine;
using Player = Exiled.API.Features.Player;

namespace BetterCoinflips.Types
{
    public class CoinFlipEffect
    {
        private static readonly Config _cfg = Plugin.Instance.Config;
        private static readonly Configs.Translations _tr = Plugin.Instance.Translation;
        private static readonly System.Random _rd = new();
        
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
        
        private static bool flag1 = _cfg.RedCardChance > _rd.Next(1, 101);

        public static List<CoinFlipEffect> GoodEffects = new()
        {
            //1
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(flag1 ? ItemType.KeycardFacilityManager : ItemType.KeycardContainmentEngineer,
                    player.Position, new Quaternion());
            }, flag1 ? _tr.RedCardMessage : _tr.ContainmentEngineerCardMessage),

            //2
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.Medkit, player.Position, new Quaternion());
                Pickup.CreateAndSpawn(ItemType.Painkillers, player.Position, new Quaternion());
            }, _tr.MediKitMessage),

            //3
            new CoinFlipEffect(player =>
            {
                player.Teleport(Door.Get(DoorType.EscapeSecondary));
            }, _tr.TpToEscapeMessage),

            //4
            new CoinFlipEffect(player =>
            {
                player.Heal(25);
            }, _tr.MagicHealMessage),

            //5
            new CoinFlipEffect(player =>
            {
                player.Health *= 1.1f;

            }, _tr.HealthIncreaseMessage),

            //6
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP268, player.Position, new Quaternion());
            }, _tr.NeatHatMessage),

            //7
            new CoinFlipEffect(player =>
            {
                var effect = _cfg.GoodEffects.ToList().RandomItem();
                player.EnableEffect(effect, 5, true);
                Log.Debug($"Chosen random effect: {effect}");
            }, _tr.RandomGoodEffectMessage),

            //8
            new CoinFlipEffect(player =>
            {
                Firearm gun = (Firearm)Item.Create(ItemType.GunLogicer);
                gun.Ammo = 1;
                gun.CreatePickup(player.Position);
            }, _tr.OneAmmoLogicerMessage),

            //9
            new CoinFlipEffect(player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP2176, player.Position, new Quaternion());
            }, _tr.LightbulbMessage),

            //10
            new CoinFlipEffect(player =>
            {
                Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                candy.CreatePickup(player.Position);
            }, _tr.PinkCandyMessage),

            //11
            new CoinFlipEffect(player =>
            {
                Firearm revo = (Firearm)Item.Create(ItemType.GunRevolver);
                revo.AddAttachment(new[]
                    { AttachmentName.CylinderMag8, AttachmentName.ShortBarrel, AttachmentName.ScopeSight });
                revo.CreatePickup(player.Position);
            }, _tr.BadRevoMessage),

            //12
            new CoinFlipEffect(player =>
            {
                MicroHIDPickup item = (MicroHIDPickup)Pickup.Create(ItemType.MicroHID);
                item.Position = player.Position;
                item.Spawn();
                item.Energy = 0;
            }, _tr.EmptyHidMessage),
            
            //13
            new CoinFlipEffect(player =>
            {
                if (Respawn.NextKnownTeam == SpawnableTeamType.NineTailedFox)
                    Respawn.ForceWave(SpawnableTeamType.NineTailedFox, true);
                else
                    Respawn.ForceWave(SpawnableTeamType.ChaosInsurgency, true);
            }, _tr.ForceRespawnMessage),
        };

        public static List<CoinFlipEffect> BadEffects = new()
        {
            //1
            new CoinFlipEffect(player =>
            {
                if ((int)player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health *= 0.7f;
            }, _tr.HpReductionMessage),
            
            //2
            new CoinFlipEffect(player =>
            {
                player.Teleport(Door.Get(DoorType.PrisonDoor));

            }, _tr.TpToClassDCellsMessage),
            
            //3
            new CoinFlipEffect(player =>
            {
                var effect = _cfg.BadEffects.ToList().RandomItem();
                if (effect == EffectType.PocketCorroding)
                    player.EnableEffect(EffectType.PocketCorroding);
                else
                    player.EnableEffect(effect, 5, true);
                Log.Debug($"Chosen random effect: {effect}");
            }, _tr.RandomBadEffectMessage),
            
            //4
            new CoinFlipEffect(player =>
            {
                if (Warhead.IsDetonated || !Warhead.IsInProgress)
                    Warhead.Start();
                else
                    Warhead.Stop();
            }, Warhead.IsDetonated || !Warhead.IsInProgress ? _tr.WarheadStartMessage : _tr.WarheadStopMessage),
            
            //5
            new CoinFlipEffect(player =>
            {
                Map.TurnOffAllLights(_cfg.MapBlackoutTime);
            }, _tr.LightsOutMessage),
            
            //6
            new CoinFlipEffect(player =>
            {
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = (float)_cfg.LiveGrenadeFuseTime;
                grenade.SpawnActive(player.Position + Vector3.up, player);
            }, _tr.LiveGrenadeMessage),
            
            //7
            new CoinFlipEffect(player =>
            {
                Firearm gun = (Firearm)Item.Create(ItemType.ParticleDisruptor);
                gun.Ammo = 0;
                gun.CreatePickup(player.Position);
            }, _tr.TrollGunMessage),
            
            //8
            new CoinFlipEffect(player =>
            {
                FlashGrenade flash = (FlashGrenade)Item.Create(ItemType.GrenadeFlash);
                flash.FuseTime = 1f;
                flash.SpawnActive(player.Position);
            }, _tr.TrollFlashMessage),
            
            //9
            new CoinFlipEffect(player =>
            {
                if (Player.Get(Side.Scp).Any())
                {
                    Player scpPlayer = Player.Get(Side.Scp).Where(p => p.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
                    player.Position = scpPlayer.Position;
                }
                else
                {
                    player.Health -= 15;
                    if (player.Health < 0) 
                        player.Kill(DamageType.Unknown);
                }
            }, Player.Get(Side.Scp).Any() ? _tr.TpToRandomScpMessage : _tr.SmallDamageMessage),
            
            //10
            new CoinFlipEffect(player =>
            {
                if ((int)player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health = 1;
            }, _tr.HugeDamageMessage),
            
            //11
            new CoinFlipEffect(player =>
            {
                Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                vase.Primed = true;
                vase.CreatePickup(player.Position);
            }, _tr.PrimedVaseMessage),
            
            //12
            new CoinFlipEffect(player =>
            {
                player.PlaceTantrum();
            }, _tr.ShitPantsMessage),
            
            //13
            new CoinFlipEffect(player =>
            {
                var scpName = _scpNames.ToList().RandomItem();
                Cassie.MessageTranslated($"scp {scpName.Key} successfully terminated by automatic security system",$"{scpName.Value} successfully terminated by Automatic Security System.");
            }, _tr.FakeScpKillMessage),
            
            //14
            new CoinFlipEffect(player =>
            {
                player.DropHeldItem();
                player.Role.Set(RoleTypeId.Scp0492, RoleSpawnFlags.AssignInventory);
                if (player.CurrentRoom.Type == RoomType.Pocket)
                    player.EnableEffect(EffectType.PocketCorroding);   
            }, _tr.ZombieFcMessage),
            
            //15
            new CoinFlipEffect(player =>
            {
                player.DropHeldItem();
                player.ResetInventory(new ItemType[] {});
            }, _tr.InventoryResetMessage),
            
            //16
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
            }, _tr.ClassSwapMessage),
            
            //17
            new CoinFlipEffect(player =>
            {
                ExplosiveGrenade instaBoom = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE);
                instaBoom.FuseTime = 0.1f;
                instaBoom.SpawnActive(player.Position, player);
            }, _tr.InstantExplosionMessage),
            
            //18
            new CoinFlipEffect(player =>
            {
                var playerList = Player.List.ToList();
                playerList.Remove(player);
                var targetPlayer = playerList.RandomItem();
                playerList.Remove(targetPlayer);
            }, _tr.PlayerSwapMessage),
            
            //19
            new CoinFlipEffect(player =>
            {
                
            } )
        };
    }
}