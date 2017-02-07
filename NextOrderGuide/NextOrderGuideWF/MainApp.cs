using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NextOrderGuide.Classes;
using NextOrderGuide.DigiAppIO;

namespace NextOrderGuide
{
    public partial class MainApp : Form
    {
        private DigiApp app;
        private Digimon selectedDigimon;

        public MainApp()
        {
            InitializeComponent();

            //ExcelReader reader = new ExcelReader(@"E:\Code\DigimonNextOrder\NextOrderGuide\NextOrderGuide\Datasets\NO PS4 Digivolve Guide.xlsx");
            //app = new DigiApp
            //{
            //    DigimonList = reader.DigimonList,
            //    DigivolveList = reader.DigivolveList,
            //    FusionList = reader.FusionList,
            //    ExeList = reader.ExeList,
            //    MistakeList = reader.MistakeList
            //};
            //DigiAppReadWrite.writeXML(app);

            app = DigiAppReadWrite.readXML();
            PopulateStagesCombo();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
            PopulateDigimonTable(app.DigimonList);
        }

        private void PopulateDigimonTable(List<Digimon> digimonList)
        {
            tblDigimon.Rows.Clear();
            foreach (var digimon in digimonList)
            {
                tblDigimon.Rows.Add(digimon.Name);
            }
            if (tblDigimon.RowCount > 0)
                tblDigimon[0, 0].Selected = true;
        }

        private void PopulateStagesCombo()
        {
            cmbStage.Items.Add("All Stages");
            cmbStage.Items.Add("Baby");
            cmbStage.Items.Add("In Training");
            cmbStage.Items.Add("Rookie");
            cmbStage.Items.Add("Champion");
            cmbStage.Items.Add("Ultimate");
            cmbStage.Items.Add("Mega");
            cmbStage.Items.Add("Fusion");
            //cmbStage.Items.Add("ExE");

            cmbStage.SelectedIndex = 0;
        }


        private void cmbStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }
        
        private void FillDigimonDetails()
        {
            FillInfoTab();
            FillDvFromTable();
            FillDvIntoTable();
            FillDvFromFusionTable();
            FillDvIntoFusionTable();
        }

        private void FillInfoTab()
        {
            lblSelectedName.Text = selectedDigimon.Name;

            string stageStr = "";
            switch (selectedDigimon.Stage)
            {
                case Digimon.MonsterStage.Baby:
                    {
                        stageStr = "Baby";
                        break;
                    }
                case Digimon.MonsterStage.InTraining:
                    {
                        stageStr = "In Training";
                        break;
                    }
                case Digimon.MonsterStage.Rookie:
                    {
                        stageStr = "Rookie";
                        break;
                    }
                case Digimon.MonsterStage.Champion:
                    {
                        stageStr = "Champion";
                        break;
                    }
                case Digimon.MonsterStage.Ultimate:
                    {
                        stageStr = "Ultimate";
                        break;
                    }
                case Digimon.MonsterStage.Mega:
                    {
                        stageStr = "Mega";
                        break;
                    }
                case Digimon.MonsterStage.Fusion:
                    {
                        stageStr = "Fusion";
                        break;
                    }
                case Digimon.MonsterStage.ExE:
                    {
                        stageStr = "ExE";
                        break;
                    }
            }
            lblSelectedStage.Text = stageStr;
        }

        private void tblDigimon_SelectionChanged(object sender, EventArgs e)
        {
            if (tblDigimon.SelectedCells.Count == 0) return;
            int rowIndex = tblDigimon.SelectedCells[0].RowIndex;
            int colIndex = tblDigimon.SelectedCells[0].ColumnIndex;
            if (rowIndex < 0) return;

            string name = tblDigimon[colIndex, rowIndex].Value.ToString();

            selectedDigimon = app.GetDigimonByName(name);
            FillDigimonDetails();
        }

        private void FillDvIntoTable()
        {
            List<Digivolution> dvList = app.GetDigivolutionsFromDigimon(selectedDigimon);

            tblDvInto.Rows.Clear();
            foreach (var dv in dvList)
            {
                var speedStr = dv.SpeedLT ? "≤" + dv.Speed.ToString() : dv.Speed.ToString();
                var weightStr = dv.WeightLT ? "≤" + dv.Weight.ToString() : dv.Weight.ToString();
                var mistakeStr = dv.MistakeLT ? "≤" + dv.Mistakes.ToString() : dv.Mistakes.ToString();
                var disciplinStr = dv.DisciplineLT ? "≤" + dv.Discipline.ToString() : dv.Discipline.ToString();

                tblDvInto.Rows.Add(dv.FinalName, dv.HP, dv.MP, dv.Strength, dv.Stamina, dv.Wisdom, speedStr, weightStr,
                    mistakeStr, dv.Bond, disciplinStr, dv.BattleWins, dv.KeyDigimon, dv.KeyPoints);
            }
        }

        private void FillDvFromTable()
        {
            List<Digivolution> dvList = app.GetDigivolutionsInToDigimon(selectedDigimon);

            tblDvFrom.Rows.Clear();
            foreach (var dv in dvList)
            {
                var speedStr = dv.SpeedLT ? "≤" + dv.Speed.ToString() : dv.Speed.ToString();
                var weightStr = dv.WeightLT ? "≤" + dv.Weight.ToString() : dv.Weight.ToString();
                var mistakeStr = dv.MistakeLT ? "≤" + dv.Mistakes.ToString() : dv.Mistakes.ToString();
                var disciplinStr = dv.DisciplineLT ? "≤" + dv.Discipline.ToString() : dv.Discipline.ToString();

                tblDvFrom.Rows.Add(dv.StartingName, dv.HP, dv.MP, dv.Strength, dv.Stamina, dv.Wisdom, speedStr, weightStr,
                    mistakeStr, dv.Bond, disciplinStr, dv.BattleWins, dv.KeyDigimon, dv.KeyPoints);
            }
        }

        private void FillDvIntoFusionTable()
        {
            List<Fusion> dvList = app.GetFusionsIntoDigimon(selectedDigimon);
            tblDvIntoFusion.Rows.Clear();
            foreach (var fusion in dvList)
            {
                tblDvIntoFusion.Rows.Add(fusion.Partner1, fusion.Partner2, fusion.TargetName);
            }
        }

        private void FillDvFromFusionTable()
        {
            List<Fusion> dvList = app.GetFusionsFromDigimon(selectedDigimon);
            tblDvFromFusion.Rows.Clear();
            foreach (var fusion in dvList)
            {
                tblDvFromFusion.Rows.Add(fusion.Partner1, fusion.Partner2, fusion.TargetName);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string name = txtSearch.Text.Trim();
            List<Digimon> searchList = new List<Digimon>();

            switch (cmbStage.SelectedIndex)
            {
                case 0:
                    {
                        searchList = app.GetDigimonContainingName(name);
                        break;
                    }
                case 1:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Baby);
                        break;
                    }
                case 2:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.InTraining);
                        break;
                    }
                case 3:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Rookie);
                        break;
                    }
                case 4:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Champion);
                        break;
                    }
                case 5:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Ultimate);
                        break;
                    }
                case 6:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Mega);
                        break;
                    }
                case 7:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.Fusion);
                        break;
                    }
                case 8:
                    {
                        searchList = app.GetDigimonContainingNameByStage(name, Digimon.MonsterStage.ExE);
                        break;
                    }
                default:
                    {
                        searchList = app.DigimonList;
                        break;
                    }
            }
            PopulateDigimonTable(searchList);
        }

        private void tblDvFrom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0) return;

            //get the name of the digimon in the row
            string name = tblDvFrom[0, rowIndex].Value.ToString();

            selectedDigimon = app.GetDigimonByName(name);
            FillDigimonDetails();
        }

        private void tblDvInto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex < 0) return;

            //get the name of the digimon in the row
            string name = tblDvInto[0, rowIndex].Value.ToString();

            selectedDigimon = app.GetDigimonByName(name);
            FillDigimonDetails();
        }

        private void tblDvIntoFusion_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex < 0 || colIndex < 0) return;
            
            string name = tblDvIntoFusion[colIndex, rowIndex].Value.ToString();

            selectedDigimon = app.GetDigimonByName(name);
            FillDigimonDetails();
        }

        private void tblDvFromFusion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;
            if (rowIndex < 0 || colIndex < 0) return;

            string name = tblDvFromFusion[colIndex, rowIndex].Value.ToString();

            selectedDigimon = app.GetDigimonByName(name);
            FillDigimonDetails();
        }
    }
}
