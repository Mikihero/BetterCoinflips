# BetterCoinflips
<a href="https://github.com/Mikihero/BetterCoinflips/releases"><img src="https://img.shields.io/github/downloads/Mikihero/BetterCoinflips/total?label=Downloads" alt="Downloads"></a>  
  
SCP:SL plugin that adds a Risk-Reward mechanic to the in-game coin. Whenever you flip a coin a 'random' effect will happen depending on the coinflips outcome.

# Features:

- Whenever a player flips a coin and it lands on heads one of the following will happen:  
 1. They will receive a Containment Engineer/Facility Manager keycard.  
 2. They will recevive a 'medical kit' consisting of a medkit and painkillers.
 3. They will be teleported to the escape zone doors.  
 4. They will be healed by 25 health.
 5. Their hp will be increased by 10%.
 6. They will get an SCP-268.
 7. They will receive a random good effect for 5 seconds.
 8. They will get a Logicer with 1 ammo.  
 9. They will receive an SCP-2176. 
 10. They will receive a pink candy. 
 11. They will receive a revolver with the worst attachments possible. 
 12. They will get an empty micro hid.
 13. MTF/CI will respawn immediatly.
 14. Their scale will be set to 1.3/0.5/1.3.

- Whenever someone flips a coin and it lands on tails one of the following will happen:  
 1. Their hp will be reduced by 30%.  
 2. They will be teleported to Class D cells.  
 3. They will get a random bad effect for 5 seconds.  
 4. The Alpha Warhead will be enabled or disabled depending on it's current state.  
 5. Lights all across the map will be turned off for 10 seconds.  
 6. A live grenade will appear on their head.
 7. They will get an empty Particle Disruptor.
 8. A live flash grenade will spawn on their head.
 9. They will be teleported to an SCP if there are any alive, otherwise they'll lose 15 hp.
 10. They will lose all but 1 hp.
 11. Thye will receive a primed SCP-244.
 12. They receive an SCP-173 tantrum.
 13. A fake CASSIE is sent saying that SCP-173 was killed by a Tesla gate.
 14. They will be forceclassed to SCP-049-2.
 15. Their inventory will be reset.
 16. Their role will be changed to the opposite one (class d - scientist, mtf - ci etc.)
 17. An instantly exploding grenade will spawn on their head.
 18. They will swap places with another player.
 19. They will be kicked.
 20. They will be replaced by a random spectator.
 21. They will be teleported to a random tesla.
 22. Their inventory will be swapped with another player's inventory.
 23. They will be teleported to a random room.
 24. They will be handcuffed and lose their items.

- The plugin will prevent the spawns of a specified amount of coins around the map.
- The plugin will replace a specified amount of the chosen item (by default SCP-500) with a coin in the SCP pedestals.
- The plugin will assign a random amount of uses to every thrown coin. This amount can be read or set with a command. If a coin runs out of uses it breaks.

# Commands

- GetSerial - gets the serial number of an item held by you or another player.
- CoinUses - gets or sets the number of uses a specific coin has. Example usage: `coinuses get player 5`, `coin uses set player 4`, `coinuses set serial 10` 

# Permissions

- bc.coinuses.set - grants access to the CoinUses Set command
- bc.coinuses.get - grants access to the CoinUses Get command

# Default config

```yaml
better_cf:
  # Whether or not the plugin should be enabled. Default: true
  is_enabled: true
  # Whether or not debug logs should be shown. Default: false
  debug: false
  # The amount of base game spawned coins that should be removed. Default: 4
  default_coins_amount: 4
  # The ItemType of the item to be replaced with a coin and the amount to be replaced, the item is supposed to be something found in SCP pedestals.
  item_to_replace:
    SCP500: 2
  # The boundaries of the random range of throws each coin will have before it breaks. The upper bound is exclusive.
  min_max_default_coins:
  - 1
  - 4
  # Time in seconds between coin toses.
  coin_cooldown: 5
  # The duration of the broadcast informing you about your 'reward'. Default: 3
  broadcast_time: 3
  # The duration of the map blackout. Default: 10
  map_blackout_time: 10
  # The fuse time of the grenade falling on your head. Default: 3.25
  live_grenade_fuse_time: 3.25
  # List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html
  bad_effects:
  - Asphyxiated
  - Bleeding
  - Blinded
  - Burned
  - Concussed
  - Corroding
  - CardiacArrest
  - Deafened
  - Decontaminating
  - Disabled
  - Ensnared
  - Exhausted
  - Flashed
  - Hemorrhage
  - Hypothermia
  - InsufficientLighting
  - Poisoned
  - PocketCorroding
  - SeveredHands
  - SinkHole
  - Stained
  - Traumatized
  # List of good effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html
  good_effects:
  - BodyshotReduction
  - DamageReduction
  - Invigorated
  - Invisible
  - MovementBoost
  - RainbowTaste
  - Scp1853
  - Scp207
  - Vitality
  # The % chance of receiving a Facility Manager keycard instead of a Containment Engineer one.
  red_card_chance: 15
  # The kick reason.
  kick_reason: 'The coin kicked your ass.'
  # The chance of these good effects happening. It's a proportional chance not a % chance.
  keycard_chance: 20
  medical_kit_chance: 35
  tp_to_escape_chance: 5
  heal_chance: 10
  more_hp_chance: 10
  hat_chance: 10
  random_good_effect_chance: 30
  one_ammo_logicer_chance: 1
  lightbulb_chance: 15
  pink_candy_chance: 10
  bad_revo_chance: 5
  empty_hid_chance: 5
  force_respawn_chance: 15
  size_change_chance: 20
  # The chance of these bad effects happening. It's a proportional chance not a % chance.
  hp_reduction_chance: 20
  tp_to_class_d_cells_chance: 5
  random_bad_effect_chance: 20
  warhead_chance: 10
  lights_out_chance: 20
  live_he_chance: 30
  troll_gun_chance: 50
  troll_flash_chance: 50
  scp_tp_chance: 20
  one_hp_left_chance: 15
  primed_vase_chance: 20
  shit_pants_chance: 40
  fake_cassie_chance: 50
  turn_into_scp_chance: 30
  inventory_reset_chance: 20
  class_swap_chance: 10
  instant_explosion_chance: 10
  player_swap_chance: 20
  kick_chance: 5
  spect_swap_chance: 10
  tesla_tp_chance: 15
  inventory_swap_chance: 20
  handcuff_chance: 10
  random_teleport_chance: 15
```