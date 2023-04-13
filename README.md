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
10. A pink candy will spawn at their feet. 

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

- The plugin will prevent the spawns of any coins in lockers etc.
- The plugin will replace every chosen item (by default SCP-1853) with a coin in the SCP pedestals.
- The plugin will remove a thrown coin from a players inventory

### Default config

```yaml
better_coinflips:
  # Whether or not the plugin should be enabled. Default: true
  is_enabled: true
  # Whether or not debug logs should be shown. Default: false
  debug: false
  # Whether or not the default coins should spawn (eg. in lockers). Default: false
  spawn_default_coins: false
  # The ItemType of the item to be replaced with a coin, the item is supposed to be something found in SCP pedestals.
  item_to_replace: SCP1853
  # Whether or not the coin should be removed from a players inventory after it's thrown. Default: false.
  remove_coin_on_throw: false
  # The duration of the broadcast informing you about your 'reward'. Default: 3
  broadcast_time: 3
  # The duration of the map blackout. Default: 10
  map_blackout_time: 10
  # The fuse time of the grenade falling on your head. Default: 3.25
  live_grenade_fuse_time: 3.25
  # List of bad effects that can be applied to the players. List available at: https://exiled-team.github.io/EXILED/api/Exiled.API.Enums.EffectType.html
  bad_effects:
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
  # The chance of these effects happening. It's a proportional chance not a % chance.
  keycard_effect_chance: 20
  medical_kit_effect_chance: 35
  t_p_to_escape_effect_chance: 5
  heal_effect_chance: 10
  more_h_p_effect_chance: 10
  hat_effect_chance: 10
  random_good_effect_chance: 30
  one_ammo_logicer_effect_chance: 1
  lightbulb_effect_chance: 15
  pink_candy_effect_chance: 10
  # The chance of these effects happening. It's a proportional chance not a % chance.
  hp_reduction_effect_chance: 20
  t_p_to_class_d_cells_effect_chance: 5
  random_bad_effect_chance: 20
  warhead_effect_chance: 10
  lights_out_effect_chance: 20
  live_h_e_effect_chance: 30
  troll_gun_effect_chance: 50
  troll_flash_effect_chance: 50
  s_c_p_tp_effect_chance: 20
  one_h_p_left_effect_chance: 15
  primed_vase_effect_chance: 20
  shit_pants_effect_chance: 40
  fake_cassie_effect_chance: 50
  # The message broadcast to a player when they receive a facility manager keycard (the red one) from the coin.
  red_card_message: You acquired a Facility Manager keycard!
  # The message broadcast to a player when they receive a containment engineer keycard (the useless one) from the coin.
  containment_engineer_card_message: You acquired a Containment Engineer keycard!
  # The message broadcast to a player when they receive a medi-kit from the coin.
  medi_kit_message: You received a Medical Kit!
  # The message broadcast to a player when they get teleported to the escape area by the coin.
  tp_to_escape_message: You can now escape! That's what you wanted right?
  # The message broadcast to a player when they get magically healed by the coin.
  magic_heal_message: You've been magically healed!
  # The message broadcast to a player when they get their hp increased by 10% by the coin.
  health_increase_message: You received 10% more hp!
  # The message broadcast to a player when they receive an SCP-268 from the coin.
  neat_hat_message: You got a neat hat!
  # The message broadcast to a player when they receive a random good effect from the coin.
  random_good_effect_message: You got a random effect.
  # The message broadcast to a player when they receive a logicer with 1 ammo from the coin.
  one_ammo_logicer_message: You got gun.
  # The message broadcast to a player when they receive an SCP-2176 from the coin.
  lightbulb_message: You got a shiny lightbulb!
  # The message broadcast to a player when they receive a pink candy from the coin.
  pink_candy_message: You got a pretty candy!
  # The message broadcast to a player when they get their hp reduced by 30% by the coin.
  h_p_reduction_message: Your hp got reduced by 30%.
  # The message broadcast to a player when they get teleported to Class D cells by the coin.
  t_p_to_class_d_cells_message: You got teleported to Class D cells.
  # The message broadcast to a player when they receive a random bad effect from the coin.
  random_bad_effect_message: You got a random effect.
  # The message broadcast to a player when the warhead has been stopped by the coin.
  warhead_stop_message: The warhead has been stopped.
  # The message broadcast to a player when the warhead has been started by the coin.
  warhead_start_message: The warhead has been started.
  # The message broadcast to a player when the lights have been turned off by the coin.
  lights_out_message: Lights out.
  # The message broadcast to a player when a grenade has been dropped on their head by the coin.
  live_grenade_message: Watch your head!
  # The message broadcast to a player when they receive a troll particle disruptor from the coin.
  troll_gun_message: YOU GOT A WHAT!?
  # The message broadcast to a player when a flash that can't blind them is dropped on their head by the coin.
  troll_flash_message: You heard something?
  # The message broadcast to a player when they are teleported to a random SCP by the coin.
  t_p_to_random_s_c_p_message: You were teleported to an SCP.
  # The message broadcast to a player when they are dealth 15 damage by the coin.
  small_damage_message: You've lost 15hp.
  # The message broadcast to a player when they are left on 1 hp by the coin.
  huge_damage_message: You've lost a lot of hp
  # The message broadcast to a player when they a primed vase is spawned on their head.
  primed_vase_message: Your grandma payed you a visit!
  # The message broadcast to a player when an SCP-173 tantrum is spawned beneath their feet.
  shit_pants_message: You just shit your pants.
  # The message broadcast to a player when the coin fakes a cassie of an SCP dying.
  fake_s_c_p_kill_message: Did you just kill an SCP?!
```