# Level 3 — Development Phases (Beginner Roadmap)

Follow these phases **in order**. Don't skip ahead — each phase builds on the previous one.
Estimated total time: **5 – 8 hours** (spread over a few sessions is fine).

---

## 📌 Phase 1: Scene Setup
**⏱️ Estimated time:** 15 – 25 minutes

### 🪜 Steps
1. In Unity, go to **File → New Scene → Basic (URP)** (or whichever template your other levels use).
2. Click **File → Save As…** and save it as `Assets/Scenes/Level3.unity`.
3. Open **File → Build Settings…** → click **Add Open Scenes** so Level 3 appears in the list.
4. Drag Level 3 below Level 2 in that list (so `LoadNextLevel()` from Level 2 loads it).
5. Close Build Settings.
6. In the Hierarchy, create empty parent GameObjects to organize the scene. Right-click → **Create Empty** for each:
   - `--- ENVIRONMENT ---`
   - `--- GAMEPLAY ---`
   - `--- LIGHTING ---`
   - `--- UI ---`
7. Save the scene (`Ctrl+S`).

### ✅ Checklist
- [ ] `Level3.unity` created in `Assets/Scenes/`
- [ ] Level 3 added to Build Settings, placed after Level 2
- [ ] Empty parent GameObjects created in the Hierarchy
- [ ] Scene saved

---

## 📌 Phase 2: Environment & Layout
**⏱️ Estimated time:** 45 – 90 minutes (longest phase — this is where the level gets shape)

### 🪜 Steps
1. **Ground / forest path**
   - Right-click in Hierarchy under `--- ENVIRONMENT ---` → **3D Object → Plane**. Name it `ForestGround`.
   - Scale it to about `(5, 1, 10)` so it forms a long path.
2. **Trees / decoration (optional but recommended)**
   - Use cubes/cylinders as placeholders or download a free pack (Quaternius / Kenney).
   - Scatter ~10 trees on each side of the path.
3. **Stairs**
   - Create 5 cubes (`3D Object → Cube`), each scaled `(4, 0.3, 1)`.
   - Place them in a rising staircase pattern at the end of the path.
4. **Castle wall**
   - Create two cubes scaled `(4, 6, 0.5)` for the wall on either side of the gate.
   - Place them at the top of the stairs with a 4-unit gap between them.
5. **Gate**
   - Create a cube scaled `(4, 5, 0.3)` between the wall pieces. Name it `Gate`.
   - Position it so the bottom sits on the ground.
6. **Arena floor**
   - Create a Plane scaled `(2, 1, 2)` — a 20×20 unit room. Name it `ArenaFloor`.
   - Place it just past the Gate.
7. **Arena walls**
   - Create 4 cubes scaled `(20, 5, 0.5)` and arrange them around the arena floor.
8. Save the scene.

> **Tip:** It doesn't have to be pretty yet. Working layout > visuals.

### ✅ Checklist
- [ ] Forest ground placed
- [ ] Stairs rising toward the castle
- [ ] Castle wall with a gap for the gate
- [ ] `Gate` cube placed in the gap
- [ ] Arena floor and walls placed past the gate
- [ ] Player can theoretically walk: spawn → forest → stairs → gate → arena
- [ ] Scene saved

---

## 📌 Phase 3: Lighting
**⏱️ Estimated time:** 15 – 25 minutes

### 🪜 Steps
1. Select the existing **Directional Light** in the Hierarchy.
2. In the Inspector, set:
   - **Color:** dark blue (cool moonlight)
   - **Intensity:** `0.3`
   - **Rotation X:** `50`
3. Inside the arena, add 4 point lights:
   - Right-click `--- LIGHTING ---` → **Light → Point Light**.
   - Set **Color:** warm orange.
   - Set **Intensity:** `2`, **Range:** `8`.
   - Place them in the four corners of the arena (these are the "torches").
4. Open **Window → Rendering → Lighting → Environment** tab.
5. Check the **Fog** box.
   - **Color:** very dark grey/black
   - **Density:** `0.03`
6. Save the scene.

### ✅ Checklist
- [ ] Directional light dimmed and tinted blue
- [ ] 4 warm point lights inside the arena
- [ ] Fog enabled with dark color
- [ ] Forest area feels dark, arena feels lit

---

## 📌 Phase 4: Player Setup
**⏱️ Estimated time:** 20 – 30 minutes

### 🪜 Steps
1. Drag your existing **Player prefab** from `Assets/Prefabs/` into the scene under `--- GAMEPLAY ---`.
2. Position the player at the start of the forest path (e.g., `(0, 1, -40)`).
3. In the Inspector, confirm:
   - **Tag** = `Player`
   - Has `PlayerController`, `PlayerHealth`, `CharacterController`
   - Child Camera has `MouseLook`
4. **Add the sword:**
   - Right-click the player Camera → **Create Empty**. Name it `Sword`.
   - Set its local Position to `(0.4, -0.3, 0.6)`.
   - Drag a sword model in as a child (or create a stretched cube `(0.05, 0.05, 1)` as a placeholder).
5. Select the `Sword` GameObject → **Add Component → SwordCombat**.
6. Fill in the Inspector:
   - **Player Camera:** drag the player Camera
   - **Damage:** `25`
   - **Attack Range:** `2.5`
   - **Attack Cooldown:** `0.6`
7. Press Play. Move with `WASD`, look with mouse. Left-click should do nothing yet (no boss to hit) — that's expected.

### ✅ Checklist
- [ ] Player placed at spawn
- [ ] Player tagged `Player`
- [ ] Camera has `MouseLook` and the cursor locks on Play
- [ ] `Sword` GameObject under camera with `SwordCombat`
- [ ] WASD movement and mouse look both work

---

## 📌 Phase 5: Gate System
**⏱️ Estimated time:** 15 – 20 minutes

### 🪜 Steps
1. Select the `Gate` cube in the Hierarchy.
2. Confirm it has a **Box Collider** with **Is Trigger = OFF** (the gate must be solid when closed).
3. **Add Component → GateController**.
4. Set in Inspector:
   - **Open Offset:** `(0, 5, 0)` — gate slides 5 units up
   - **Move Speed:** `2`
   - **Auto Open When Player Near:** ✔
   - **Open Distance:** `6`
5. Press Play. Walk toward the gate — it should rise. Walk through.
6. (The gate stays open by design — we'll close it later in Phase 7.)

### ✅ Checklist
- [ ] `Gate` has a non-trigger collider
- [ ] `GateController` attached
- [ ] Walking close to the gate makes it slide up
- [ ] Player can walk through the open gate

---

## 📌 Phase 6: NavMesh (so the boss can walk)
**⏱️ Estimated time:** 10 – 15 minutes

### 🪜 Steps
1. Select **all your floor objects** (forest ground, stair cubes, arena floor) by holding `Ctrl` and clicking each.
2. In the Inspector, click the **Static** dropdown (top right, next to the name).
3. From the dropdown, check **Navigation Static**.
4. Open **Window → AI → Navigation**.
5. Click the **Bake** tab.
6. Click the **Bake** button at the bottom.
7. You should see a **blue overlay** appear on every walkable surface. That blue area is where enemies can walk.

> **If you're on Unity 6 / new Navigation system:** add a `NavMeshSurface` component to a parent GameObject of your floors, and click **Bake** on that component instead.

### ✅ Checklist
- [ ] All floor objects marked Navigation Static
- [ ] NavMesh baked successfully
- [ ] Blue overlay covers the forest path, stairs, and arena
- [ ] No floating gaps in the blue overlay

---

## 📌 Phase 7: Boss Setup
**⏱️ Estimated time:** 30 – 45 minutes

### 🪜 Steps
1. **Create the boss GameObject:**
   - Drag your knight model into the scene (or create a Capsule as a placeholder).
   - Name it `KingBoss`. Tag it `Enemy`.
   - Place it in the center of the arena.
2. **Add components** to `KingBoss`:
   - `NavMeshAgent` → set **Speed: 3.5**, **Stopping Distance: 2**
   - `Capsule Collider` (size to fit the model)
   - **BossAI** (the script you already have)
3. **Boss Health Bar UI:**
   - In the Hierarchy, right-click `--- UI ---` → **UI → Canvas** (skip if you already have one from earlier levels — use that one).
   - Right-click the Canvas → **UI → Slider**. Name it `BossHealthBar`.
   - Anchor it **top-center** (use the anchor preset in the RectTransform).
   - Delete the slider's `Handle Slide Area` child (we want a static bar).
   - Select the `Fill` child → set its color to **red**.
   - **Disable** the `BossHealthBar` GameObject (uncheck the box at the top of the Inspector). The script enables it when the fight starts.
4. **Fill in the BossAI Inspector:**
   - **Max Health:** `200`
   - **Damage:** `20`
   - **Chase Range:** `30`
   - **Attack Range:** `2.5`
   - **Attack Cooldown:** `1.5`
   - **Boss Health Bar:** drag the `BossHealthBar` Slider here
   - **Win Delay:** `2`
5. Confirm the boss is standing **on top of the blue NavMesh** (re-bake if not).

### ✅ Checklist
- [ ] `KingBoss` placed in the arena
- [ ] Has NavMeshAgent, Collider, and `BossAI`
- [ ] `BossHealthBar` Slider exists in the Canvas, anchored top-center
- [ ] BossHealthBar is **disabled** in the Hierarchy
- [ ] BossAI Inspector fields all filled in
- [ ] Boss is standing on the blue NavMesh

---

## 📌 Phase 8: Boss Trigger (start the fight)
**⏱️ Estimated time:** 10 – 15 minutes

### 🪜 Steps
1. Right-click `--- GAMEPLAY ---` → **3D Object → Cube**. Name it `BossTriggerVolume`.
2. Set its Scale to `(8, 4, 1)`. Place it just past the gate, blocking the entry into the arena.
3. In the Inspector:
   - **Disable the Mesh Renderer** component (uncheck it). The volume should be invisible.
   - On the **Box Collider**, check **Is Trigger** = ✔.
4. **Add Component → BossTrigger**.
5. Fill in the Inspector:
   - **Boss:** drag `KingBoss`
   - **Gate To Close:** drag `Gate`
   - **Intro UI:** (leave empty for now — we'll add a banner later if you want)
   - **Intro Duration:** `2`

### ✅ Checklist
- [ ] `BossTriggerVolume` placed past the gate
- [ ] Mesh Renderer disabled (volume is invisible)
- [ ] Box Collider has `Is Trigger` enabled
- [ ] `BossTrigger` references the boss and the gate
- [ ] Walking into the volume locks the gate and wakes the boss

---

## 📌 Phase 9: Win Condition
**⏱️ Estimated time:** 5 – 10 minutes

### 🪜 Steps
1. Confirm your scene has a **`GameManager`** GameObject (with the `GameManager` script). If not, drag it from Level 2's scene/prefabs.
2. Confirm your scene has a **`UIManager`** GameObject (with the `UIManager` script and a `WinPanel` child). Drag from Level 2 if missing.
3. That's it — the wiring is automatic. When the boss dies:
   - `BossAI.Die()` runs.
   - After `Win Delay` seconds it calls `GameManager.Instance.LevelComplete()`.
   - `GameManager` calls `UIManager.Instance.ShowWin()`, which enables your existing Win panel.

### ✅ Checklist
- [ ] `GameManager` GameObject exists in Level 3
- [ ] `UIManager` GameObject exists in Level 3
- [ ] `UIManager` has a `WinPanel` reference filled in (same as your other levels)
- [ ] Build Settings still includes Level 3

---

## 📌 Phase 10: Test & Polish
**⏱️ Estimated time:** 30 – 60 minutes

### 🪜 Steps
1. Press **Play** and run through the full level:
   - Walk forward → gate opens → walk through → gate closes → boss activates → fight → win screen.
2. **If something is broken**, check the "Common gotchas" list below.
3. **Tune the values** if the fight feels off:
   - Boss too easy? Raise `Max Health` to `300` or `Damage` to `30`.
   - Boss too hard? Lower the same numbers, raise `Attack Cooldown` to `2`.
   - Sword feels weak? Raise SwordCombat `Damage` to `35`.
4. (Optional polish — only after the fight works end-to-end):
   - Add the knight Animator with `Attack` / `Hit` / `Die` triggers.
   - Add a particle effect when the boss dies.
   - Add a sound effect on sword swing and boss hit.
   - Add the "Intro UI" banner that says "The Fallen King".

### Common gotchas
- **Boss doesn't move:** NavMesh isn't baked, or the boss isn't standing on it. Re-bake.
- **Sword doesn't damage boss:** SwordCombat's Player Camera field is empty, or the boss is too far away. Increase Attack Range.
- **Gate doesn't open:** Player isn't tagged `Player`, or Open Distance is too small.
- **Win screen doesn't appear:** Scene is missing `GameManager` or `UIManager`. Drag them in from Level 2.
- **Cursor visible during play:** something is unlocking it. Check that `MouseLook` runs and the pause panel isn't accidentally enabled at start.

### ✅ Checklist (final)
- [ ] Player can walk from spawn to gate
- [ ] Gate opens, player enters arena
- [ ] Gate closes, boss activates, health bar appears
- [ ] Sword damages the boss
- [ ] Boss damages the player
- [ ] Boss dies when health reaches 0
- [ ] Win screen appears after a couple seconds
- [ ] Restart and Main Menu buttons on Win panel work
- [ ] Level builds without errors (`Build Settings → Build`)

---

## 🧭 Phase Summary

| # | Phase | Time |
|---|---|---|
| 1 | Scene Setup | 15–25 min |
| 2 | Environment & Layout | 45–90 min |
| 3 | Lighting | 15–25 min |
| 4 | Player Setup | 20–30 min |
| 5 | Gate System | 15–20 min |
| 6 | NavMesh | 10–15 min |
| 7 | Boss Setup | 30–45 min |
| 8 | Boss Trigger | 10–15 min |
| 9 | Win Condition | 5–10 min |
| 10 | Test & Polish | 30–60 min |
| **Total** | | **~3.5 – 5.5 hrs** of focused work |

Build it phase by phase, **test after each phase**, and check the boxes. By the end of Phase 10 you'll have a fully working Level 3.
