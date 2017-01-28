using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NextOrderGuide.Classes;
using System.Xml.Serialization;
using System.Xml;
using NextOrderGuide.DigiAppIO;

namespace NextOrderGuide
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //string path = "NO Digivolve Guide.xlsx";
            //ExcelReader.ExcelReader er = new ExcelReader.ExcelReader(path);
            ////var mons = parseDVListToDigiDex(er.DigivolveList, er.JogressList);

            //DigiApp digiapp = new DigiApp()
            //{
            //    DigimonList = er.DigimonList,
            //    DigivolveList = er.DigivolveList,
            //    JogressList = er.JogressList
            //};
            //DigiAppReadWrite.writeDigiAppXML(digiapp);
            DigiApp myApp = DigiAppReadWrite.readXML();
        }

        

        public List<Digimon> parseDVListToDigiDex(List<Digivolution> dvList, List<Jogress> jogList)
        {
            List<Digimon> monList = new List<Digimon>();
            foreach(var dv in dvList)
            {
                string startMon = dv.StartingName;
                string finalMon = dv.FinalName;
                bool hasStart = false;
                bool hasFinal = false;
                foreach(var mon in monList)
                {
                    if (mon.Name == startMon)
                    {
                        hasStart = true;
                        break;
                    }
                }
                if (!hasStart)
                {
                    Digimon toAdd = new Digimon()
                    {
                        Name = startMon,
                        Stage = dv.StartingStage
                    };
                    monList.Add(toAdd);
                }

                foreach (var mon in monList)
                {
                    if (mon.Name == finalMon)
                    {
                        hasFinal = true;
                        break;
                    }
                }
                if (!hasFinal)
                {
                    Digimon toAdd = new Digimon()
                    {
                        Name = finalMon,
                        Stage = dv.FinalStage
                    };
                    monList.Add(toAdd);
                }
            }

            foreach(var jog in jogList)
            {
                string p1Mon = jog.Partner1;
                string p2Mon = jog.Partner2;
                string targetMon = jog.TargetName;

                bool hasP1 = false;
                bool hasP2 = false;
                bool hasTarget = false;

                foreach(var mon in monList)
                {
                    if (mon.Name == p1Mon)
                    {
                        hasP1 = true;
                        break;
                    }
                }
                if (!hasP1)
                {
                    Digimon toAdd = new Digimon()
                    {
                        Name = p1Mon,
                        Stage = jog.StartingStage
                    };
                    monList.Add(toAdd);
                }
                //p2
                foreach (var mon in monList)
                {
                    if (mon.Name == p2Mon)
                    {
                        hasP2 = true;
                        break;
                    }
                }
                if (!hasP2)
                {
                    Digimon toAdd = new Digimon()
                    {
                        Name = p2Mon,
                        Stage = jog.StartingStage
                    };
                    monList.Add(toAdd);
                }
                //target
                foreach (var mon in monList)
                {
                    if (mon.Name == targetMon)
                    {
                        hasTarget = true;
                        break;
                    }
                }
                if (!hasTarget)
                {
                    Digimon toAdd = new Digimon()
                    {
                        Name = targetMon,
                        Stage = jog.FinalStage
                    };
                    monList.Add(toAdd);
                }
            }
            Console.WriteLine(monList.Count);
            foreach(var mon in monList)
            {
                Console.WriteLine(mon);
            }
            return monList;
        }
    }
}
