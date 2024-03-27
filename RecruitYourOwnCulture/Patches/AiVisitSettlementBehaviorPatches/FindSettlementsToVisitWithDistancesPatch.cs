// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Patches.AiVisitSettlementBehaviorPatches.FindSettlementsToVisitWithDistancesPatch
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors.AiBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;


#nullable enable
namespace RecruitYourOwnCulture.Patches.AiVisitSettlementBehaviorPatches
{
    [HarmonyPatch(typeof(AiVisitSettlementBehavior), "FindSettlementsToVisitWithDistances")]
    internal class FindSettlementsToVisitWithDistancesPatch
    {
        private const float MaximumFilteredDistance = 700f;

        public static bool Prefix(
          ref SortedList<(float, int), Settlement> __result,
          MobileParty mobileParty)
        {
            SortedList<(float, int), Settlement> sortedList = new SortedList<(float, int), Settlement>();
            MapDistanceModel mapDistanceModel = Campaign.Current.Models.MapDistanceModel;
            if (mobileParty.LeaderHero != null && mobileParty.LeaderHero.MapFaction.IsKingdomFaction)
            {
                if (mobileParty.Army == null || mobileParty.Army.LeaderParty == mobileParty)
                {
                    foreach (Settlement settlement in FindSettlementsNear(mobileParty, 60f))
                    {
                        float distance = mapDistanceModel.GetDistance(mobileParty, settlement);
                        if ((double)distance < 700.0)
                            sortedList.Add((distance, ((object)settlement).GetHashCode()), settlement);
                    }
                }
                foreach (Settlement settlement in mobileParty.MapFaction.Settlements)
                {
                    if (FindSettlementsToVisitWithDistancesPatch.IsSettlementSuitableForVisitingCondition(mobileParty, settlement))
                    {
                        float distance = mapDistanceModel.GetDistance(mobileParty, settlement);
                        if ((double)distance < 700.0 && (double)distance > 1250.0)
                            sortedList.Add((distance, ((object)settlement).GetHashCode()), settlement);
                    }
                }
                __result = sortedList;
                return false;
            }
            foreach (Settlement settlement in FindSettlementsNear(mobileParty, 100f))
            {
                float distance = mapDistanceModel.GetDistance(mobileParty, settlement);
                if ((double)distance < 700.0 && !sortedList.ContainsKey((distance, ((object)settlement).GetHashCode())))
                    sortedList.Add((distance, ((object)settlement).GetHashCode()), settlement);
            }
            __result = sortedList;
            return false;
        }

        private static List<Settlement> FindSettlementsNear(MobileParty party, float radius)
        {
            return Settlement.FindAll(r =>
                        r.Position2D.Distance(party.Position2D) <= radius
                        && !r.IsCastle
                        && IsSettlementSuitableForVisitingCondition(party, r)).ToList();
        } 

        private static bool IsSettlementSuitableForVisitingCondition(
          MobileParty mobileParty,
          Settlement settlement)
        {
            if (settlement == null || mobileParty == null || mobileParty.Party == null || mobileParty.Party.Owner == null || settlement.Party.MapEvent != null || settlement.Party.SiegeEvent != null || mobileParty.Party.Owner.MapFaction.IsAtWarWith(settlement.MapFaction) && (!mobileParty.Party.Owner.MapFaction.IsMinorFaction || !settlement.IsVillage) || !settlement.IsVillage && !settlement.IsFortification)
                return false;
            if (!settlement.IsVillage)
                return true;
            bool flag1 = settlement.Village.VillageState == 0;
            if (flag1)
            {
                bool isKingdomFaction = mobileParty.Party.Owner.MapFaction.IsKingdomFaction;
                bool flag2 = mobileParty.Party.Owner.MapFaction.Culture != settlement.Culture;
                bool flag3 = mobileParty.Party.Owner.Clan.Culture != settlement.Culture;
                bool flag4 = mobileParty.Party.Owner.Culture == settlement.Culture;
                if (isKingdomFaction)
                {
                    if (flag4)
                        return true;
                    if (flag2)
                        return false;
                    if (mobileParty.Party.Owner.MapFaction == settlement.Owner.MapFaction)
                        return true;
                    if (flag3)
                        return false;
                }
            }
            return flag1;
        }
    }
}
