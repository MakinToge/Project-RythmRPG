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
        /// Add 
        /// </summary>
        AttackMegaBoost,
        EnduranceMegaBoost,
        CriticalDamage,
        Paralysis,
        Cure,
        None
    }
}
