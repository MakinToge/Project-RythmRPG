using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character
{
    /// <summary>
    /// Unique skill that only a hero can have
    /// </summary>
    public enum UniqueSkill
    {
        /// <summary>
        /// Earn gold at the end of a level
        /// </summary>
        GoldDigger,
        /// <summary>
        /// Increase stat depending on the number of restart
        /// </summary>
        Experimentation,
        /// <summary>
        /// Use the defense stat instead of the attack stat to deal damage
        /// </summary>
        Berserker,
        /// <summary>
        /// Have a chance to kill the ennemy, regardless of his health and defense
        /// </summary>
        FatalBlow
    }
}
