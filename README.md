# BetterCoinflips
<a href="https://github.com/Mikihero/BetterCoinflips/releases"><img src="https://img.shields.io/github/downloads/Mikihero/BetterCoinflips/total?label=Downloads" alt="Downloads"></a>  
  
SCP:SL plugin that adds a Risk-Reward mechanic to the in-game coin. Whenever you flip a coin a 'random' effect will happen depending on the coinflips outcome.

### **Features:**

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
11. A fake CASSIE is send saying that SCP-173 was killed by a Tesla gate.

- If the config option for it is set to true, the plugin will prevent the spawns of any coins in lockers etc.
- The plugin will replace every default SCP-1853 with a coin in the SCP pedestals.

### Default config

```yaml
better_coinflips:
  # Whether or not the plugin should be enabled.
  is_enabled: true
  # Whether or not the default coins should spawn (eg. in lockers). Default: false
  spawn_default_coins: false
  # The duration of the broadcast informing you about your 'reward'. Default: 3
  broadcast_time: 3
  # The duration of the map blackout. Default: 10
  map_blackout_time: 10
  # List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html
  bad_effects:
  - Amnesia
  - Asphyxiated
  - Blinded
  - Burned
  - Concussed
  - Deafened
  - Disabled
  - Ensnared
  - Exhausted
  - Flashed
  - Hemorrhage
  - SeveredHands
  - SinkHole
  - Stained
  - Visual173Blink
  # List of good effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html
  good_effects:
  - BodyshotReduction
  - DamageReduction
  - Invigorated
  - Invisible
  - MovementBoost
  - RainbowTaste
  - Scp207
  - Vitality
  # The % chance of receiving a Facility Manager keycard instead of a Containment Engineer keycard when that effect is chosen. Default: 15
  red_card_chance: 15
  # The % chance for each of the below good effects to happen, they are checked separately and thus don't have to add up to 100%. If none of those are chosen then the last effect happens.
  keycard_effect_chance: 20
  medical_kit_effect_chance: 35
  escape_effect_chance: 5
  heal_effect_chance: 10
  more_h_p_effect_chance: 10
  hat_effect_chance: 10
  random_good_effect_chance: 30
  one_ammo_logicer_effect_chance: 5
  lightbulb_effect_chance: 15
  # The % chance for each of the below bad effects to happen, they are checked separately and thus don't have to add up to 100%. If none of those are chosen then the last effect happens.
  hp_reduction_effect_chance: 20
  t_p_to_class_d_cells_effect_chance: 10
  random_bad_effect_chance: 30
  warhead_effect_chance: 25
  lights_out_effect_chance: 15
  live_h_e_effect_chance: 50
  troll_gun_effect_chance: 50
  live_flas_effect_chance: 50
  s_c_p_tp_effect_chance: 35
  one_h_p_left_effect_chance: 20
  fake_cassie_effect_chance: 45
```

**THESE PERCENTAGES ARE NOT AT ALL BALANCED, THEY'RE JUST SOMETHING I PUT IN PLACE FOR NOW, IF YOU FEEL LIKE EVERYONE WOULD BENEFIT FROM CHANGING ONE OF THE DEFAULT VALUES FEEL FREE TO OPEN A PR/ISSUE ON GITHUB.**