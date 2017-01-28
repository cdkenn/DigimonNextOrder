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
        public int ID { get; set; }

        public enum MonsterStage
        {
            BabyI = 1,
            BabyII = 2,
            Child = 3,
            Adult = 4,
            Perfect = 6,
            Ultimate = 7,
            SuperJogress = 8
        }
            
        public override string ToString()
        {
            string stageName = Enum.GetName(typeof(MonsterStage), Stage);
            return string.Format("{0},{1}", Name, stageName);
        }
    }
}
