using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class MistakeDigivolution
    {
        public Digimon.MonsterStage StartingStage { get; set; }
        public Digimon.MonsterStage FinalStage { get; set; }

        public string FinalName { get; set; }
        public string Notes { get; set; }
    }
}
