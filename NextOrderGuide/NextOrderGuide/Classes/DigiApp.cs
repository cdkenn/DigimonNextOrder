using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class DigiApp
    {
        public List<Digimon> DigimonList { get; set; }
        public List<Digivolution> DigivolveList { get; set; }
        public List<Jogress> JogressList { get; set; }
    }
}
