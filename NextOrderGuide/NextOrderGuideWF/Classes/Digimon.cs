using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class Digimon
    {
        public string Name { get; set; }
        public MonsterStage Stage { get; set; }
        public MonsterDigivolveType DigivolveType { get; set; }
        //public int Id { get; set; }

        public enum MonsterStage
        {
            Baby = 1,
            InTraining = 2,
            Rookie = 3,
            Champion = 4,
            Ultimate = 6,
            Mega = 7,
            Fusion = 8,
            ExE = 9
        }

        public enum MonsterDigivolveType
        {
            Normal = 1,
            Fusion = 2,
            Exe = 3,
            Mistake = 4
        }

        public override string ToString()
        {
            var stageName = Enum.GetName(typeof(MonsterStage), Stage);
            return $"{Name},{stageName}";
        }
    }
}
