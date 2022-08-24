# BetterCoinflips
<a href="https://github.com/Mikihero/BetterCoinflips/releases"><img src="https://img.shields.io/github/downloads/Mikihero/BetterCoinflips/total?label=Downloads" alt="Downloads"></a>  
  
SCP:SL plugin that adds a Risk-Reward mechanic to the in-game coin. Whenever you flip a coin a 'random' effect will happen depending on the coinflips outcome.

### **Features:**

- Whenever someone flips a coin and it lands on heads one of the following will happen:  
1. A Containment Engineer/Facility Manager(75%/15%, configurable) keycard will spawn at their feet.  
2. A 'medical kit' will spawn at their feet consisting of a medkit and painkillers.  
3. They will be teleported to the doors leading straight to the escape zone.  
4. They will be healed by 25 health.  
5. Their current hp will be increased by 10%.
6. An SCP-268 will spawn at their feet.
7. They will receive a random good effect (configurable) for 5 seconds.
8. An SCP-2176 will spawn at their feet.  

- Whenever someone flips a coin and it lands on tails one of the following will happen:  
1. Their current hp will be set to 70%.  
2. They will be teleported to Class D containment cells.  
3. They will get a random bad effect (configurable) for 5 seconds.  
4. Warhead will be enabled or disabled depending on it's current state.  
5. Lights all across the map will be turned off for 10 seconds.  
6. They will get a Logicer with 1 ammo
7. A live grenade will be spawned on their head and explode ~3 seconds later.
8. An empty Particle Disruptor will be spawned at their feet and instantly disappear.
9. A flash grenade will spawn on their head and explode after 1 second.
10. They are teleported to an SCP if there are any alive, otherwise they lose 15 hp.
11. They lose all but 1 hp.
12. A fake CASSIE is send saying that SCP-173 was killed by a Tesla gate.

### Default config

```yaml
better_coinflips:
  # Whether or not the plugin should be enabled.
  is_enabled: true
  # Whether or not the default coins should spawn (eg. in lockers). Default: false
  spawn_default_coins: false
  # The duration of the broadcast informing you about your 'reward'. Default: 5
  broadcast_time: 5
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
```