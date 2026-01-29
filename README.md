# MissionFramework

A lightweight, narrative-driven mission framework for Terraria mods.  
Designed to make story, structure, and gameplay progression live in the same data space.

This is a **framework library**. It provides infrastructure, *not content*.

## Features

- Declarative mission definitions
- Event-driven triggers
- Multiplayer-safe mission tracking
- Serializable mission state
- Designed to integrate cleanly with Terraria mod APIs

### Mission
A mission is a container for:
- Conditions
- Event routing
- Rewards

MissionFramework also includes built-in support for:

- Cutscene sequencing
- Dialogue sequencing

These systems are optional but designed to plug directly into mission progression, allowing story, dialogue, and scripted events to be driven by the same deterministic state machine.

## Cutscene System

The Cutscene System is a deterministic state controller that temporarily takes control of the game loop to execute scripted narrative sequences.

It is responsible for:
- Input locking
- UI suppression
- Camera and player control
- World manipulation
- Music transitions
- Screen fading
- Letterboxing
- Skip handling
- Multiplayer-safe execution

Only one cutscene may run at a time.

The CutsceneSystem owns:

- Cutscene lifecycle
- Rendering order
- Update order
- UI layer suppression
- Skip logic
- Transition cleanup

Game code never manipulates UI or controls directly during a cutscene.
All authority passes through the Cutscene base class.


## Dialogue System

The Dialogue System is a state-driven controller that manages in-game conversations.

It is responsible for:
- Dialogue sequencing and queuing
- Typewriter text effects
- Character portraits and emotes
- Audio voice ticks and sound effects
- Screen letterboxing
- Camera zoom control
- Music override and restoration
- Input handling
- Dialogue completion events

### Architecture

Dialogue is split into three layers:

1. DialogueManager  
   Owns lifecycle, queueing, zoom, letterbox, and music control.

2. DialogueBox  
   Owns rendering, animation, typewriter effects, and interaction.

3. DialogueBuilder  
   Builds dialogue data from localization (.hjson) and parses effects.

### Dialogue Authoring

Dialogue is authored entirely through localization files.

Each dialogue entry is composed of:
- Speaker type
- Emote index
- Text content
- Speed modifier
- Inline effects
- Sound behavior

Example:
```
DialogueLibrary.Intro.Line1 {
  Speaker: "Guide"
  Emote: 2
  Speed: 1.2
  Text: "You survived the night?! That already puts you ahead of most."
}
```

### Inline Dialogue Effects

Dialogue text supports markup tags that modify pacing and presentation:

| Tag | Description |
|------|------------|
| `<pause:60>` | Pauses typing for 60 frames |
| `<shake:int> </shake>` | Applies shake effect to text |
| `<sine:int> </sine>` | Applies vertical sine motion |
| `<fast:int> </fast>` | Temporarily increases typing speed |
| `<slow:int> </slow>` | Temporarily decreases typing speed |
| `<pitch:double>` | Modifies voice pitch for characters |

### DialogueBox

DialogueBox is the visual and interactive part of dialogue.

It handles:
- Panel animation in/out
- Portrait rendering
- Speaker name rendering
- Typewriter effect
- Text wrapping
- Skip handling
- Mouse and keyboard interaction
- Per-character effects
