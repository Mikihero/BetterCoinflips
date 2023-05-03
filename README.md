# BetterCoinflips
<a href="https://github.com/Mikihero/BetterCoinflips/releases"><img src="https://img.shields.io/github/downloads/Mikihero/BetterCoinflips/total?label=Downloads" alt="Downloads"></a>  
  
SCP:SL plugin that adds a Risk-Reward mechanic to the in-game coin. Whenever you flip a coin a 'random' effect will happen depending on the coinflips outcome.

# Features:

- Whenever someone flips a coin and it lands on heads one of the following will happen:  
 1. A Containment Engineer/Facility Manager(85%/15%, configurable) keycard will spawn at their feet.  
 2. A 'medical kit' will spawn at their feet consisting of a medkit and painkillers.  
 3. They will be teleported to the doors leading straight to the escape zone.  
 4. They will be healed by 25 health.  
 5. Their current hp will be increased by 10%.
 6. An SCP-268 will spawn at their feet.
 7. They will receive a random good effect (configurable) for 5 seconds.
 8. They will get a Logicer with 1 ammo.  
 9. An SCP-2176 will spawn at their feet. 
 10. A pink candy will spawn at their feet. 
 11. An empty micro hid will spawn at their feet.
 12. A revolver with the worst attachments possible will spawn at their feet. 

- Whenever someone flips a coin and it lands on tails one of the following will happen:  
 1. Their current hp will be set to 70%.  
 2. They will be teleported to Class D containment cells.  
 3. They will get a random bad effect (configurable) for 5 seconds.  
 4. Warhead will be enabled or disabled depending on it's current state.  
 5. Lights all across the map will be turned off for 10 seconds.  
 6. A live grenade will be spawned on their head and explode ~3 seconds later.
 7. An empty Particle Disruptor will be spawned at their feet and instantly disappear.
 8. A flash grenade will spawn on their head and explode after 1 second.
 9. They are teleported to an SCP if there are any alive, otherwise they lose 15 hp.
 10. They lose all but 1 hp.
 11. A primed SCP-244 is spawned on their head.
 12. An SCP-173 tantrum is placed at their feet.
 13. A fake CASSIE is send saying that SCP-173 was killed by a Tesla gate.

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
  - SeveredHands
  - SinkHole
  - Stained
  - SoundtrackMute
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
  # The % chance of receiving a Facility Manager keycard instead of a Containment Engineer keycard when that effect is chosen. Default: 15
  red_card_chance: 15
  # The chance of these good effects happening. It's a proportional chance not a % chance.
  keycard_effect_chance: 20
  medical_kit_effect_chance: 35
  tp_to_escape_effect_chance: 5
  heal_effect_chance: 10
  more_hp_effect_chance: 10
  hat_effect_chance: 10
  random_good_effect_chance: 30
  one_ammo_logicer_effect_chance: 1
  lightbulb_effect_chance: 15
  pink_candy_effect_chance: 10
  empty_hid_effect_chance: 10
  bad_revo_effect_chance: 5
  # The chance of these bad effects happening. It's a proportional chance not a % chance.
  hp_reduction_effect_chance: 20
  tp_to_class_d_cells_effect_chance: 5
  random_bad_effect_chance: 20
  warhead_effect_chance: 10
  lights_out_effect_chance: 20
  live_he_effect_chance: 30
  troll_gun_effect_chance: 50
  troll_flash_effect_chance: 50
  scp_tp_effect_chance: 20
  one_hp_left_effect_chance: 15
  primed_vase_effect_chance: 20
  shit_pants_effect_chance: 40
  fake_cassie_effect_chance: 50
```
