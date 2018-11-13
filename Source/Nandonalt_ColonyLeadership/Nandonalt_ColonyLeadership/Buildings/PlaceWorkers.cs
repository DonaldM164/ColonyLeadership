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
            List<Thing> allBuildingsColonist = map.listerThings.AllThings;
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
            GenDraw.DrawFieldEdges(WatchBuildingUtility.CalculateWatchCells(def, center, rot, ourMap).ToList<IntVec3>());

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
                Thing thing = allBuildingsColonist[i];
                if (thing.def.defName == "BallotBox" || thing.def.defName == "BallotBox_Blueprint")
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

