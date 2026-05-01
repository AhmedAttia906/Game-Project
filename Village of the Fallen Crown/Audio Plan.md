# Village of the Fallen Crown — Audio Design Plan

---

## Game Summary

A medieval first-person adventure across 3 levels, each with a different gameplay style:
- **Level 01** — Village race + horse rescue side quest → chest reward
- **Level 02** — Archery challenge (3 targets) → chest reward
- **Level 03** — Castle boss fight (sword + shield bash) → final victory

Core player actions: run/crouch/jump, sword attack, shield bash, bow shoot, E-key interactions (chest, horse, villager).

---

## Main Menu

| Type | Sound | Notes |
|---|---|---|
| Background music | Slow medieval orchestral — soft lute/harp melody, gentle strings | Sets mystery/epic tone. ~60% volume, loops |
| Button hover | Soft wooden knock | Very subtle, don't distract |
| Button click | Heavier thud / wax seal stamp | Satisfying but not jarring |
| Settings open | Parchment unroll crinkle | Short, 0.3s |
| Start game | Rising string swell + distant horn | Crossfades into Level_01 music |
| Quit | Soft fading chord | |

> **Search on Freesound:** `"medieval lute loop"` · `"parchment crinkle"` · `"UI click wooden"`

---

## Level 01 — The Village Race + Horse Rescue

### Background Music
- **Race phase:** Fast energetic medieval folk — quick fiddle, flute, driving hand drum. Upbeat, racing feel. Loops.
- **Side quest (after finish):** Slower adventurous tone — same instruments but half the tempo, more melodic.

### Race System

| Trigger | Sound | Timing |
|---|---|---|
| Countdown "3, 2, 1" | Single bell toll per number | Each second |
| "GO!" | Short horn blast | Instant |
| Checkpoint passed | Bright coin-jingle ding (0.5s) | `PlayerPassedCheckpoint()` |
| Position changes | Subtle ascending/descending chime | `UpdatePosition()` |
| Race won (1st) | Crowd cheer burst + fanfare sting | `WinRace()` → chest activates |
| Race lost | Deflating trombone + faint crowd groan | `LoseRace()` |

### Player Movement

| Action | Sound |
|---|---|
| Footsteps (normal) | Dry dirt crunch, cycle of 4 steps |
| Footsteps (crouch) | Slower, quieter cloth shuffle |
| Jump | Short effort exhale + soft whoosh |
| Land | Dull thud — heavier for longer falls |

### Horse Rescue Side Quest

| Trigger | Sound | Notes |
|---|---|---|
| Villager first contact | Friendly NPC "hmm" grunt + parchment open | `VillagerTrigger` first enter |
| Enemy detected (chase starts) | Combat music kicks in — folk track but drums get louder | |
| Sword swing | Short blade whoosh (metallic air slice) | Left mouse click |
| Sword hits enemy | Metal impact + dull flesh thud | Raycast hit at 4 unit range |
| Player takes hit | Armor clank + short pain grunt | |
| Enemy death | Long groan + collapse body thud | `EnemyHealth` reaches 0 |
| Horse becomes available | Gentle horse whinny (happy, soft) | `HorseInteract.canInteract = true` |
| Horse picked up (E key) | Closer whinny + hoof stomp | `HorseInteract` pickup |
| Horse following | Soft hoofbeat loop (quiet, positional) | While horse follows player |
| Villager return (mission done) | Short triumphant sting + coin clink | `VillagerTrigger` mission complete |

### Chest (Win Reward)

| Trigger | Sound |
|---|---|
| Chest activates (appears) | Achievement jingle ✅ already assigned (270404) |
| Player walks near chest | Very soft magical shimmer/sparkle (ambient, looping quietly) |
| Chest opening (E key) | Wood creak + old lock click + lid lifting groan |
| Fully open / reward revealed | Short magical chime burst (treasure sting) |
| Level complete panel | Triumphant orchestral sting |

### Environment / Ambient
- Distant birds chirping (loop, low)
- Light wind through trees
- Faint crowd chatter/murmur near start line
- Hoofbeats of race bots (quiet, directional based on position)

> **Search on Freesound:** `"medieval folk loop fast"` · `"crowd cheer short"` · `"horse whinny soft"` · `"dirt footsteps"` · `"sword whoosh"` · `"chest open wood"`

---

## Level 02 — Archery Challenge

### Background Music
Focused, tense atmosphere — slower than Level 01. Plucked lute, light frame drum, occasional solo flute. Feels like a tournament/competition. Medium intensity, builds slightly with each target hit.

### Archery Actions

| Action | Sound | Timing |
|---|---|---|
| Bow draw | String tension creak + light wood flex | On left-click press |
| Arrow fire | Sharp "thwip" + fast air whoosh | Arrow instantiated |
| Arrow in flight | Subtle high-pitch wind (short, 0.3s) | Arrow moving |
| Arrow hits target | Solid wooden "thunk" + target vibration buzz | `TargetHit.OnCollisionEnter` |
| Arrow misses | Fading whoosh + distant thud | Timeout / no hit |
| 1st target hit | Short bright chime (positive feedback) | Score 1/3 |
| 2nd target hit | Slightly louder chime + light drum accent | Score 2/3 |
| 3rd target (challenge done) | Full fanfare hit + chest activates | `CompleteChallenge()` |
| Target counter update | Subtle UI tick/click | Each `UpdateCounter()` |

### Chest
All chest sounds identical to Level 01 — keeps it consistent and rewarding.

### Environment / Ambient
- Open air breeze (medium wind, loop)
- Distant birds
- Occasional subtle crowd murmur (watching the challenge)
- Arrow stuck in target: soft wobble/vibration sound lingering after hit

> **Search on Freesound:** `"arrow release bow"` · `"arrow thunk wood"` · `"archery ambience"` · `"medieval tournament music"`

---

## Level 03 — The Boss Fight

This level has the most dynamic audio. Sound should change in real time with the fight.

### Music Phases

| Phase | Music | Trigger |
|---|---|---|
| Approaching arena | Dark ominous underscore — low strings, bass drone, slow heartbeat rhythm | Level start |
| BossTrigger entered | Music cuts abruptly → 1.5 seconds of pure silence | `BossTrigger.OnTriggerEnter` |
| Boss fight active | Full orchestral battle music — fast driving strings, brass stabs, war drums | `BossAI` activated |
| Boss below 50% HP | Add a layer — higher pitched strings, faster drums (or pitch-up the loop slightly) | `BossHealth` damage check |
| Boss defeated | Music stops instantly → 2-second silence → slow emotional/triumphant swell | `BossHealth` death |

### Gate Mechanic

| Event | Sound |
|---|---|
| Gate closing | Heavy stone grinding + iron chain rattling (1.5s) |
| Gate fully closed / locked | Massive iron "CLUNK" — deep, final |

### Boss Intro Sequence

| Event | Sound |
|---|---|
| Boss activates (wakes up) | Deep monstrous growl/roar — long, intimidating |
| Boss health bar appears | Dramatic low brass sting |
| Boss first step toward player | Single heavy footstep thud |

### Player Combat (Sword + Shield)

| Action | Sound |
|---|---|
| Sword swing (left click) | Fast blade air-slice whoosh |
| Sword hits boss | Heavy metal clang + deep impact thud |
| Boss reacts to hit | Short pained grunt/growl |
| Sword swing misses | Air whoosh only |
| Shield bash (Space) | Shield slam impact — sharp metal smash + bass thud |
| Knockback connects | Heavy physical impact + short "push" whoosh |
| Boss roars from knockback | Angry growl — distinct from normal hit reaction |

### Player Taking Damage

| Event | Sound |
|---|---|
| Player hit | Armor clank + short pain grunt |
| Health below 30 | Subtle heartbeat begins (low, steady ~70 BPM) + low-pass filter on music |
| Player death | Sharp pain cry + music drops to bass drone → silence |

### Boss Actions

| Action | Sound |
|---|---|
| Boss footsteps (chasing) | Heavy slow thuds — every step felt |
| Boss attack wind-up | Short low growl / grunt before swing |
| Boss attack hits player | Heavy slam impact |
| Boss attack misses | Big air whoosh |

### Boss Death Sequence

| Event | Sound | Timing |
|---|---|---|
| Boss death animation starts | Agonized roar fading into groan | Immediately |
| Body hits ground | Massive floor-shake thud + distant rumble | On collapse |
| 2-second pause | Complete silence | Hold for dramatic weight |
| Victory panel shows | Full orchestral triumph swell | `FinalVictoryManager` |

### Environment / Ambient
- Stone arena echo — apply **reverb (Room/Hall)** to all sounds in this level
- Crackling torches (loop, quiet)
- Distant wind through stone corridors
- Chains creaking (rare, occasional — adds dungeon feel)

> **Search on Freesound:** `"boss battle music loop"` · `"stone gate close"` · `"monster roar deep"` · `"sword clang metal"` · `"heartbeat game"` · `"orchestral victory fanfare"` · `"torch crackling loop"`

---

## Volume / Mixing Guide

```
Background Music      ████████░░  60-70%
Boss Battle Music     █████████░  75-80%
Enemy / Boss SFX      ████████░░  65%
Player Action SFX     █████████░  80-90%  ← highest priority
UI / Reward sounds    █████████░  85-95%
Ambient / Environment ████░░░░░░  25-40%
Footsteps             ████░░░░░░  25-30%
```

**Key rules:**
- **Duck** background music by ~30% during boss intro silence and chest open moments
- **Never** let ambient sounds compete with boss sounds — keep ambient under 35%
- Sword hits and player damage sounds should always cut through everything — highest priority
- Achievement/chest jingle should briefly override music (plays at full volume)
- Level 03 — apply a **reverb effect** (Room/Hall reverb) to all sounds in the stone arena
- Keep footsteps low — they become annoying fast if too loud

---

## Free Sound Sources

| Site | Best For |
|---|---|
| [freesound.org](https://freesound.org) | Everything — massive library, filter by license |
| [opengameart.org](https://opengameart.org) | Full music loops, RPG/fantasy SFX packs |
| [mixkit.co](https://mixkit.co) | Clean UI sounds, victory/achievement jingles |
| [zapsplat.com](https://zapsplat.com) | Environmental sounds, creature sounds |
| [pixabay.com/music](https://pixabay.com/music) | Background music loops (free, no attribution needed) |

### Best Freesound Search Tags for Your Game

`medieval sword clash` · `arrow thunk wood` · `horse whinny` · `monster roar deep` · `stone gate grinding` · `chest open creak` · `crowd cheer short` · `heartbeat tense` · `orchestral battle loop` · `dungeon ambience` · `torch crackling` · `achievement jingle`
