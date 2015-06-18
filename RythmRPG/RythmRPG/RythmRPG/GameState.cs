using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public enum GameState {
        StartMenu,
        Options,
        CharacterManagement,
        GameMenu,
        Commands,
        Achievements,
        HighScores,
        Credits,
        Loading,
        Playing,
        Paused
    }
}
