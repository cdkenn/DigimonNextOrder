using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextOrderGuide.Classes;
using Microsoft.Office.Interop.Excel;

namespace NextOrderGuide.DigiAppIO
{
    /*
    * using this to parse my excel files that are my initial data source
    * after everything has been sucessfully parsed i'll be moving to xml
    */
    class ExcelReader
    {
        public List<Digimon> DigimonList { get; set; }
        public List<Digivolution> DigivolveList { get; set; }
        public List<Jogress> JogressList { get; set; }

        Application xlApp;
        Workbook xlWorkBook;
        object misValue = System.Reflection.Missing.Value;


        public ExcelReader(string path)
        {
            DigimonList = new List<Digimon>();
            DigivolveList = new List<Digivolution>();
            JogressList = new List<Jogress>();
            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ParseFiles();
        }

        public void ParseFiles()
        {
            Worksheet worksheet1 = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Worksheet worksheet2 = (Worksheet)xlWorkBook.Worksheets.get_Item(2);
            Worksheet worksheet3 = (Worksheet)xlWorkBook.Worksheets.get_Item(3);
            Worksheet worksheet4 = (Worksheet)xlWorkBook.Worksheets.get_Item(4);
            Worksheet worksheet5 = (Worksheet)xlWorkBook.Worksheets.get_Item(5);
            Worksheet worksheet6 = (Worksheet)xlWorkBook.Worksheets.get_Item(6);
            Worksheet worksheet7 = (Worksheet)xlWorkBook.Worksheets.get_Item(7);

            ParseB1B2(worksheet1);
            ParseB2Child(worksheet2);
            ParseChildAdult(worksheet3);
            ParseAdultPerfect(worksheet4);
            ParsePerfectUltimate(worksheet5);
            ParseSuperJogress(worksheet6);
            parseDigimonList(worksheet7);

        }

        public void ParseB1B2(Worksheet wrksht)
        {
            Range range;
            string cellVal;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= range.Rows.Count; rowCount++)
            {
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
                                Digimon digimon = new Digimon();
                                digimon.Name = startName;
                                digimon.Stage = Digimon.MonsterStage.BabyI;
                                //DigimonList.Add(digimon);
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
                dv.StartingStage = Digimon.MonsterStage.BabyI;
                dv.FinalStage = Digimon.MonsterStage.BabyII;
                DigivolveList.Add(dv);
            }
        }

        public void ParseB2Child(Worksheet wrksht)
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
                dv.StartingStage = Digimon.MonsterStage.BabyII;
                dv.FinalStage = Digimon.MonsterStage.Child;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    //Console.WriteLine(cellVal);
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal))
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = Digimon.MonsterStage.BabyII;
                                    //DigimonList.Add(d);
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
                                if (cellVal != null) dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if(cellVal!=null) dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null) dv.ATK = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null) dv.DEF = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null) dv.INT = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                if (cellVal != null) dv.SPD = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 9:
                            {
                                //quota
                                if (cellVal != null) dv.Quota = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseChildAdult(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            int rowMax = 118; //after this is where it goes to special dvs
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= rowMax; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Child;
                dv.FinalStage = Digimon.MonsterStage.Adult;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal))
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = Digimon.MonsterStage.Child;
                                    //DigimonList.Add(d);

                                    Digivolution numeDV = new Digivolution()
                                    {
                                        StartingName = startName,
                                        FinalName = "Numemon",
                                        StartingStage = Digimon.MonsterStage.Child,
                                        FinalStage = Digimon.MonsterStage.Adult,
                                        Notes = "When no other evolution conditions are met at age 7 (high ATK)"
                                    };
                                    Digivolution gereDV = new Digivolution()
                                    {
                                        StartingName = startName,
                                        FinalName = "Geremon",
                                        StartingStage = Digimon.MonsterStage.Child,
                                        FinalStage = Digimon.MonsterStage.Adult,
                                        Notes = "When no other evolution conditions are met at age 7 (high INT)"
                                    };
                                    Digivolution sukaDV = new Digivolution()
                                    {
                                        StartingName = startName,
                                        FinalName = "Sukamon",
                                        StartingStage = Digimon.MonsterStage.Child,
                                        FinalStage = Digimon.MonsterStage.Adult,
                                        Notes = "Forced to evolve into this when the curse (poop) gauge reaches max (Bond lower than 99)."
                                    };
                                    DigivolveList.Add(numeDV);
                                    DigivolveList.Add(gereDV);
                                    DigivolveList.Add(sukaDV);
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
                                if (cellVal != null) dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null) dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null) dv.ATK = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null) dv.DEF = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null) dv.INT = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                if (cellVal != null) dv.SPD = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
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
                                string str = cellVal?.ToString();
                                if (str !=null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.MistLT = true;
                                }
                                dv.Mist = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null) dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.DisLT = true;
                                }
                                dv.Dis = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null) dv.Battle = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null) dv.Key = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null) dv.Quota = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
        }

        public void ParseAdultPerfect(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            int rowMax = 127; //after this is where it goes to special dvs
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= rowMax; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Adult;
                dv.FinalStage = Digimon.MonsterStage.Perfect;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal))
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = Digimon.MonsterStage.Adult;
                                    //DigimonList.Add(d);
                                    
                                    Digivolution sukaDV = new Digivolution()
                                    {
                                        StartingName = startName,
                                        FinalName = "PlatinumSukamon",
                                        StartingStage = Digimon.MonsterStage.Adult,
                                        FinalStage = Digimon.MonsterStage.Perfect,
                                        Notes = "Forced to evolve into this when the curse (poop) gauge reaches max (100 Bond)."
                                    };
                                    DigivolveList.Add(sukaDV);
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
                                if (cellVal != null) dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null) dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null) dv.ATK = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null) dv.DEF = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null) dv.INT = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.SpdLT = true;
                                }
                                dv.SPD = Convert.ToInt32(str);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
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
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.MistLT = true;
                                }
                                dv.Mist = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null) dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.DisLT = true;
                                }
                                dv.Dis = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null) dv.Battle = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null) dv.Key = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null) dv.Quota = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
            //add jogress
            Jogress pailJog = new Jogress()
            {
                Partner1 = "XV-mon",
                Partner2 = "Stingmon",
                TargetName = "Paildramon",
                StartingStage = Digimon.MonsterStage.Adult,
                FinalStage = Digimon.MonsterStage.Perfect,
                Notes = "At Evolution Dojo, you can Jogress evolve into this (requires both Digimon)."
            };
            JogressList.Add(pailJog);
        }

        public void ParsePerfectUltimate(Worksheet wrksht)
        {
            Range range;
            //string cellVal;
            int rowCount = 0;
            int columnCount = 0;
            int rowMax = 108; //after this is where it goes to special dvs
            range = wrksht.UsedRange;
            string startName = "";
            //start on row 2 to avoid header row
            for (rowCount = 2; rowCount <= rowMax; rowCount++)
            {
                Digivolution dv = new Digivolution();
                dv.StartingStage = Digimon.MonsterStage.Perfect;
                dv.FinalStage = Digimon.MonsterStage.Ultimate;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //start name
                                if (!string.IsNullOrEmpty(cellVal))
                                {
                                    startName = cellVal;
                                    Digimon d = new Digimon();
                                    d.Name = startName;
                                    d.Stage = Digimon.MonsterStage.Perfect;
                                    //DigimonList.Add(d);
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
                                if (cellVal != null) dv.HP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 4:
                            {
                                //mp
                                if (cellVal != null) dv.MP = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 5:
                            {
                                //atk
                                if (cellVal != null) dv.ATK = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 6:
                            {
                                //def
                                if (cellVal != null) dv.DEF = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 7:
                            {
                                //int
                                if (cellVal != null) dv.INT = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 8:
                            {
                                //spd
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.SpdLT = true;
                                }
                                dv.SPD = Convert.ToInt32(str);
                                break;
                            }
                        case 9:
                            {
                                //weight
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
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
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.MistLT = true;
                                }
                                dv.Mist = Convert.ToInt32(str);
                                break;
                            }
                        case 11:
                            {
                                //bond
                                if (cellVal != null) dv.Bond = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 12:
                            {
                                //dis
                                //check for <
                                string str = cellVal?.ToString();
                                if (str != null && str.StartsWith("<"))
                                {
                                    str = str.Substring(1);
                                    dv.DisLT = true;
                                }
                                dv.Dis = Convert.ToInt32(str);
                                break;
                            }
                        case 13:
                            {
                                //battle
                                if (cellVal != null) dv.Battle = Convert.ToInt32(cellVal);
                                break;
                            }
                        case 14:
                            {
                                //key
                                if (cellVal != null) dv.Key = cellVal;
                                break;
                            }
                        case 15:
                            {
                                //quota
                                if (cellVal != null) dv.Quota = Convert.ToInt32(cellVal);
                                break;
                            }
                    }
                }
                DigivolveList.Add(dv);
            }
            //add jogress
            Jogress masteJog = new Jogress()
            {
                Partner1 = "Angewomon",
                Partner2 = "LadyDevimon",
                TargetName = "Mastemon",
                StartingStage = Digimon.MonsterStage.Perfect,
                FinalStage = Digimon.MonsterStage.Ultimate,
                Notes = "At Evolution Dojo, you can Jogress evolve into this (requires both Digimon)."
            };
            JogressList.Add(masteJog);
        }

        public void ParseSuperJogress(Worksheet wrksht)
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
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    cellVal = (string)(range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                //target name
                                target = cellVal;
                                break;
                            }
                        case 2:
                            {
                                //p1
                                p1 = cellVal;
                                break;
                            }
                        case 3:
                            {
                                //p2 list
                                string[] p2s = cellVal.Split('/');
                                for(int k = 0; k < p2s.Length; k++)
                                {
                                    string p2 = p2s[k];
                                    Jogress jog = new Jogress()
                                    {
                                        TargetName = target.Trim(),
                                        Partner1 = p1.Trim(),
                                        Partner2 = p2.Trim(),
                                        StartingStage = Digimon.MonsterStage.Ultimate,
                                        FinalStage = Digimon.MonsterStage.SuperJogress
                                    };
                                    JogressList.Add(jog);
                                }
                                break;
                            }
                    }
                }
            }
        }

        public void parseDigimonList(Worksheet wrksht)
        {
            Range range;
            int rowCount = 0;
            int columnCount = 0;

            range = wrksht.UsedRange;
            //start on row 2 to avoid header row
            for (rowCount = 1; rowCount <= range.Rows.Count; rowCount++)
            {
                string name = "";
                Digimon.MonsterStage stage = Digimon.MonsterStage.BabyI;
                int id = -1;
                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    var cellVal = (range.Cells[rowCount, columnCount] as Range).Value2;
                    switch (columnCount)
                    {
                        case 1:
                            {
                                name = cellVal;
                                break;
                            }
                        case 2:
                            {
                                stage = (Digimon.MonsterStage)Enum.Parse(typeof(Digimon.MonsterStage), cellVal);
                                break;
                            }
                        case 3:
                            {
                                id = (int)cellVal;
                                break;
                            }
                    }
                }
                Digimon d = new Digimon();
                d.ID = id;
                d.Name = name;
                d.Stage = stage;
                DigimonList.Add(d);
            }
            Console.WriteLine(DigimonList.Count);
        }
    }
}
