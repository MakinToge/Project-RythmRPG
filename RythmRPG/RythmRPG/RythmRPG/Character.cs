using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RythmRPG {
    public class Character {
        public string Name { get; set; }
        public CharacterType Type { get; set; }
        public string AssetName { get; set; }
        public int Level { get; set; }
        public int HealthPoints { get; set; }
        public int StrengthPoints { get; set; }
        public int EndurancePoints { get; set; }
        public int StatsPoints { get; set; }
        public int TotalXP { get; set; }
        public int XP { get; set; }
        public int[] ArrayLevelXP { get; set; }
        public string[] SkillsActives { get; set; }
        public int ReachLevelMax { get; set; }
        public int Gold { get; set; }
        public int Vitality { get; set; }
        public string Abilility { get; set; }

        public Character(string name, CharacterType type, string assetName, int level, int vitality, int strength, int endurance, int statsPoints, int totalXP, int XP, int reachLevelMax) {
            this.Name = name;
            this.Type = type;
            this.AssetName = assetName;
            this.Level = level;
            this.HealthPoints = vitality;
            this.Vitality = vitality;
            this.StrengthPoints = strength;
            this.EndurancePoints = endurance;
            this.StatsPoints = statsPoints;
            this.TotalXP = totalXP;
            this.XP = XP;
            this.ReachLevelMax = reachLevelMax;

            this.Abilility = "Special Ability";
        }
    }
}
