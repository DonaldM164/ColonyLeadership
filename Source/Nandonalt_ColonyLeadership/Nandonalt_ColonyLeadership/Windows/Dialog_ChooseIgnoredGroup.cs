using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Sound;
using System.Collections;

namespace Nandonalt_ColonyLeadership
{
    /**
     * A group of sixteen colonists is collected, and their respective check-mark option is returned to the ignored list. 
     **/
    class Dialog_ChooseIgnoredGroup : Window
    {
        public List<PawnIgnoreData> group = new List<PawnIgnoreData>();
        private Building_TeachingSpot spot;

        public Dialog_ChooseIgnoredGroup(List<PawnIgnoreData> group, ref Building_TeachingSpot spot)
        {
            this.forcePause = true;
            this.doCloseX = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
            this.group = group;
            this.spot = spot;
        }

        public override Vector2 InitialSize
        {
            get
            {
                float maxHeight = 15f * 25f;
                return new Vector2(280f, 130f + (maxHeight));
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Small;
            bool flag = false;

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                flag = true;
                Event.current.Use();
            }

            Listing_Standard ignoreList = new Listing_Standard();
            ignoreList.Begin(inRect);
            ignoreList.Label("IgnoreLectures".Translate());
            ignoreList.Gap(5f);

            foreach (PawnIgnoreData piData in this.group)
            {
                Pawn p = piData.reference;
                if (!p.Dead)
                {
                    ignoreList.CheckboxLabeled(p.LabelShort, ref piData.value);
                    //ignoreTempValue[ignoreTemp.IndexOf(p)] = b;
                }
            }
            if (ignoreList.ButtonText("OK".Translate(), null) || flag)
            {
                foreach (PawnIgnoreData piData in this.group)
                {

                    Pawn p = piData.reference;
                    if (spot.ignored.Contains(p))
                    {
                        if (piData.value == false)
                        {
                            spot.ignored.Remove(p);
                        }
                    }
                    else
                    {
                        if (piData.value == true)
                        {
                            spot.ignored.Add(p);
                        }
                    }
                }
                Find.WindowStack.TryRemove(this, true);
            }
            ignoreList.Gap(10f);
            ignoreList.End();
        }
    }
}
