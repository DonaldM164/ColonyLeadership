﻿using System;
using System.Collections.Generic;
using Verse;
using Verse.AI.Group;
using RimWorld;

namespace Nandonalt_ColonyLeadership
{
    public class LordJob_Joinable_SetLeadership : LordJob_VoluntarilyJoinable
    {
        private IntVec3 spot;

        private Trigger_TicksPassed timeoutTrigger;        

        public LordJob_Joinable_SetLeadership()
        {
        }

        public LordJob_Joinable_SetLeadership(IntVec3 spot)
        {
            this.spot = spot;
        }

        public override StateGraph CreateGraph()
        {
            StateGraph stateGraph = new StateGraph();
            LordToil_SetLeadership lordToil_Party = new LordToil_SetLeadership(this.spot);
            stateGraph.AddToil(lordToil_Party);
            LordToil_End lordToil_End = new LordToil_End();
            stateGraph.AddToil(lordToil_End);
            Transition transition = new Transition(lordToil_Party, lordToil_End);
            transition.AddTrigger(new Trigger_TickCondition(() => this.ShouldBeCalledOff()));
            transition.AddTrigger(new Trigger_PawnLostViolently());
            transition.AddPreAction(new TransitionAction_Message("ElectionFail_Disaster".Translate(), MessageTypeDefOf.NegativeEvent, new TargetInfo(this.spot, base.Map, false)));
            stateGraph.AddTransition(transition);
            this.timeoutTrigger = new Trigger_TicksPassed(Rand.RangeInclusive(5000, 8000));
            Transition transition2 = new Transition(lordToil_Party, lordToil_End);
            transition2.AddTrigger(this.timeoutTrigger);

            transition2.AddPreAction(new TransitionAction_Message("ElectionFinished".Translate(), MessageTypeDefOf.PositiveEvent, new TargetInfo(this.spot, base.Map, false)));
            transition2.AddPreAction(new TransitionAction_Custom(new Action(delegate
            {
                this.Finished();
            })));
            stateGraph.AddTransition(transition2);
            return stateGraph;
        }

        private bool ShouldBeCalledOff()
        {
            return !GatheringsUtility.AcceptableGameConditionsToContinueGathering(base.Map) || (!this.spot.Roofed(base.Map) && !JoyUtility.EnjoyableOutsideNow(base.Map, null));
        }

        private void Finished()
        {
            List<Pawn> ownedPawns = this.lord.ownedPawns;
            int num = 0;
            for (int i = 0; i < ownedPawns.Count; i++)
            {
                
                if (GatheringsUtility.InGatheringArea(ownedPawns[i].Position, this.spot, base.Map))
                {
                    ownedPawns[i].needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("AttendedElection"), null);
                    num++;
                }
            }
            if (num != 0)
            {
           
                IncidentParms parms = StorytellerUtility.DefaultParmsNow(IncidentDef.Named("SetLeadership").category, this.Map);
                IncidentDef.Named("SetLeadership").Worker.TryExecute(parms);
            }
            else
            {
                Messages.Message("ElectionNoAttendees".Translate(), MessageTypeDefOf.RejectInput);
            }
        }

        public override float VoluntaryJoinPriorityFor(Pawn p)
        {
            if (!this.IsInvited(p))
            {
                return 0f;
            }
            if (!GatheringsUtility.ShouldGuestKeepAttendingGathering(p))
            {
                return 0f;
            }
            if (!this.lord.ownedPawns.Contains(p) && this.IsPartyAboutToEnd())
            {
                return 0f;
            }
            return 20f;
        }

        public override void ExposeData()
        {
            Scribe_Values.Look<IntVec3>(ref this.spot, "spot", default(IntVec3), false);
        }

        public override string GetReport(Pawn pawn)
        {
            return "AttendingElectionDesc".Translate();
        }

        private bool IsPartyAboutToEnd()
        {
            return this.timeoutTrigger.TicksLeft < 1200;
        }

        private bool IsInvited(Pawn p)
        {
            return p.Faction == this.lord.faction;
        }
    }
}
