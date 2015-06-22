using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public enum GameState {
        /// <summary>
        /// The start menu
        /// </summary>
        StartMenu,
        /// <summary>
        /// The options page
        /// </summary>
        Options,
        /// <summary>
        /// The character management page
        /// </summary>
        CharacterManagement,
        /// <summary>
        /// The modify character page
        /// </summary>
        ModifyCharacter,
        /// <summary>
        /// The game menu
        /// </summary>
        GameMenu,
        /// <summary>
        /// The commands page
        /// </summary>
        Commands,
        /// <summary>
        /// The single music page
        /// </summary>
        SingleMusic,
        /// <summary>
        /// The playlist challenge page
        /// </summary>
        PlaylistChallenge,
        /// <summary>
        /// The victory page
        /// </summary>
        Victory,
        /// <summary>
        /// The song victory page
        /// </summary>
        SongVictory,
        /// <summary>
        /// The playlist victory page
        /// </summary>
        PlaylistVictory,
        /// <summary>
        /// The playlist defeat page
        /// </summary>
        PlaylistDefeat,
        /// <summary>
        /// The pause page
        /// </summary>
        Pause,
        /// <summary>
        /// The defeat page
        /// </summary>
        Defeat,
        /// <summary>
        /// The music playing page
        /// </summary>
        MusicPlaying,
        /// <summary>
        /// The exit state
        /// </summary>
        Exit
    }
}
