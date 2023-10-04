// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCultureBackup.Patches.DefaultVolunteerModelPatch
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;


namespace RecruitYourOwnCultureBackup.Patches
{
    internal class DefaultVolunteerModelPatch
    {
        public static int MaxVolunteerLimit => GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimit;

        public static ExplainedNumber CalculateMaximumRecruitmentIndex(
          Hero buyerHero,
          Hero sellerHero,
          int useValueAsRelation = -101,
          bool explanations = false)
        {
            ExplainedNumber result;
            // ISSUE: explicit constructor call
            result = new ExplainedNumber(0f, explanations, null);
            result.LimitMin(0f);
            result.LimitMax(sellerHero.VolunteerTypes.Length);
            if (buyerHero != null)
                useValueAsRelation = sellerHero.GetRelation(buyerHero);
            result.Add((float)DefaultVolunteerModelPatch.GetRelationFactor(useValueAsRelation), GameTexts.FindText("str_notable_relations", (string)null), (TextObject)null);
            if (sellerHero.CurrentSettlement != null)
            {
                int num = DefaultVolunteerModelPatch.BaseMaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation);
                result.Add((float)((double)num * ((double)DefaultVolunteerModelPatch.MaxVolunteerLimit / 6.0) * 0.5), (TextObject)null, (TextObject)null);
                DefaultVolunteerModelPatch.defaultPerks(ref result, buyerHero, sellerHero, useValueAsRelation);
            }
            return result;
        }

        private static int GetRelationFactor(int relation) => (int)((double)DefaultVolunteerModelPatch.MaxVolunteerLimit * ((double)((float)relation / 50f) * 0.15000000596046448));

        private static void defaultPerks(
          ref ExplainedNumber result,
          Hero buyerHero,
          Hero sellerHero,
          int useValueAsRelation = -101)
        {
            Settlement currentSettlement = sellerHero.CurrentSettlement;
            if (sellerHero.IsGangLeader && currentSettlement != null && currentSettlement.OwnerClan == buyerHero.Clan)
            {
                if (currentSettlement.IsTown)
                {
                    Hero governor = currentSettlement.Town.Governor;
                    if (governor != null && governor.GetPerkValue(DefaultPerks.Roguery.OneOfTheFamily))
                        result.Add(DefaultPerks.Roguery.OneOfTheFamily.SecondaryBonus, ((PropertyObject)DefaultPerks.Roguery.OneOfTheFamily).Name, (TextObject)null);
                }
                if (currentSettlement.IsVillage)
                {
                    Hero governor = currentSettlement.Village.Bound.Town.Governor;
                    int num = governor == null ? (true ? 1 : 0) : (!governor.GetPerkValue(DefaultPerks.Roguery.OneOfTheFamily) ? 1 : 0);
                }
            }
            if (sellerHero.IsMerchant && buyerHero.GetPerkValue(DefaultPerks.Trade.ArtisanCommunity))
                result.Add(DefaultPerks.Trade.ArtisanCommunity.SecondaryBonus, ((PropertyObject)DefaultPerks.Trade.ArtisanCommunity).Name, (TextObject)null);
            if (sellerHero.Culture == buyerHero.Culture && buyerHero.GetPerkValue(DefaultPerks.Leadership.CombatTips))
                result.Add(DefaultPerks.Leadership.CombatTips.SecondaryBonus, ((PropertyObject)DefaultPerks.Leadership.CombatTips).Name, (TextObject)null);
            if (sellerHero.IsRuralNotable && buyerHero.GetPerkValue(DefaultPerks.Charm.Firebrand))
                result.Add(DefaultPerks.Charm.Firebrand.SecondaryBonus, ((PropertyObject)DefaultPerks.Charm.Firebrand).Name, (TextObject)null);
            if (sellerHero.IsUrbanNotable && buyerHero.GetPerkValue(DefaultPerks.Charm.FlexibleEthics))
                result.Add(DefaultPerks.Charm.FlexibleEthics.SecondaryBonus, ((PropertyObject)DefaultPerks.Charm.FlexibleEthics).Name, (TextObject)null);
            if (!sellerHero.IsArtisan || buyerHero.PartyBelongedTo == null || buyerHero.PartyBelongedTo.EffectiveEngineer == null || !buyerHero.PartyBelongedTo.EffectiveEngineer.GetPerkValue(DefaultPerks.Engineering.EngineeringGuilds))
                return;
            result.Add(DefaultPerks.Engineering.EngineeringGuilds.PrimaryBonus, ((PropertyObject)DefaultPerks.Engineering.EngineeringGuilds).Name, (TextObject)null);
        }

        private static int BaseMaximumIndexHeroCanRecruitFromHero(
          Hero buyerHero,
          Hero sellerHero,
          int useValueAsRelation = -101)
        {
            Settlement currentSettlement = sellerHero.CurrentSettlement;
            int num1 = 1;
            int num2 = buyerHero == Hero.MainHero ? Campaign.Current.Models.DifficultyModel.GetPlayerRecruitSlotBonus() : 0;
            int num3 = buyerHero != Hero.MainHero ? 1 : 0;
            int num4 = currentSettlement == null || buyerHero.MapFaction != currentSettlement.MapFaction ? 0 : 1;
            int num5 = currentSettlement == null || !buyerHero.MapFaction.IsAtWarWith(currentSettlement.MapFaction) ? 0 : -(1 + num3);
            if (buyerHero.IsMinorFactionHero && currentSettlement != null && currentSettlement.IsVillage)
                num5 = 0;
            int num6 = useValueAsRelation < -100 ? buyerHero.GetRelation(sellerHero) : useValueAsRelation;
            int num7 = num6 >= 100 ? 7 : (num6 >= 80 ? 6 : (num6 >= 60 ? 5 : (num6 >= 40 ? 4 : (num6 >= 20 ? 3 : (num6 >= 10 ? 2 : (num6 >= 5 ? 1 : (num6 < 0 ? -1 : 0)))))));
            int num8 = 0;
            return MathF.Min(6, MathF.Max(0, num1 + num3 + num7 + num2 + num4 + num5 + num8));
        }
    }
}
