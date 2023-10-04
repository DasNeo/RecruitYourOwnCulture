// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Patches.RecruitmentCampaignBehaviorPatch
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using HarmonyLib;
using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using RecruitYourOwnCulture.Util;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;


#nullable enable
namespace RecruitYourOwnCulture.Patches
{
    [HarmonyPatch(typeof(RecruitmentCampaignBehavior))]
    internal class RecruitmentCampaignBehaviorPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch]
        private static bool UpdateVolunteersOfNotablesInSettlementPrefix(Settlement settlement)
        {
            bool flag1 = (!settlement.IsTown || settlement.Town.InRebelliousState) && (!settlement.IsVillage || settlement.Village.Bound.Town.InRebelliousState) && !settlement.IsCastle;
            if (!GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimitEnable)
                return true;
            if (!flag1)
            {
                foreach (Hero notable in settlement.Notables)
                {
                    if (notable.CanHaveRecruits)
                    {
                        bool flag2 = false;
                        CharacterObject basicVolunteer = Campaign.Current.Models.VolunteerModel.GetBasicVolunteer(notable);
                        for (int index = 0; index < notable.VolunteerTypes.Length; ++index)
                        {
                            if ((double)MBRandom.RandomFloat < (double)Campaign.Current.Models.VolunteerModel.GetDailyVolunteerProductionProbability(notable, index, settlement))
                            {
                                CharacterObject volunteerType = notable.VolunteerTypes[index];
                                if (volunteerType == null)
                                {
                                    notable.VolunteerTypes[index] = basicVolunteer;
                                    flag2 = true;
                                }
                                else if (volunteerType.UpgradeTargets.Length != 0 && volunteerType.Tier <= 3 && (double)MBRandom.RandomFloat < (double)MathF.Log(notable.Power / (float)volunteerType.Tier, 2f) * 0.0099999997764825821)
                                {
                                    notable.VolunteerTypes[index] = volunteerType.UpgradeTargets[MBRandom.RandomInt(volunteerType.UpgradeTargets.Length)];
                                    flag2 = true;
                                }
                            }
                        }
                        if (flag2)
                        {
                            CharacterObject[] volunteerTypes = notable.VolunteerTypes;
                            for (int index1 = 1; index1 < notable.VolunteerTypes.Length; ++index1)
                            {
                                CharacterObject characterObject1 = volunteerTypes[index1];
                                if (characterObject1 != null)
                                {
                                    int num = 0;
                                    int index2 = index1 - 1;
                                    CharacterObject characterObject2 = volunteerTypes[index2];
                                    while (index2 >= 0 && (characterObject2 == null || (double)((BasicCharacterObject)characterObject1).Level + (((BasicCharacterObject)characterObject1).IsMounted ? 0.5 : 0.0) < (double)((BasicCharacterObject)characterObject2).Level + (((BasicCharacterObject)characterObject2).IsMounted ? 0.5 : 0.0)))
                                    {
                                        if (characterObject2 == null)
                                        {
                                            --index2;
                                            ++num;
                                            if (index2 >= 0)
                                                characterObject2 = volunteerTypes[index2];
                                        }
                                        else
                                        {
                                            volunteerTypes[index2 + 1 + num] = characterObject2;
                                            --index2;
                                            num = 0;
                                            if (index2 >= 0)
                                                characterObject2 = volunteerTypes[index2];
                                        }
                                    }
                                    volunteerTypes[index2 + 1 + num] = characterObject1;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch]
        private static bool UpdateOnTroopRecruitedPrefix(
          Hero recruiter,
          Settlement settlement,
          Hero recruitmentSource,
          CharacterObject troop,
          int count)
        {
            if (recruiter != null && recruiter.Clan != null && recruiter.Clan.IsMinorFaction && !recruiter.MapFaction.IsKingdomFaction)
            {
                Clan clan = recruiter.Clan;
                CultureObject culture = clan.Culture;
                if (recruiter != null && recruiter.PartyBelongedTo != null && troop.Culture != culture)
                {
                    recruiter.PartyBelongedTo.MemberRoster.RemoveTroop(troop, count, new UniqueTroopDescriptor(), 0);
                    troop = TroopUtil.tryToLevel(clan.BasicTroop, troop.Tier);
                    recruiter.PartyBelongedTo.AddElementToMemberRoster(troop, count, false);
                }
                if (recruiter != null && recruiter.PartyBelongedTo != null && recruiter.GetPerkValue(DefaultPerks.Leadership.FamousCommander))
                    recruiter.PartyBelongedTo.MemberRoster.AddXpToTroop((int)DefaultPerks.Leadership.FamousCommander.SecondaryBonus * count, troop);
                SkillLevelingManager.OnTroopRecruited(recruiter, count, troop.Tier);
                if (recruiter != null && recruiter.PartyBelongedTo != null && troop.Occupation == Occupation.Bandit)
                    SkillLevelingManager.OnBanditsRecruited(recruiter.PartyBelongedTo, troop, count);
            }
            else
            {
                if (recruiter != null && recruiter.PartyBelongedTo != null && recruiter.GetPerkValue(DefaultPerks.Leadership.FamousCommander))
                    recruiter.PartyBelongedTo.MemberRoster.AddXpToTroop((int)DefaultPerks.Leadership.FamousCommander.SecondaryBonus * count, troop);
                SkillLevelingManager.OnTroopRecruited(recruiter, count, troop.Tier);
                if (recruiter != null && recruiter.PartyBelongedTo != null && troop.Occupation == Occupation.Bandit)
                    SkillLevelingManager.OnBanditsRecruited(recruiter.PartyBelongedTo, troop, count);
            }
            return false;
        }
    }
}
