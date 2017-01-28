using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class Jogress
    {
        public string Partner1 { get; set; }
        public string Partner2 { get; set; }
        public string TargetName { get; set; }
        public Digimon.MonsterStage StartingStage { get; set; }
        public Digimon.MonsterStage FinalStage { get; set; }
        public string Notes { get; set; }

        public override string ToString()
        {
            return string.Format("{0} + {1} -> {2} ({3})", Partner1, Partner2, TargetName, FinalStage);
        }
    }
}
