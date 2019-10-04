using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using RimWorld;
using Verse;
using UnityEngine;
using System.IO;

namespace Nandonalt_ColonyLeadership
{


    
    [StaticConstructorOnStartup]
    public class ColonyLeadership
    {


        #region Variables
        public static String lastReadVersion = "none";
        public static String newVersion = "v1.5";        
        public static List<GovType> govtypes = new List<GovType>();
        public static GovType tempGov = null;
        public static GameInfo gameInfoTemp = null;
        #endregion
       
        public static String updateNotes = "";
        public static String helpNotes = "";

        //Uncomment for trace source debugging
        //private static TraceSource _source = new TraceSource("DebugLog");




        public static void doUpdateNotes()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("COLONY LEADERSHIP MOD  - VERSION 1.5.1 - UPDATE!");
            str.AppendLine("");
            str.AppendLine("What's new:");
            str.AppendLine("  - Resolved issue where colonist leadership icon was blocking inspiration icon");
            str.AppendLine("     -> Special thanks to Lethe for finding the fix.");
            str.AppendLine("  - Resolved issue where caravan traders were attending lessons");
            str.AppendLine("  - Future releases will be on adding new leadership types (Monarchy, Republic)");
            str.AppendLine("");
            str.AppendLine("-> This Update is brought to you by McKay, please report any bugs! ");
            str.AppendLine("-> Please support the original author! Nandonalt - https://www.patreon.com/nandonalt");

            updateNotes = str.ToString();

        }

        public static void doHelpNotes()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("FAQ:");
            str.AppendLine(" - How do I switch government types?");
            str.AppendLine("    -> Enter Development mode, and proceed to the Leadership Menu at the bottom ");
            str.AppendLine("       of the screen. From there, you will see that there are several new buttons");
            str.AppendLine("       prepended with DEV. Simply select the button that says Reset Leadership Type.");
            str.AppendLine("");
            str.AppendLine(" - My Colonists are gathering for elections but they don't actually vote, what's going on?");
            str.AppendLine("    -> Check your job schedule, if a colonist is scheduled to switch from one task to another");
            str.AppendLine("       during elections, they'll leave the election in progress and won't vote. It helps to set");
            str.AppendLine("       their work schedule to Any during elections.");
            str.AppendLine("");
            str.AppendLine("If you have any problems, comment on the steam workshop page and I'll do my best to address them!");
            str.AppendLine(" - McKay");

            helpNotes = str.ToString();
        }


        static ColonyLeadership()
        {

            try
            {
                Detour.Detours.TryDetourFromTo(typeof(ColonistBarColonistDrawer).GetMethod("DrawIcons", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance), typeof(Icon).GetMethod("DrawIconsModded"));
            }
            catch (Exception e)
            {
                //File.WriteAllText("logt.txt", e.Message.ToString());
            }

            govtypes.Add(new GovType("Democracy".Translate(), "DemocracyDesc".Translate(), "Leader".Translate()));
            govtypes.Add(new GovType("Dictatorship".Translate(), "DictatorshipDesc".Translate(), "Dictator".Translate()));
            govtypes.Add(new GovType("Monarchy", "MonarchyDesc", "Ruler"));
            bool flag = File.Exists(Path.Combine(GenFilePaths.SaveDataFolderPath, "ColonyLeadershipGlobal.xml"));
            if (flag)
            {
                try
                {
                    Scribe.loader.InitLoading(Path.Combine(GenFilePaths.SaveDataFolderPath, "ColonyLeadershipGlobal.xml"));
                    Scribe_Values.Look<String>(ref lastReadVersion, "lastReadVersion", "none", false);
                    Scribe.loader.FinalizeLoading();


                    //PostLoadIniter.DoAllPostLoadInits();
                }
                catch (Exception ex)
                {
                    //File.WriteAllText("logt.txt", ex.Message.ToString());
                    Log.Error("Exception loading colony leadership userdata: " + ex.ToString());
                    Scribe.ForceStop();
                }
            }

            if (ColonyLeadership.lastReadVersion != ColonyLeadership.newVersion)
            {
                DefDatabase<MainButtonDef>.GetNamed("LeaderTab").label = "(!) " + "LeadershipTab".Translate();
                doUpdateNotes();
            }
            doHelpNotes();
        }
        public static void Save()
        {
            try
            {
                string path = Path.Combine(GenFilePaths.SaveDataFolderPath, "ColonyLeadershipGlobal.xml");
                string label = "ColonyLeadershipGlobal";
                Action action;
                action = delegate () {
                    Scribe_Values.Look<String>(ref lastReadVersion, "lastReadVersion", "none", false);
                    //Scribe_Values.LookValue<int>(ref MaxSize, "MaxSize", 90, false);
                    //Scribe_Values.LookValue<bool>(ref permanentCamps, "permanentCamps", false, false);
                };
                SafeSaver.Save(path, label, action);
            }
            catch (Exception ex)
            {
                Log.Error("Exception while saving colony leadership userdata: " + ex.ToString());
            }
        }
    }
    [StaticConstructorOnStartup]
    public class ModTextures
    {
      

        #region Textures
        public static readonly Texture2D GreenTex = SolidColorMaterials.NewSolidColorTexture(Color.green);
        public static readonly Texture2D BlueColor = SolidColorMaterials.NewSolidColorTexture(Color.blue);
        public static readonly Texture2D RedColor = SolidColorMaterials.NewSolidColorTexture(Color.red);
        public static readonly Texture2D YellowColor = SolidColorMaterials.NewSolidColorTexture(Color.yellow);


        public static readonly Texture2D carpenter1 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/carpenter1", true);
        public static readonly Texture2D carpenter2 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/carpenter2", true);
        public static readonly Texture2D carpenter3 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/carpenter3", true);

        public static readonly Texture2D botanist1 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/botanist1", true);
        public static readonly Texture2D botanist2 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/botanist2", true);
        public static readonly Texture2D botanist3 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/botanist3", true);

        public static readonly Texture2D warrior1 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/warrior1", true);
        public static readonly Texture2D warrior2 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/warrior2", true);
        public static readonly Texture2D warrior3 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/warrior3", true);

        public static readonly Texture2D scientist1 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/scientist1", true);
        public static readonly Texture2D scientist2 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/scientist2", true);
        public static readonly Texture2D scientist3 = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/scientist3", true);

        public static readonly Texture2D waiting = ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/waiting", true);
        public static List<Texture2D> icons_leader1 = new List<Texture2D>();
        public static List<Texture2D> icons_leader2 = new List<Texture2D>();
        public static List<Texture2D> icons_leader3 = new List<Texture2D>();
        public static List<Texture2D> icons_leader4 = new List<Texture2D>();

        public static Graphic CH_Empty = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, ThingDef.Named("ChalkboardCL").graphicData.texPath, ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);

        public static Graphic CH_Botanist = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_botanist", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Botanist1 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_botanist1", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Botanist2 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_botanist2", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);

        public static Graphic CH_Warrior = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_warrior", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Warrior1 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_warrior1", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Warrior2 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_warrior2", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);

        public static Graphic CH_Carpenter = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_carpenter", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Carpenter1 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_carpenter1", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Carpenter2 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_carpenter2", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);

        public static Graphic CH_Scientist = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_scientist", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Scientist1 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_scientist1", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);
        public static Graphic CH_Scientist2 = GraphicDatabase.Get(ThingDef.Named("ChalkboardCL").graphicData.graphicClass, "ColonyLeadership/Chalkboard/ch_scientist2", ThingDef.Named("ChalkboardCL").graphic.Shader, ThingDef.Named("ChalkboardCL").graphicData.drawSize, ThingDef.Named("ChalkboardCL").graphicData.color, ThingDef.Named("ChalkboardCL").graphicData.colorTwo);



        #endregion textures        



        static ModTextures()
        {
            int botanistTextures = 3;
            int warriorTextures = 3;
            int scientistTextures = 3;
            int carpenterTextures = 3;

            for (int i = 1; i <= botanistTextures; i++)
            {
                icons_leader1.Add(ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/botanist" + i, true));
            }

            for (int i = 1; i <= warriorTextures; i++)
            {
                icons_leader2.Add(ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/warrior" + i, true));
            }

            for (int i = 1; i <= carpenterTextures; i++)
            {
                icons_leader3.Add(ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/carpenter" + i, true));
            }

            for (int i = 1; i <= scientistTextures; i++)
            {
                icons_leader4.Add(ContentFinder<Texture2D>.Get("ColonyLeadership/TeachingBubbles/scientist" + i, true));
            }

         

         

        }

          

      


    }
}
