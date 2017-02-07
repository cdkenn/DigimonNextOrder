using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class ExeFusion : Fusion
    {
        public new Digimon.MonsterStage FinalStage => Digimon.MonsterStage.ExE;
    }
}
