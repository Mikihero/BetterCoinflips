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
    public abstract class CoinEffect
    {
        public abstract List<(Func<bool>, string)> ConditionalMessages { get; set; }

        public abstract EffectType EffectType { get; set; }
        
        public abstract void OnExecute(Player player);




        private static readonly Dictionary<string, string> _scpNames = new()
        {
            { "1 7 3", "SCP-173"},
            { "9 3 9", "SCP-939"},
            { "0 9 6", "SCP-096"},
            { "0 7 9", "SCP-079"},
            { "0 4 9", "SCP-049"},
            { "1 0 6", "SCP-106"}
        };
        
        private static bool flag1 = Config.RedCardChance > Rd.Next(1, 101);

        // GoodEffects list
        public static List<CoinEffect> GoodEffects = new()
        {
            // 0: Spawns a red keycard or containment enginner
            new CoinEffect(flag1 ? Translations.RedCardMessage : Translations.ContainmentEngineerCardMessage, player =>
            {
                Pickup.CreateAndSpawn(flag1 ? ItemType.KeycardFacilityManager : ItemType.KeycardContainmentEngineer, player.Position, new Quaternion());
            }),

            // 1: Spawns a medkit and painkillers for the player.
            new CoinEffect(Translations.MediKitMessage, player =>
            {
                Pickup.CreateAndSpawn(ItemType.Medkit, player.Position, new Quaternion());
                Pickup.CreateAndSpawn(ItemType.Painkillers, player.Position, new Quaternion());
            }),

            // 2: Teleports the player to the escape secondary door.
            new CoinEffect(Translations.TpToEscapeMessage, player =>
            {
                player.Teleport(Door.Get(DoorType.EscapeSecondary));
            }),

            // 3: Heals the player by 25 health points.
            new CoinEffect(Translations.MagicHealMessage, player =>
            {
                player.Heal(25);
            }),

            // 4: Increases the player's health by 10%.
            new CoinEffect(Translations.HealthIncreaseMessage, player =>
            {
                player.Health *= 1.1f;
            }),

            // 5: Spawns SCP-268 (Neat Hat) for the player.
            new CoinEffect(Translations.NeatHatMessage, player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP268, player.Position, new Quaternion());
            }),

            // 6: Applies a random good effect to the player.
            new CoinEffect(Translations.RandomGoodEffectMessage, player =>
            {
                var effect = Config.GoodEffects.ToList().RandomItem();
                player.EnableEffect(effect, 5, true);
                Log.Debug($"Chosen random effect: {effect}");
            }),

            // 7: Spawns a Logicer with one ammo for the player.
            new CoinEffect(Translations.OneAmmoLogicerMessage, player =>
            {
                Firearm gun = (Firearm)Item.Create(ItemType.GunLogicer);
                gun.BarrelAmmo = 1;
                gun.CreatePickup(player.Position);
            }),

            // 8: Spawns SCP-2176 (lightbulb) for the player.
            new CoinEffect(Translations.LightbulbMessage, player =>
            {
                Pickup.CreateAndSpawn(ItemType.SCP2176, player.Position, new Quaternion());
            }),

            // 9: Spawns pink candy (SCP-330) for the player.
            new CoinEffect(Translations.PinkCandyMessage, player =>
            {
                Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Pink);
                candy.CreatePickup(player.Position);
            }),

            // 10: Spawns a customized revolver with attachments for the player.
            new CoinEffect(Translations.BadRevoMessage, player =>
            {
                Firearm revo = (Firearm)Item.Create(ItemType.GunRevolver);
                revo.AddAttachment(new[]
                    {AttachmentName.CylinderMag7, AttachmentName.ShortBarrel, AttachmentName.ScopeSight});
                revo.CreatePickup(player.Position);
            }),

            // 11: Spawns a MicroHID with no energy for the player.
            new CoinEffect(Translations.EmptyHidMessage, player =>
            {
                MicroHIDPickup item = (MicroHIDPickup)Pickup.Create(ItemType.MicroHID);
                item.Position = player.Position;
                item.Spawn();
                item.Energy = 0;
            }),

            // 12: Forces a respawn wave of the team that has more ticketes
            new CoinEffect(Translations.ForceRespawnMessage, player =>
            {
                Respawn.ForceWave(WaveManager.Waves.RandomItem());
            }),

            // 13: Changes the player's size
            new CoinEffect(Translations.SizeChangeMessage, player =>
            {
                player.Scale = new Vector3(1.13f, 0.5f, 1.13f);
            }),

            // 14: Spawns a random item for the player.
            new CoinEffect(Translations.RandomItemMessage, player =>
            {
                Item.Create(Config.ItemsToGive.ToList().RandomItem()).CreatePickup(player.Position);
            }),
        };


        // BadEffects list
        public static List<CoinEffect> BadEffects = new()
        {
            // 0: Reduces player's health by 30%
            new CoinEffect(Translations.HpReductionMessage, player =>
            {
                if ((int) player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health *= 0.7f;
            }),

            // 1: Teleports the player to the class D cells.
            new CoinEffect(Warhead.IsDetonated ? Translations.TpToClassDCellsAfterWarheadMessage : Translations.TpToClassDCellsMessage, player =>
            {
                player.DropHeldItem();
                player.Teleport(Door.Get(DoorType.PrisonDoor));

                if (Warhead.IsDetonated)
                {
                    player.Kill(DamageType.Decontamination);
                }
            }),

            // 2: Applies a random bad effect to the player.
            new CoinEffect(Translations.RandomBadEffectMessage, player =>
            {
                var effect = Config.BadEffects.ToList().RandomItem();
                
                //prevents players from staying in PD infinitely
                if (effect == EffectType.PocketCorroding)
                    player.EnableEffect(EffectType.PocketCorroding);
                else
                    player.EnableEffect(effect, 5, true);
                
                Log.Debug($"Chosen random effect: {effect}");
            }),

            // 3: Starts or stops the warhead based on its state.
            new CoinEffect(Warhead.IsDetonated || !Warhead.IsInProgress ? Translations.WarheadStartMessage : Translations.WarheadStopMessage, player =>
            {
                if (Warhead.IsDetonated || !Warhead.IsInProgress)
                    Warhead.Start();
                else
                    Warhead.Stop();
            }),

            // 4: Turns off all lights
            new CoinEffect(Translations.LightsOutMessage, player =>
            {
                Map.TurnOffAllLights(Config.MapBlackoutTime);
            }),

            // 5: Spawns a live HE grenade
            new CoinEffect(Translations.LiveGrenadeMessage, player =>
            {
                ExplosiveGrenade grenade = (ExplosiveGrenade) Item.Create(ItemType.GrenadeHE);
                grenade.FuseTime = (float) Config.LiveGrenadeFuseTime;
                grenade.SpawnActive(player.Position + Vector3.up, player);
            }),

            // 6: Spawns a flash grenade with a short fuse time, sets the flash owner to the player so that it hopefully blinds people
            new CoinEffect(Translations.TrollFlashMessage, player =>
            {
                FlashGrenade flash = (FlashGrenade) Item.Create(ItemType.GrenadeFlash, player);
                flash.FuseTime = 1f;
                flash.SpawnActive(player.Position);
            }),

            // 7: Teleports the player to a random SCP or inflicts damage if no SCPs exist.
            new CoinEffect(Player.Get(Side.Scp).Any(x => x.Role.Type != RoleTypeId.Scp079) ? Translations.TpToRandomScpMessage : Translations.SmallDamageMessage, player =>
            {
                if (Player.Get(Side.Scp).Any(x => x.Role.Type != RoleTypeId.Scp079))
                {
                    Player scpPlayer = Player.Get(Side.Scp).Where(x => x.Role.Type != RoleTypeId.Scp079).ToList().RandomItem();
                    player.Position = scpPlayer.Position;
                    return;
                }
                player.Hurt(15);
            }),

            // 8: Sets player hp to 1 or kills if it was already 1
            new CoinEffect(Translations.HugeDamageMessage, player =>
            {
                if ((int) player.Health == 1)
                    player.Kill(DamageType.CardiacArrest);
                else
                    player.Health = 1;
            }),

            // 9: Spawns a primed SCP-244 vase for the player.
            new CoinEffect(Translations.PrimedVaseMessage, player =>
            {
                Scp244 vase = (Scp244)Item.Create(ItemType.SCP244a);
                vase.Primed = true;
                vase.CreatePickup(player.Position);
            }),

            // 10: Spawns a tantrum on the player Keywords: shit spawn create
            new CoinEffect(Translations.ShitPantsMessage, player =>
            {
                player.PlaceTantrum();
            }),

            // 11: Broadcasts a fake SCP termination message.
            new CoinEffect(Translations.FakeScpKillMessage, player =>
            {
                var scpName = _scpNames.ToList().RandomItem();
                
                Cassie.MessageTranslated($"scp {scpName.Key} successfully terminated by automatic security system",
                    $"{scpName.Value} successfully terminated by Automatic Security System.");
            }),

            // 12: Forceclass the player to a random scp from the list Keywords: scp fc forceclass
            new CoinEffect(Translations.TurnIntoScpMessage, player =>
            {
                player.DropItems();
                player.Scale = new Vector3(1, 1, 1);
                
                var randomScp = Config.ValidScps.ToList().RandomItem();
                player.Role.Set(randomScp, RoleSpawnFlags.AssignInventory);
                
                //prevents the player from staying in PD forever
                if (player.CurrentRoom.Type == RoomType.Pocket)
                    player.EnableEffect(EffectType.PocketCorroding);
            }),
            
            // 13: Resets player's inventory
            new CoinEffect(Translations.InventoryResetMessage, player =>
            {
                player.DropHeldItem();
                player.ClearInventory();
            }),

            // 14: Flips the players role to the opposite
            new CoinEffect(Translations.ClassSwapMessage, player =>
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

                //prevents the player from staying in PD forever
                if (player.CurrentRoom.Type == RoomType.Pocket)
                {
                    player.EnableEffect(EffectType.PocketCorroding);
                }
            }),

            // 15: Spawns an HE grenade with a very short fuse time
            new CoinEffect(Translations.InstantExplosionMessage, player =>
            {
                ExplosiveGrenade instaBoom = (ExplosiveGrenade) Item.Create(ItemType.GrenadeHE);
                instaBoom.FuseTime = 0.1f;
                instaBoom.SpawnActive(player.Position, player);
            }),

            // 16: Swaps positions with another random player
            new CoinEffect(Player.List.Count(x => x.IsAlive && !Config.PlayerSwapIgnoredRoles.Contains(x.Role.Type)) <= 1 ? Translations.PlayerSwapIfOneAliveMessage : Translations.PlayerSwapMessage, player =>
            {
                var playerList = Player.List.Where(x => x.IsAlive && !Config.PlayerSwapIgnoredRoles.Contains(x.Role.Type)).ToList();
                playerList.Remove(player);
                
                if (playerList.IsEmpty())
                {
                    return;
                }

                var targetPlayer = playerList.RandomItem();
                var pos = targetPlayer.Position;
                
                targetPlayer.Teleport(player.Position);
                player.Teleport(pos);
                
                EventHandlers.SendBroadcast(targetPlayer, Translations.PlayerSwapMessage);
            }),

            // 17: kicks the player
            new CoinEffect(Translations.KickMessage, player =>
            {
                //delay so the broadcast can be sent to the player and doesn't throw NRE
                Timing.CallDelayed(1f, () => player.Kick(Config.KickReason));
            }),

            // 18: swap with a spectator
            new CoinEffect(Player.List.Where(x => x.Role.Type == RoleTypeId.Spectator).IsEmpty() ? Translations.SpectSwapNoSpectsMessage : Translations.SpectSwapPlayerMessage, player =>
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
                
                
                //give spect the players ammo, has to be done before ClearInventory() or else ammo will fall on the floor
                for (int i = 0; i < player.Ammo.Count; i++)
                {
                    spect.AddAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), player.Ammo.ElementAt(i).Value);
                    player.SetAmmo(player.Ammo.ElementAt(i).Key.GetAmmoType(), 0);
                }
                
                player.ClearInventory();
                player.Role.Set(RoleTypeId.Spectator);

                EventHandlers.SendBroadcast(spect, Translations.SpectSwapSpectMessage);
            }),

            // 19: Teleports to a random Tesla gate if warhead is not detonated
            new CoinEffect(Warhead.IsDetonated ? Translations.TeslaTpAfterWarheadMessage : Translations.TeslaTpMessage, player =>
            {
                player.DropHeldItem();
                
                player.Teleport(Exiled.API.Features.TeslaGate.List.ToList().RandomItem());
                
                if (Warhead.IsDetonated)
                {
                    player.Kill(DamageType.Decontamination);
                }
            }),

            // 20: Swaps inventory and ammo with another random player
            new CoinEffect(Player.List.Where(x => !Config.InventorySwapIgnoredRoles.Contains(x.Role.Type)).Count(x => x.IsAlive) <= 1 ? Translations.InventorySwapOnePlayerMessage : Translations.InventorySwapMessage, player =>
            {
                List<Player> playerList = Player.List.Where(x => x != player && !Config.InventorySwapIgnoredRoles.Contains(x.Role.Type)).ToList();
                
                if (playerList.Count(x => x.IsAlive) <= 1)
                {
                    player.Hurt(50);
                    return;
                }
             
                var target = playerList.Where(x => x != player).ToList().RandomItem();

                // Saving items
                List<ItemType> items1 = player.Items.Select(item => item.Type).ToList();
                List<ItemType> items2 = target.Items.Select(item => item.Type).ToList();

                // Saving and removing ammo
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

                // setting items
                target.ResetInventory(items1);
                player.ResetInventory(items2);

                // setting ammo
                foreach (var ammo in ammo2)
                {
                    player.SetAmmo(ammo.Key, ammo.Value);
                }
                foreach (var ammo in ammo1)
                {
                    target.SetAmmo(ammo.Key, ammo.Value);
                }

                EventHandlers.SendBroadcast(target, Translations.InventorySwapMessage);
            }),

            // 21: Spawns a red candy or teleports the player to a random room based on warhead state.
            new CoinEffect(Warhead.IsDetonated ? Translations.RandomTeleportWarheadDetonatedMessage : Translations.RandomTeleportMessage, player =>
            {
                if (Warhead.IsDetonated)
                {
                    Scp330 candy = (Scp330)Item.Create(ItemType.SCP330);
                    candy.AddCandy(InventorySystem.Items.Usables.Scp330.CandyKindID.Red);
                    candy.CreatePickup(player.Position);
                    return;
                }
                
                player.Teleport(Room.Get(Config.RoomsToTeleport.GetRandomValue()));
            }),

            // 22: Handcuffs the player and drops their items
            new CoinEffect(Translations.HandcuffMessage, player =>
            {
                player.Handcuff();
                player.DropItems();
            }),
        };
    }
}
