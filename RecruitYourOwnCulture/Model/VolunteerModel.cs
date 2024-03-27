using AdonnaysTroopChanger.Settings;
using AdonnaysTroopChanger.XMLReader;
using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace RecruitYourOwnCulture.Model
{
    internal class VolunteerModel : DefaultVolunteerModel
    {
        public int MaxVolunteerLimit => GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimit;

        public override CharacterObject GetBasicVolunteer(Hero sellerHero)
        {
            try
            {
                CharacterObject basicVolunteer = ATCConfig.GetFactionRecruit(sellerHero);
                while (((BasicCharacterObject)basicVolunteer).Level < ATCSettings.LevelRecruitsUpToTier * 5 + 1 && basicVolunteer.UpgradeTargets.Length != 0)
                    basicVolunteer = basicVolunteer.UpgradeTargets[MBRandom.RandomInt(basicVolunteer.UpgradeTargets.Length)];
                return basicVolunteer;
            } catch(Exception)
            {
                return base.GetBasicVolunteer(sellerHero);
            }
        }

        public override int MaximumIndexHeroCanRecruitFromHero(
          Hero buyerHero,
          Hero sellerHero,
          int useValueAsRelation = -101)
        {
            RecruitYourOwnCultureSettings instance = GlobalSettings<RecruitYourOwnCultureSettings>.Instance;
            ExplainedNumber recruitmentIndex = this.CalculateMaximumRecruitmentIndex(buyerHero, sellerHero, useValueAsRelation);
            int num = (int)Math.Floor((double)recruitmentIndex.ResultNumber);
            if (instance.RecruitOnlyOwnCulture)
            {
                bool flag1 = buyerHero.Culture == sellerHero.Culture;
                bool flag2 = buyerHero.Clan != null;
                bool flag3 = buyerHero == Hero.MainHero;
                bool cultureEnabledToPlayer = instance.RecruitOnlySameCultureEnabledToPlayer;
                bool cultureEnabledToAiLord = instance.RecruitOnlySameCultureEnabledToAiLord;
                bool keepKingdomCulture = instance.RecruitOnlySameCultureKeepKingdomCulture;
                bool cultureKeepClanCulture = instance.RecruitOnlySameCultureKeepClanCulture;
                return !buyerHero.MapFaction.IsKingdomFaction && buyerHero.Clan.IsMinorFaction && !flag3 || flag1 && (cultureEnabledToPlayer && flag3 || cultureEnabledToAiLord && !flag3) || cultureKeepClanCulture && flag2 && buyerHero.Clan.Culture == sellerHero.Culture && (cultureEnabledToPlayer && flag3 || cultureEnabledToAiLord && !flag3) || keepKingdomCulture && flag2 && buyerHero.Clan.Kingdom != null && buyerHero.Clan.Kingdom.Culture == sellerHero.Culture && (cultureEnabledToPlayer && flag3 || cultureEnabledToAiLord && !flag3) || !cultureEnabledToAiLord && !flag3 || !cultureEnabledToPlayer && flag3 ? num : -100;
            }
            return !instance.VolunteerLimitEnable ? base.MaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation) : num;
        }

        public ExplainedNumber CalculateMaximumRecruitmentIndex(
          Hero buyerHero,
          Hero sellerHero,
          int useValueAsRelation = -101,
          bool explanations = false)
        {
            ExplainedNumber result;
            // ISSUE: explicit constructor call
            result = new ExplainedNumber(0.0f, explanations, (TextObject)null);
            result.LimitMin(0.0f);
            result.LimitMax((float)sellerHero.VolunteerTypes.Length);
            if (buyerHero != null)
                useValueAsRelation = sellerHero.GetRelation(buyerHero);
            result.Add((float)this.GetRelationFactor(useValueAsRelation), GameTexts.FindText("str_notable_relations", (string)null), (TextObject)null);
            if (sellerHero.CurrentSettlement != null)
            {
                int num = base.MaximumIndexHeroCanRecruitFromHero(buyerHero, sellerHero, useValueAsRelation);
                result.Add((float)((double)num * ((double)this.MaxVolunteerLimit / 6.0) * 0.5), (TextObject)null, (TextObject)null);
                this.defaultPerks(ref result, buyerHero, sellerHero, useValueAsRelation);
            }
            return result;
        }

        private int GetRelationFactor(int relation) => (int)((double)this.MaxVolunteerLimit * ((double)((float)relation / 50f) * 0.15000000596046448));

        private void defaultPerks(
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
    }
}
