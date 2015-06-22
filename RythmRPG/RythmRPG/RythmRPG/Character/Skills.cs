using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
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
        /// Add more strength to the character when attacking a boss
        /// PlayableCharacter use only
        /// </summary>
        AttackMegaBoost,
        /// <summary>
        /// Add more defense to the character when attacked by boss
        /// PlayableCharacter use only
        /// </summary>
        EnduranceMegaBoost,
        /// <summary>
        /// Chance to kill instantly the enemy
        /// </summary>
        CriticalDamage,
        /// <summary>
        /// Chance to paralyse the enemy for a short period of time
        /// </summary>
        Paralysis,
        /// <summary>
        /// Gives health points back when critically low
        /// </summary>
        Cure,
        /// <summary>
        /// None
        /// </summary>
        None
    }
}
