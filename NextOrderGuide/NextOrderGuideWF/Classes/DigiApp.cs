using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextOrderGuide;
using NextOrderGuide.Classes;

namespace NextOrderGuide.Classes
{
    [Serializable]
    public class DigiApp
    {
        public List<Digimon> DigimonList { get; set; }
        public List<Digivolution> DigivolveList { get; set; }
        public List<Fusion> FusionList { get; set; }
        public List<ExeFusion> ExeList { get; set; }
        public List<MistakeDigivolution> MistakeList { get; set; }

        public Digimon GetDigimonByName(string name)
        {
            return DigimonList.FirstOrDefault(t => t.Name == name);
        }

        public List<Digimon> GetDigimonContainingName(string name)
        {
            name = name.ToLower();
            return (from t in DigimonList where t.Name.ToLower().Contains(name.ToLower()) select (t)).ToList();
        }

        public List<Digimon> GetDigimonContainingNameByStage(string name, Digimon.MonsterStage stage)
        {
            name = name.ToLower();
            return (from t in DigimonList where t.Name.ToLower().Contains(name) && t.Stage == stage select (t)).ToList();
        }

        public List<Digimon> GetDigimonByStage(Digimon.MonsterStage stage)
        {
            return DigimonList.Where(t => t.Stage == stage).ToList();
        }

        public List<Digivolution> GetDigivolutionsFromDigimon(string name)
        {
            return (from t in DigivolveList where t.StartingName == name select (t)).ToList();
        }

        public List<Digivolution> GetDigivolutionsFromDigimon(Digimon digimon)
        {
            return GetDigivolutionsFromDigimon(digimon.Name);
        }

        public List<Digivolution> GetDigivolutionsInToDigimon(string name)
        {
            return DigivolveList.Where(t => t.FinalName == name).ToList();
        }

        public List<Digivolution> GetDigivolutionsInToDigimon(Digimon digimon)
        {
            return GetDigivolutionsInToDigimon(digimon.Name);
        }

        public List<Fusion> GetFusionsFromDigimon(string name)
        {
            return FusionList.Where(t => t.Partner1 == name || t.Partner2 == name).ToList();
        }

        public List<Fusion> GetFusionsFromDigimon(Digimon digimon)
        {
            return GetFusionsFromDigimon(digimon.Name);
        }

        public List<Fusion> GetFusionsIntoDigimon(string name)
        {
            return FusionList.Where(t => t.TargetName == name).ToList();
        }

        public List<Fusion> GetFusionsIntoDigimon(Digimon digimon)
        {
            return GetFusionsIntoDigimon(digimon.Name);
        }

        public List<ExeFusion> GetExeFusionsFromDigimon(string name)
        {
            return ExeList.Where(t => t.Partner1 == name || t.Partner2 == name).ToList();
        }

        public List<MistakeDigivolution> GetMistakeDigivolutionsByFinalStage(Digimon.MonsterStage stage)
        {
            return MistakeList.Where(t => t.FinalStage == stage).ToList();
        }

        public List<MistakeDigivolution> GetMistakeDigivolutionsByStartingStage(Digimon.MonsterStage stage)
        {
            return MistakeList.Where(t => t.StartingStage == stage).ToList();
        }
    }
}
