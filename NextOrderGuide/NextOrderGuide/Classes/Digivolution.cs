using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? ATK { get; set; }
        public int? DEF { get; set; }
        public int? INT { get; set; }
        public int? SPD { get; set; }
        public int? Weight { get; set; }
        public int? Mist { get; set; }
        public int? Bond { get; set; }
        public int? Dis { get; set; }
        public int? Battle { get; set; }
        public string Key { get; set; }
        public int? Quota { get; set; }

        public bool SpdLT { get; set; }
        public bool WeightLT { get; set; }
        public bool MistLT { get; set; } 
        public bool DisLT { get; set; } 

        public override string ToString()
        {
            return string.Format("{0} -> {1} ({2})", StartingName, FinalName, FinalStage);
        }
    }
}
