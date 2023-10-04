// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Behaviors.GarissonBehavior
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using RecruitYourOwnCulture.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;


#nullable enable
namespace RecruitYourOwnCulture.Behaviors
{
    internal class GarissonBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents() => CampaignEvents.DailyTickEvent.AddNonSerializedListener((object)this, new Action(this.OnDailyTick));

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void OnDailyTick()
        {
            if (!GlobalSettings<RecruitYourOwnCultureSettings>.Instance.EnableGarrisonConversion)
                return;
            this.dailyChanceConversation();
        }

        private void dailyChanceConversation()
        {
            RecruitYourOwnCultureSettings instance = GlobalSettings<RecruitYourOwnCultureSettings>.Instance;
            if (instance.EnableGarrisonConversionToPlayer)
            {
                List<Settlement> list = ((IEnumerable<Settlement>)Settlement.All).Where<Settlement>((Func<Settlement, bool>)(c => !c.IsVillage && !c.IsUnderSiege && c.IsTown && c.Owner != null && c.Owner == Hero.MainHero)).ToList<Settlement>();
                if (list.Count > 0)
                    this.conversionLogic(list, instance);
            }
            if (!instance.EnableGarrisonConversionToAi)
                return;
            this.conversionLogic(((IEnumerable<Settlement>)Settlement.All).Where<Settlement>((Func<Settlement, bool>)(c => !c.IsVillage && !c.IsUnderSiege && c.IsTown && c.Owner != null && c.Owner != Hero.MainHero)).ToList<Settlement>(), instance);
        }

        private void conversionLogic(
          List<Settlement> settlements,
          RecruitYourOwnCultureSettings settings)
        {
            try
            {
                foreach (Settlement settlement in settlements)
                {
                    int num = 0;
                    if (((Fief)settlement.Town).GarrisonParty != null)
                    {
                        foreach (MobileParty party in ((IEnumerable<MobileParty>)settlement.Parties).Where<MobileParty>((Func<MobileParty, bool>)(party => party.IsGarrison)))
                        {
                            foreach (FlattenedTroopRosterElement troop in party.MemberRoster.ToFlattenedRoster())
                            {
                                CultureObject culture = settlement.Culture;
                                if (troop.Troop.Culture != culture && Chance.getChance((float)((double)settlement.Town.Loyalty / 2.0 * 0.30000001192092896)))
                                {
                                    bool flag = GarissonBehavior.IsTroopElite(troop.Troop);
                                    CharacterObject level = TroopUtil.tryToLevel(flag ? settlement.Culture.EliteBasicTroop : settlement.Culture.BasicTroop, troop.Troop.Tier);
                                    if (flag)
                                    {
                                        if (this.DoConversion(party, troop, level, settings))
                                            ++num;
                                    }
                                    else if (this.DoConversion(party, troop, level, settings))
                                        ++num;
                                }
                            }
                        }
                    }
                    if (settings.EnableGarrisonConversionDisplayMessageInPlayerSettlement && settlement.Owner != null && settlement.Owner == Hero.MainHero)
                        InformationManager.DisplayMessage(new InformationMessage(string.Format("{1} Converted Troops in settlement of {0}", (object)settlement.Name, (object)num)));
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static bool IsTroopElite(CharacterObject unit)
        {
            List<CharacterObject> characterObjectList = new List<CharacterObject>();
            Stack<CharacterObject> characterObjectStack = new Stack<CharacterObject>();
            characterObjectStack.Push(unit.Culture.EliteBasicTroop);
            characterObjectList.Add(unit.Culture.EliteBasicTroop);

            while (!TaleWorlds.Core.Extensions.IsEmpty<CharacterObject>((IEnumerable<CharacterObject>)characterObjectStack))
            {
                CharacterObject characterObject = characterObjectStack.Pop();
                if (characterObject.UpgradeTargets != null && characterObject.UpgradeTargets.Length != 0)
                {
                    for (int index = 0; index < characterObject.UpgradeTargets.Length; ++index)
                    {
                        if (!characterObjectList.Contains(characterObject.UpgradeTargets[index]))
                        {
                            characterObjectList.Add(characterObject.UpgradeTargets[index]);
                            characterObjectStack.Push(characterObject.UpgradeTargets[index]);
                        }
                    }
                }
            }
            return characterObjectList.Contains(unit);
        }

        private bool DoConversion(
          MobileParty party,
          FlattenedTroopRosterElement troop,
          CharacterObject replacement,
          RecruitYourOwnCultureSettings settings)
        {
            if (settings.EnableGarrisonConversionIncludeBandit)
            {
                party.MemberRoster.RemoveTroop(troop.Troop, 1, new UniqueTroopDescriptor(), 0);
                party.AddElementToMemberRoster(replacement, 1, false);
                return true;
            }
            if (settings.EnableGarrisonConversionIncludeBandit || troop.Troop.Occupation == Occupation.Bandit)
                return false;
            party.MemberRoster.RemoveTroop(troop.Troop, 1, new UniqueTroopDescriptor(), 0);
            party.AddElementToMemberRoster(replacement, 1, false);
            return true;
        }
    }
}
