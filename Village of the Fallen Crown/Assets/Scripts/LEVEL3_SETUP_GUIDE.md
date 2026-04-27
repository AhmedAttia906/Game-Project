# Level 3 — The Fallen King (Beginner Setup Guide)

This guide walks you through Level 3 end-to-end: scene structure, gameplay flow, the four new scripts, and the exact Unity Inspector wiring. You already have `PlayerController`, `MouseLook`, `PlayerHealth`, `GameManager`, `UIManager`, and `LevelFinish` working from previous levels — we'll reuse those as-is.

---

## What you need to do (overview)

1. Create a new Scene called `Level3` and add it to Build Settings.
2. Build the layout: forest path → stairs → gate → arena.
3. Add the player (reuse the same prefab/setup from Level 2) and a `Sword` child under the camera.
4. Place the `Gate`, `BossTrigger` arena volume, and the `KingBoss`.
5. Bake a NavMesh so the boss can walk.
6. Hook the scripts up in the Inspector.
7. Press Play and tune values.

---

## Step 1 — Scene Setup

### 1a. Create the scene
- `File → New Scene → Basic (URP)` (or whatever your other levels use).
- `File → Save As… → Assets/Scenes/Level3.unity`.
- Open `File → Build Settings…` and click **Add Open Scenes** so `LoadNextLevel()` works from Level 2.

### 1b. Hierarchy structure (recommended)

```
Level3 (scene)
├── --- ENVIRONMENT ---
│   ├── ForestPath           (terrain or large stretched cubes)
│   ├── Stairs               (built from cubes, or a stair model)
│   ├── CastleWalls          (cubes or castle prefab)
│   ├── Gate                 (the moving piece; GateController goes here)
│   └── Arena                (floor + walls of the boss room)
├── --- GAMEPLAY ---
│   ├── Player               (your existing player prefab)
│   ├── KingBoss             (NavMeshAgent + BossAI)
│   ├── BossTriggerVolume    (large box trigger inside the arena)
│   └── LevelFinish          (optional — already exists for win pickup)
├── --- LIGHTING ---
│   ├── DirectionalLight (Moonlight, dim, blue tint)
│   ├── ArenaTorches         (a few Point Lights with warm color)
│   └── (optional) Fog enabled in Lighting → Environment
└── --- UI ---
    └── Canvas
        ├── HealthBar (you already have this)
        ├── BossHealthBar (new — Slider, hidden at start)
        ├── BossIntroBanner (optional Text: "The Fallen King")
        ├── PausePanel / WinPanel / LosePanel (already exist)
```

### 1c. Layout sketch

- **Spawn area:** flat ground, dark.
- **Forest path:** narrow corridor of trees/rocks pointing toward the castle.
- **Stairs:** rise toward a closed gate in the castle wall.
- **Gate:** blocks entry; opens automatically when player gets near.
- **Arena:** square room past the gate, 20×20 units is fine. Place the King in the middle.
- **BossTriggerVolume:** a thin invisible box covering the arena entrance (roughly 5×3×2). Walking into it starts the fight.

### 1d. Free assets that are good enough
Anything from these gets you a working level fast:
- Unity Asset Store → "POLYGON Castle" (free trial) or "Castle Pack Lite".
- Mixamo for a free animated knight as the King (export FBX, import into Unity).
- Quaternius/Kenney free packs for trees and stones.
- Or just use **primitive cubes/cylinders** and a few materials. Working > pretty for now.

### 1e. Lighting
- Set **Environment Lighting → Source = Color** to a dark blue/grey for the forest part.
- Add a `Directional Light` rotated like moonlight, low intensity (`0.2 – 0.4`).
- Add **Point Lights** with warm orange color near each torch in the arena. The contrast between dark approach and lit arena is the whole vibe.
- Optional: `Window → Rendering → Lighting → Environment → Fog` enabled, dark color, density `~0.03`.

---

## Step 2 — Player Setup (reuse what you have)

Drag your Player prefab into the scene. Make sure:
- Tag = **Player**.
- It has `PlayerController`, `PlayerHealth`, and a child Camera with `MouseLook`.

### Add the sword
1. Under the player **Camera**, create an empty: `Sword`.
2. Move it to roughly `(0.4, -0.3, 0.6)` so it sits in the bottom-right of the view.
3. Drag a sword model in as a child (or use an elongated stretched cube as a placeholder).
4. **Add Component → SwordCombat** on the `Sword` GameObject.

Inspector for `SwordCombat`:
| Field | Value |
|---|---|
| Player Camera | drag the player Camera |
| Sword Animator | (leave empty for now, or drag if you have one) |
| Damage | 25 |
| Attack Range | 2.5 |
| Attack Cooldown | 0.6 |
| Hit Layers | Everything |

---

## Step 3 — Gate

1. Make a tall thin cube: scale `(4, 5, 0.3)`, position it across the castle entrance.
2. Rename it `Gate`.
3. **Add Component → GateController**.
4. Make sure it has a `Box Collider` (NOT a trigger — the gate is solid when closed).

Inspector for `GateController`:
| Field | Value |
|---|---|
| Open Offset | (0, 5, 0)   ← gate slides up 5 units |
| Move Speed | 2 |
| Auto Open When Player Near | ✔ |
| Player | (leave empty — auto-found by Player tag) |
| Open Distance | 6 |

Test: walk toward it in Play mode — it should rise. Walk away — by design it stays open (we don't close it again until BossTrigger fires).

---

## Step 4 — King Boss

### 4a. Build the boss object
1. Drop in any humanoid model, or use a tall capsule as a placeholder.
2. Rename it `KingBoss`. Tag it `Enemy`.
3. Add components:
   - `NavMeshAgent` (Speed = 3.5, Stopping Distance = 2)
   - `Capsule Collider` (size to fit the model)
   - `BossAI` (the new script)
   - `Animator` (only if you have animations — otherwise leave it off)

### 4b. Bake the NavMesh
This is what lets the boss walk.
1. Select all the **arena floor** objects → in the Inspector, mark them **Static** (top-right checkbox dropdown → "Navigation Static").
2. Open `Window → AI → Navigation`.
3. Go to the **Bake** tab → click **Bake**.
4. You should see a blue overlay on walkable surfaces.

> If you're on Unity 6 / new Navigation: add a `NavMeshSurface` component to a parent of the arena floor and click **Bake** on that component instead.

### 4c. Boss Health Bar UI
1. In the Canvas, create `UI → Slider`. Rename it `BossHealthBar`.
2. Anchor it to the **top center** of the screen.
3. Remove the slider's "Handle" child (we want a static bar).
4. Color the Fill red.
5. Disable the GameObject (the BossAI script enables it when the fight starts).

### 4d. Inspector for `BossAI` (on KingBoss)
| Field | Value |
|---|---|
| Max Health | 200 |
| Damage | 20 |
| Chase Range | 30 |
| Attack Range | 2.5 |
| Attack Cooldown | 1.5 |
| Player | (auto-finds by tag, or drag the Player) |
| Agent | (auto-fills with NavMeshAgent) |
| Animator | (drag if present) |
| Boss Health Bar | drag the `BossHealthBar` Slider |
| Win Delay | 2 |

> If you have an Animator, add three Trigger parameters: `Attack`, `Hit`, `Die`. The script just sets them — you wire them to your states.

---

## Step 5 — Boss Trigger (starts the fight)

1. Create `Cube`, rename it `BossTriggerVolume`. Place it just past the gate, covering the arena entrance.
2. Scale it like `(8, 4, 1)` so the player can't miss it.
3. Disable its **Mesh Renderer** (it should be invisible).
4. Make sure the `Box Collider` has **Is Trigger** = ✔.
5. **Add Component → BossTrigger**.

Inspector for `BossTrigger`:
| Field | Value |
|---|---|
| Boss | drag `KingBoss` |
| Gate To Close | drag `Gate` |
| Intro UI | (optional) drag a UI banner GameObject |
| Intro Duration | 2 |

Now: walking through it triggers the gate to lock and the boss to wake up.

---

## Step 6 — Win Condition

When the boss dies, `BossAI.Die()` waits `Win Delay` seconds, then calls:

```csharp
GameManager.Instance.LevelComplete();
```

That's already wired in your existing `GameManager.cs` to call `UIManager.Instance.ShowWin()`. So as long as your scene contains the `GameManager` and `UIManager` GameObjects (with their existing prefabs/objects), you're done — no extra setup.

> If you want a "walk to the throne to finish" instead of auto-finishing on death: keep `BossAI.winDelay` long (or remove `TriggerLevelComplete()`), and place the existing `LevelFinish` prefab on the throne — its OnTriggerEnter already calls `LevelComplete`.

---

## Step 7 — Tags and Layers Checklist

Before you press Play, double-check:
- Player GameObject Tag = **Player**.
- KingBoss Tag = **Enemy** (not strictly required, but consistent with your other enemies).
- Gate has a non-trigger collider (so the player can't walk through it when closed).
- BossTriggerVolume's collider has **Is Trigger** = ✔.
- Arena floor is marked Navigation Static and a NavMesh has been baked.
- A `GameManager` and `UIManager` GameObject exist in the scene (drag from your other levels if needed).

---

## Step 8 — Test Flow

1. Press Play.
2. Walk forward through the dark forest.
3. Approach the gate → it slides up.
4. Walk through into the arena → gate slams down behind you, BossHealthBar appears, KingBoss starts chasing.
5. Left-click to swing the sword. Each hit lowers the boss bar.
6. When the bar empties, the boss stops, plays Die (if animated), waits 2 seconds, then your existing Win panel appears.

---

## The four new scripts at a glance

| Script | What it does | Attach to |
|---|---|---|
| `BossAI.cs` | Boss health, chase, attack, death → win | KingBoss |
| `SwordCombat.cs` | Left-click melee raycast that damages the boss | Sword (child of camera) |
| `GateController.cs` | Slides a gate up when player is near; can be locked | Gate |
| `BossTrigger.cs` | Locks gate + activates boss when player enters | BossTriggerVolume |

The reused scripts are unchanged: `PlayerController`, `MouseLook`, `PlayerHealth`, `GameManager`, `UIManager`, `LevelFinish`.

---

## Common beginner gotchas

- **Boss doesn't move:** NavMesh isn't baked, or KingBoss isn't on the NavMesh. Mark floor as Navigation Static and rebake.
- **Sword doesn't damage:** the Sword's `Player Camera` field is empty, OR the boss's collider is on a layer not in `Hit Layers`, OR you're standing too far away (raise `Attack Range`).
- **Gate doesn't open:** Player isn't tagged `Player`, OR `Open Distance` is too small.
- **Win screen doesn't show:** scene is missing `GameManager` or `UIManager`. Drag them in from Level 2.
- **Cursor stays visible during play:** `MouseLook` calls `Cursor.lockState = Locked` on Start. If something else is unlocking it, check your UIManager pause logic.

Once gameplay works, you can polish: add the knight model, animations, particles, sound effects, and a proper boss intro banner.
