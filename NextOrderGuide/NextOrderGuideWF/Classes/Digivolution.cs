using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextOrderGuide.Classes;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class Digivolution
    {
        public Digimon.MonsterStage StartingStage { get; set; }
        public Digimon.MonsterStage FinalStage { get; set; }

        public string StartingName { get; set; }
        public string FinalName { get; set; }
        public string Notes { get; set; }
        public int? HP { get; set; }
        public int? MP { get; set; }
        public int? Strength { get; set; }
        public int? Stamina { get; set; }
        public int? Wisdom { get; set; }
        public int? Speed { get; set; }
        public int? Weight { get; set; }
        public int? Mistakes { get; set; }
        public int? Bond { get; set; }
        public int? Discipline { get; set; }
        public int? BattleWins { get; set; }
        public string KeyDigimon { get; set; }
        public int? KeyPoints { get; set; }

        public bool SpeedLT { get; set; }
        public bool WeightLT { get; set; }
        public bool MistakeLT { get; set; }
        public bool DisciplineLT { get; set; }

        public override string ToString()
        {
            return $"{StartingName} -> {FinalName} ({FinalStage})";
        }
    }
}
