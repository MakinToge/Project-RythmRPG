using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public enum GameState {
        StartMenu,
        Options,
        CharacterManagement,
        ModifyCharacter,
        GameMenu,
        Commands,
        SingleMusic,
        PlaylistChallenge,
        Victory,
        SongVictory,
        PlaylistVictory,
        PlaylistDefeat,
        Pause,
        Defeat,
        MusicPlaying
    }
}
