using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// Skills that a character can have
    /// </summary>
    public enum Skills
    {
        /// <summary>
        /// Add attack to the character
        /// </summary>
        AttackBoost,
        /// <summary>
        /// Add defense to the character
        /// </summary>
        DefenseBoost,
        /// <summary>
        /// Add vitality to the character
        /// </summary>
        VitalityBoost,
        /// <summary>
        /// The character earns more experience at the end of a level
        /// </summary>
        ExperienceBoost,
        /// <summary>
        /// Add more attack to the character only when attacking a boss
        /// </summary>
        AttackMegaBoost,
        /// <summary>
        /// Add more defense to the character only when attacked by a boss
        /// </summary>
        DefenseMegaBoost,
        /// <summary>
        /// Do more damage after a successful combo
        /// </summary>
        CriticalDamage,
        /// <summary>
        /// Paralyse a boss after a sccessful combo
        /// </summary>
        Paralysis,
        /// <summary>
        /// Restore health when critically low, only one time during a level
        /// </summary>
        Cure
    }
}
