using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG.Character.Skill
{
    class AbstractSkill
    {
        private AbstractCharacter character { get; set; }

        public AbstractSkill(AbstractCharacter character)
        {
            this.character = character;
        }
    }
}
