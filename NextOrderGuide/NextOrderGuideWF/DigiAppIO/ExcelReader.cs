using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using NextOrderGuide.Classes;

namespace NextOrderGuide.DigiAppIO
{
    class ExcelReader
    {
        public List<Digimon> DigimonList { get; set; }
        public List<Digivolution> DigivolveList { get; set; }
        public List<Fusion> FusionList { get; set; }
        public List<ExeFusion> ExeList { get; set; }
        public List<MistakeDigivolution> MistakeList { get; set; }

        Application xlApp;
        Workbook xlWorkBook;
        object misValue = System.Reflection.Missing.Value;

        public ExcelReader(string path)
        {
            DigimonList = new List<Digimon>();
            DigivolveList = new List<Digivolution>();
            FusionList = new List<Fusion>();
            ExeList = new List<ExeFusion>();
            MistakeList = new List<MistakeDigivolution>();

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ParseFiles();
        }

        public void ParseFiles()
        {
            var worksheet1 = (Worksheet)xlWorkBook.Worksheets.Item[1];
            var worksheet2 = (Worksheet)xlWorkBook.Worksheets.Item[2];
            var worksheet3 = (Worksheet)xlWorkBook.Worksheets.Item[3];
            var worksheet4 = (Worksheet)xlWorkBook.Worksheets.Item[4];
            var worksheet5 = (Worksheet)xlWorkBook.Worksheets.Item[5];
            var worksheet6 = (Worksheet)xlWorkBook.Worksheets.Item[6];
            var worksheet7 = (Worksheet)xlWorkBook.Worksheets.Item[7];
            var worksheet8 = (Worksheet)xlWorkBook.Worksheets.Item[8];

            ParseBabyToInTraining(worksheet1);
            ParseInTrainingToRookie(worksheet2);
            ParseRookieToChampion(worksheet3);
            ParseChampionToUltimate(worksheet4);
            ParseUltimateToMega(worksheet5);
            ParseFusion(worksheet6);
            //ParseExE(worksheet7);
            ParseMistakeDigivolutions(worksheet8);
        }

        public void ParseBabyToInTraining(Worksheet wrksht)
        {
            Range range;
            string cellVal;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                //col 1 = name
                //col 2 = evo name
                string startName = "";
                string endName = "";
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    cellVal = (string)(range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                startName = cellVal;
                                Digimon d = new Digimon();
                                d.Name = startName;
                                d.Stage = Digimon.MonsterStage.Baby;
                                d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                DigimonList.Add(d);
                                break;
                            }
                        case 2:
                            {
                                endName = cellVal;
                                break;
                            }
                    }
                }
                Digivolution dv = new Digivolution();
                dv.StartingName = startName;
                dv.FinalName = endName;
                dv.StartingStage = Digimon.MonsterStage.Baby;
                dv.FinalStage = Digimon.MonsterStage.InTraining;
                DigivolveList.Add(dv);
            }
        }

        public void ParseInTrainingToRookie(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.InTraining;
                dv.FinalStage = Digimon.MonsterStage.Rookie;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal) && cellVal != "-")
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = dv.StartingStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                    DigimonList.Add(d);
                                }
                                dv.StartingName = startName;
                                break;
                            }
                        case 2:
                            {
                                //end name
                                dv.FinalName = cellVal;
                                break;
                            }
                        case 3:
                            {
                                //hp
                                if (cellVal != null && cellVal.ToString() != "-") dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null && cellVal.ToString() != "-") dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //strength
                                if (cellVal != null && cellVal.ToString() != "-") dv.Strength = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //stamina
                                if (cellVal != null && cellVal.ToString() != "-") dv.Stamina = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //wisdom
                                if (cellVal != null && cellVal.ToString() != "-") dv.Wisdom = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //speed
                                if (cellVal != null && cellVal.ToString() != "-") dv.Speed = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 15:
                            {
                                //KeyPoints
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyPoints = Convert.ToInt32(cellVal);

                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseRookieToChampion(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Rookie;
                dv.FinalStage = Digimon.MonsterStage.Champion;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal) && cellVal.ToString() != "-")
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = dv.StartingStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                    DigimonList.Add(d);
                                }
                                dv.StartingName = startName;
                                break;
                            }
                        case 2:
                            {
                                //end name
                                dv.FinalName = cellVal;
                                break;
                            }
                        case 3:
                            {
                                //hp
                                if (cellVal != null && cellVal.ToString() != "-") dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null && cellVal.ToString() != "-") dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null && cellVal.ToString() != "-") dv.Strength = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null && cellVal.ToString() != "-") dv.Stamina = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null && cellVal.ToString() != "-") dv.Wisdom = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                if (cellVal != null && cellVal.ToString() != "-") dv.Speed = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.WeightLT = true;
                                }
                                dv.Weight = Convert.ToInt32(str);
                                break;
                            }
                        case 10:
                            {
                                //mist
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.MistakeLT = true;
                                }
                                dv.Mistakes = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null && cellVal.ToString() != "-") dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.DisciplineLT = true;
                                }
                                dv.Discipline = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null && cellVal.ToString() != "-") dv.BattleWins = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyDigimon = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyPoints = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseChampionToUltimate(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Champion;
                dv.FinalStage = Digimon.MonsterStage.Ultimate;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal) && cellVal.ToString() != "-")
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = dv.StartingStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                    DigimonList.Add(d);
                                }
                                dv.StartingName = startName;
                                break;
                            }
                        case 2:
                            {
                                //end name
                                dv.FinalName = cellVal;
                                break;
                            }
                        case 3:
                            {
                                //hp
                                if (cellVal != null && cellVal.ToString() != "-") dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null && cellVal.ToString() != "-") dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null && cellVal.ToString() != "-") dv.Strength = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null && cellVal.ToString() != "-") dv.Stamina = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null && cellVal.ToString() != "-") dv.Wisdom = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                if (cellVal != null && cellVal.ToString() != "-") dv.Speed = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.WeightLT = true;
                                }
                                dv.Weight = Convert.ToInt32(str);
                                break;
                            }
                        case 10:
                            {
                                //mist
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.MistakeLT = true;
                                }
                                dv.Mistakes = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null && cellVal.ToString() != "-") dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.DisciplineLT = true;
                                }
                                dv.Discipline = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null && cellVal.ToString() != "-") dv.BattleWins = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyDigimon = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyPoints = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseUltimateToMega(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Ultimate;
                dv.FinalStage = Digimon.MonsterStage.Mega;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal) && cellVal.ToString() != "-")
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = dv.StartingStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                    DigimonList.Add(d);
                                }
                                dv.StartingName = startName;
                                break;
                            }
                        case 2:
                            {
                                //end name
                                dv.FinalName = cellVal;
                                bool alreadyInDex = false;
                                for (int i = DigimonList.Count - 1; i >= 0; i--)
                                {
                                    if (DigimonList[i].Name == dv.FinalName)
                                    {
                                        alreadyInDex = true;
                                        break;
                                    }
                                }
                                if (!alreadyInDex)
                                {
                                    Digimon d = new Digimon();
                                    d.Name = dv.FinalName;
                                    d.Stage = dv.FinalStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Normal;
                                    DigimonList.Add(d);
                                }
                                break;
                            }
                        case 3:
                            {
                                //hp
                                if (cellVal != null && cellVal.ToString() != "-") dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null && cellVal.ToString() != "-") dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null && cellVal.ToString() != "-") dv.Strength = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null && cellVal.ToString() != "-") dv.Stamina = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null && cellVal.ToString() != "-") dv.Wisdom = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                if (cellVal != null && cellVal.ToString() != "-") dv.Speed = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.WeightLT = true;
                                }
                                dv.Weight = Convert.ToInt32(str);
                                break;
                            }
                        case 10:
                            {
                                //mist
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.MistakeLT = true;
                                }
                                dv.Mistakes = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null && cellVal.ToString() != "-") dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                if (cellVal == null || cellVal.ToString() == "-") break;
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("≤"))
                                {
                                    str = str.Substring(1);
                                    dv.DisciplineLT = true;
                                }
                                dv.Discipline = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null && cellVal.ToString() != "-") dv.BattleWins = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyDigimon = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null && cellVal.ToString() != "-") dv.KeyPoints = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseFusion(Worksheet wrksht)
        {
            Range range;
            string cellVal;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                string target = "";
                string p1 = "";
                string p2 = "";
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    cellVal = (string)(range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //p1
                                p1 = cellVal.ToString();
                                break;
                            }
                        case 2:
                            {
                                //p2
                                p2 = cellVal.ToString();
                                break;
                            }
                        case 3:
                            {
                                //target
                                target = cellVal.ToString();
                                break;
                            }
                        case 4:
                            {
                                //level
                                Digimon.MonsterStage startingStage = Digimon.MonsterStage.Mega;
                                Digimon.MonsterStage finalStage = Digimon.MonsterStage.Fusion;
                                switch (cellVal)
                                {
                                    case "Mega":
                                        {
                                            startingStage = Digimon.MonsterStage.Ultimate;
                                            finalStage = Digimon.MonsterStage.Mega;
                                            break;
                                        }
                                    case "Ultimate":
                                        {
                                            startingStage = Digimon.MonsterStage.Champion;
                                            finalStage = Digimon.MonsterStage.Ultimate;
                                            break;
                                        }
                                }
                                bool alreadyInDex = false;
                                for (int i = DigimonList.Count - 1; i >= 0; i--)
                                {
                                    if (DigimonList[i].Name == target)
                                    {
                                        alreadyInDex = true;
                                        break;
                                    }
                                }
                                if (!alreadyInDex)
                                {
                                    Digimon d = new Digimon();
                                    d.Name = target;
                                    d.Stage = finalStage;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Fusion;
                                    DigimonList.Add(d);
                                }

                                Fusion f = new Fusion();
                                f.Partner1 = p1;
                                f.Partner2 = p2;
                                f.TargetName = target;
                                f.StartingStage = startingStage;
                                f.FinalStage = finalStage;
                                FusionList.Add(f);
                                break;
                            }
                    }
                }
            }
        }

        public void ParseExE(Worksheet wrksht)
        {
            Range range;
            string cellVal;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                string target = "";
                string p1 = "";
                string p2 = "";
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    cellVal = (string)(range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //p1
                                p1 = cellVal.ToString();
                                break;
                            }
                        case 2:
                            {
                                //p2
                                p2 = cellVal.ToString();
                                break;
                            }
                        case 3:
                            {
                                //target
                                target = cellVal.ToString();
                                ExeFusion f = new ExeFusion();
                                f.Partner1 = p1;
                                f.Partner2 = p2;
                                f.TargetName = target;
                                f.StartingStage = Digimon.MonsterStage.Mega;
                                ExeList.Add(f);

                                bool alreadyInDex = false;
                                for (int i = DigimonList.Count - 1; i >= 0; i--)
                                {
                                    if (DigimonList[i].Name == target)
                                    {
                                        alreadyInDex = true;
                                        break;
                                    }
                                }
                                if (!alreadyInDex)
                                {
                                    Digimon d = new Digimon();
                                    d.Name = target;
                                    d.Stage = Digimon.MonsterStage.ExE;
                                    d.DigivolveType = Digimon.MonsterDigivolveType.Exe;
                                    DigimonList.Add(d);
                                }
                                break;
                            }
                    }
                }
            }
        }

        public void ParseMistakeDigivolutions(Worksheet wrksht)
        {
            Range range;
            string cellVal;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
                string target = "";
                Digimon.MonsterStage startingStage = Digimon.MonsterStage.Rookie;
                Digimon.MonsterStage finalStage = Digimon.MonsterStage.Champion;
                string notes = "";
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    cellVal = (string)(range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                target = cellVal;
                                break;
                            }
                        case 2:
                            {
                                if (cellVal == "Ultimate")
                                {
                                    startingStage = Digimon.MonsterStage.Champion;
                                    finalStage = Digimon.MonsterStage.Ultimate;
                                }
                                break;
                            }
                        case 3:
                            {
                                notes = cellVal;
                                MistakeDigivolution misDv = new MistakeDigivolution();
                                misDv.FinalName = target;
                                misDv.StartingStage = startingStage;
                                misDv.FinalStage = finalStage;
                                misDv.Notes = notes;
                                MistakeList.Add(misDv);

                                Digimon d = new Digimon();
                                d.Name = target;
                                d.Stage = finalStage;
                                d.DigivolveType = Digimon.MonsterDigivolveType.Mistake;
                                DigimonList.Add(d);
                                break;
                            }
                    }
                }
            }
        }
    }
}
