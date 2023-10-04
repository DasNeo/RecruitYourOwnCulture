// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Model.PrisonerRecruitmentCalculationModel
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;


#nullable enable
namespace RecruitYourOwnCulture.Model
{
    internal class PrisonerRecruitmentCalculationModel : DefaultPrisonerRecruitmentCalculationModel
    {
        public virtual bool IsPrisonerRecruitable(
          PartyBase party,
          CharacterObject character,
          out int conformityNeeded)
        {
            RecruitYourOwnCultureSettings instance = GlobalSettings<RecruitYourOwnCultureSettings>.Instance;
            if (!character.IsRegular || character.Tier > Campaign.Current.Models.CharacterStatsModel.MaxCharacterTier || party.LeaderHero == null)
            {
                conformityNeeded = 0;
                return false;
            }
            if (!instance.AllowedRecruitPrisonerCulture)
                return base.IsPrisonerRecruitable(party, character, out conformityNeeded);
            try
            {
                if (instance.RecruitPrisonerAllowedBandit && character.Occupation == Occupation.Bandit)
                {
                    int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                    conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                    return elementXp >= conformityNeeded;
                }
                if (instance.RecruitPrisonerOnlySameCulture)
                {
                    if (party.LeaderHero.Clan != null)
                    {
                        if (instance.AllowedRecruitPrisonerKingdomCulture && party.LeaderHero.Clan.Kingdom != null)
                        {
                            if (party.LeaderHero.Clan.Kingdom.Culture == character.Culture)
                            {
                                int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                                conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                                return elementXp >= conformityNeeded;
                            }
                            if (party.LeaderHero.Culture == character.Culture)
                            {
                                int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                                conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                                return elementXp >= conformityNeeded;
                            }
                        }
                        if (instance.AllowedRecruitPrisonerClanCulture)
                        {
                            if (party.LeaderHero.Clan.Culture == character.Culture)
                            {
                                int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                                conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                                return elementXp >= conformityNeeded;
                            }
                            if (party.LeaderHero.Culture == character.Culture)
                            {
                                int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                                conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                                return elementXp >= conformityNeeded;
                            }
                        }
                        if (party.LeaderHero.Culture == character.Culture)
                        {
                            int elementXp = party.MobileParty.PrisonRoster.GetElementXp(character);
                            conformityNeeded = ((PrisonerRecruitmentCalculationModel)this).GetConformityNeededToRecruitPrisoner(character);
                            return elementXp >= conformityNeeded;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            conformityNeeded = 0;
            return false;
        }
    }
}
