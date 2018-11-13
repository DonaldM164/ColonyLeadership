using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace Nandonalt_ColonyLeadership
{
    public class PlaceWorker_TeachingSpot : PlaceWorker
    {
        Map ourMap;

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            List<Thing> allBuildingsColonist = map.listerThings.AllThings;
=======
            Map currentMap = Find.CurrentMap;
            List<Thing> allBuildingsColonist = currentMap.listerThings.AllThings;
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
=======
            Map currentMap = Find.CurrentMap;
            List<Thing> allBuildingsColonist = currentMap.listerThings.AllThings;
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
            for (int i = 0; i < allBuildingsColonist.Count; i++)
            {
                Thing thing = allBuildingsColonist[i];
                if (thing.def.defName == "TeachingSpot" || thing.def.defName == "TeachingSpot_Blueprint")
                {
                    return new AcceptanceReport(reasonText: "OnlyOnePerColony".Translate(new object[] { thing.def.LabelCap }));
                }
            }
            ourMap = map;

            return true;
        }

        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, ourMap).ToList<IntVec3>());

=======
            Map currentMap = Find.CurrentMap;
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, currentMap).ToList<IntVec3>());
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
=======
            Map currentMap = Find.CurrentMap;
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, currentMap).ToList<IntVec3>());
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
        }

    }

    public class PlaceWorker_BallotBox : PlaceWorker
    {
        Map ourMap;

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {

            List<Thing> allBuildingsColonist = map.listerThings.AllThings;
            for (int i = 0; i < allBuildingsColonist.Count; i++)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                Thing thing = allBuildingsColonist[i];
                if (thing.def.defName == "BallotBox" || thing.def.defName == "BallotBox_Blueprint")
=======
=======
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
                Map currentMap = Find.CurrentMap;
                List<Thing> allBuildingsColonist = currentMap.listerThings.AllThings;
                for (int i = 0; i < allBuildingsColonist.Count; i++)
>>>>>>> bec287482b5d4209ca7b3c4826f77b0e7a1882d0
                {
                    return new AcceptanceReport("OnlyOnePerColony".Translate(new object[] { thing.def.LabelCap }));
                }
            }

            return true;
        }

        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol)
        {
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, ourMap).ToList<IntVec3>());

        }
    }
}

