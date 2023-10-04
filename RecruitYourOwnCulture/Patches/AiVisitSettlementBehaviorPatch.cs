// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Patches.AiVisitSettlementBehaviorPatch
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors.AiBehaviors;
using TaleWorlds.CampaignSystem.Settlements;


#nullable enable
namespace RecruitYourOwnCulture.Patches
{
  [HarmonyPatch(typeof (AiVisitSettlementBehavior))]
  internal class AiVisitSettlementBehaviorPatch
  {
    [HarmonyPrefix]
    [HarmonyPatch]
    public static bool UpdateApproximateNumberOfVolunteersCanBeRecruitedFromSettlementPrefix(
      ref int __result,
      Hero hero,
      Settlement settlement)
    {
      bool isKingdomFaction = hero.MapFaction.IsKingdomFaction;
      bool flag1 = hero.MapFaction.Culture == settlement.Culture;
      bool flag2 = hero.Clan.Culture != settlement.Culture && !hero.MapFaction.IsMinorFaction;
      bool flag3;
      if (isKingdomFaction && !flag1)
      {
        __result = 0;
        flag3 = false;
      }
      else if (flag2)
      {
        __result = 0;
        flag3 = false;
      }
      else
      {
        int num1 = 4;
        if (hero.MapFaction != settlement.MapFaction)
          num1 = 2;
        int num2 = 0;
        foreach (Hero notable in settlement.Notables)
        {
          for (int index = 0; index < num1; ++index)
          {
            if (notable.VolunteerTypes[index] != null)
              ++num2;
          }
        }
        __result = num2;
        flag3 = false;
      }
      return false;
    }
  }
}
