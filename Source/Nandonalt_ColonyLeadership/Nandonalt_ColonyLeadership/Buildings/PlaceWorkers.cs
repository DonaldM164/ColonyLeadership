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

        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
        {
            Map currentMap = Find.CurrentMap;
            List<Thing> allBuildingsColonist = currentMap.listerThings.AllThings;
            for (int i = 0; i < allBuildingsColonist.Count; i++)
            {
                Thing thing = allBuildingsColonist[i];
                if (thing.def.defName == "TeachingSpot" || thing.def.defName == "TeachingSpot_Blueprint")
                {
                    return new AcceptanceReport(reasonText: "OnlyOnePerColony".Translate(new object[] { thing.def.LabelCap }));
                }
            }

            return true;
        }

        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol)
        {
            Map currentMap = Find.CurrentMap;
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, currentMap).ToList<IntVec3>());
        }

        public class PlaceWorker_BallotBox : PlaceWorker
        {

            public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null)
            {
                Map currentMap = Find.CurrentMap;
                List<Thing> allBuildingsColonist = currentMap.listerThings.AllThings;
                for (int i = 0; i < allBuildingsColonist.Count; i++)
                {
                    Thing thing = allBuildingsColonist[i];
                    if (thing.def.defName == "BallotBox" || thing.def.defName == "BallotBox_Blueprint")
                    {
                        return new AcceptanceReport("OnlyOnePerColony".Translate(new object[] { thing.def.LabelCap }));
                    }
                }

                return true;
            }

        }
    }
}

