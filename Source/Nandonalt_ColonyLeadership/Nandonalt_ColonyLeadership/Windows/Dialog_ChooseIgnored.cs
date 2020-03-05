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
    public class Dialog_ChooseIgnored : Window
    {
        protected string curName;
        public List<Pawn> ignoreTemp;
        public List<PawnIgnoreData> tempPawnList = new List<PawnIgnoreData>();
        public int MaxSize;
        public int MinSize;
        public string MaxSizebuf;
        public string MinSizebuf;
        public bool permanent;
        private Building_TeachingSpot spot;

        
        
        private Vector2 scrollPosition = Vector2.zero;
        private float scrollViewHeight;


        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(280, 130 + (tempPawnList.Count * 25));
            }
        }

        public Dialog_ChooseIgnored(Building_TeachingSpot spot)
        {
            this.forcePause = true;
            this.doCloseX = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
            this.spot = spot;
            this.ignoreTemp = IncidentWorker_SetLeadership.getAllColonists();


            foreach (Pawn p in this.ignoreTemp)
            {
                if (this.spot.ignored.Contains(p))
                {
                    tempPawnList.Add(new PawnIgnoreData(p, true));
                }
                else
                {
                    tempPawnList.Add(new PawnIgnoreData(p, false));
                }
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

            Rect position = new Rect(0f, 0f, inRect.width, inRect.height);
            GUI.BeginGroup(position);
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            Widgets.Label(new Rect(5f, 5f, 140f, 30f), "Choose Ignore Pawns");

            Rect outRect = new Rect(0f, 50f, position.width, position.height - 50f);
            

            Rect rect = new Rect(0f, 0f, position.width - 16f, this.scrollViewHeight);
            Widgets.BeginScrollView(outRect, ref this.scrollPosition, rect);


            float num = 0f;
            foreach (PawnIgnoreData temp in tempPawnList)
            {
                Pawn p = temp.reference;
                if (!p.Dead)
                {
                    GUI.color = new Color(1f, 1f, 1f, 0.2f);
                    Widgets.DrawLineHorizontal(0f, num, rect.width);
                    GUI.color = Color.white;
                    num += this.DrawIgnorePawnRow(p, num, rect);
                }


            }
            if (Event.current.type == EventType.Layout)
            {
                this.scrollViewHeight = num;
            }

            Widgets.EndScrollView();
            GUI.EndGroup();
            /**
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);

            listing_Standard.Label("IgnoreLectures".Translate());
            listing_Standard.Gap(5f);
            **/
            /**
            foreach (PawnIgnoreData temp in tempPawnList)
            {
                Pawn p = temp.reference;
                if (!p.Dead)
                {
                    //listing_Standard.CheckboxLabeled(p.LabelShort, ref temp.value);
                    //ignoreTempValue[ignoreTemp.IndexOf(p)] = b;
                }
            }
            if (listing_Standard.ButtonText("OK".Translate(), null) || flag)
            {
                foreach (PawnIgnoreData temp in tempPawnList)
                {
                    Pawn p = temp.reference;
                    if (spot.ignored.Contains(p))
                    {
                        if (temp.value == false)
                        {
                            spot.ignored.Remove(p);
                        }
                    }
                    else
                    {
                        if (temp.value == true)
                        {
                            spot.ignored.Add(p);
                        }
                    }
                }
                Find.WindowStack.TryRemove(this, true);
            }
            listing_Standard.Gap(10f);
            listing_Standard.End();
            **/
        }


        private float DrawIgnorePawnRow(Pawn ignorePawn, float rowY, Rect fillRect)
        {
            Rect rect = new Rect(40f, rowY, 300f, 80f);
            //Need_LeaderLevel need = (Need_LeaderLevel)leader.needs.AllNeeds.Find((Need x) => x.def == DefDatabase<NeedDef>.GetNamed("LeaderLevel"));
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("");
            string text = stringBuilder.ToString();
            float width = fillRect.width - rect.xMax;
            float num = Text.CalcHeight(text, width);
            float num2 = Mathf.Max(80f, num);
            Rect position = new Rect(8f, rowY + 12f, 30f, 30f);
            Rect rect2 = new Rect(0f, rowY, fillRect.width, num2);
            if (Mouse.IsOver(rect2))
            {
                StringBuilder stringBuilder2 = new StringBuilder();
                //stringBuilder2.AppendLine("AverageOpinionAbout".Translate() + need.opinion);


                //if (need.opinion < -60 && !Utility.isDictatorship) stringBuilder2.AppendLine("UnpopularLeader".Translate());
                //else if (need.opinion < -20) stringBuilder2.AppendLine("UnlikedLeader".Translate());
                TooltipHandler.TipRegion(rect2, stringBuilder2.ToString());
                GUI.DrawTexture(rect2, TexUI.HighlightTex);
                if (Event.current.type == EventType.MouseDown)
                {
                    if (Event.current.button == 0)
                    {
                        //CameraJumper.TryJumpAndSelect(leader);
                    }
                }
            }
            Text.Font = GameFont.Medium;

            Text.Anchor = TextAnchor.UpperLeft;
            //Widgets.ThingIcon(position, leader, 1f);

            //Widgets.DrawRectFast(position, Color.white, null);
            string label = string.Concat(new string[]
            {
                /**
                leader.Name.ToStringFull,
                "\n",
                "   ",
                leaderType(leader),
                "\n"
                **/
            });
            /**
            if (need.opinion < -20) GUI.color = Color.yellow;
            if (need.opinion < -60) GUI.color = Color.red;
            **/
            Widgets.Label(rect, label);
            GUI.color = Color.white;
            return num2;
        }

    }

    public class PawnIgnoreData
    {
        public bool value;
        public Pawn reference;

        public PawnIgnoreData(Pawn re, bool val = false)
        {
            this.value = val;
            this.reference = re;
        }

    }



}