// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Behaviors.NotableBehavior
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;


#nullable enable
namespace RecruitYourOwnCulture.Behaviors
{
    internal class NotableBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.WeeklyTickEvent.AddNonSerializedListener((object)this, new Action(this.OnWeeklyTick));
            CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener((object)this, new Action(this.OnCharacterCreationIsOver));
            CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener((object)this, new Action<CampaignGameStarter>(this.OnGameLoaded));
        }

        public override void SyncData(IDataStore dataStore)
        {
        }

        private void OnGameLoaded(CampaignGameStarter gameStarter) => this.AdjustVolunteersRecruitmentAvailable();

        private void OnCharacterCreationIsOver() => this.AdjustVolunteersRecruitmentAvailable();

        private void OnWeeklyTick() => this.AdjustVolunteersRecruitmentAvailable();

        private void AdjustVolunteersRecruitmentAvailable()
        {
            int limit = GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimit;
            if (!GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimitEnable)
                return;
            foreach (Hero hero in ((IEnumerable<Hero>)Hero.AllAliveHeroes).Where<Hero>((Func<Hero, bool>)(hero => hero.CanHaveRecruits && hero.VolunteerTypes.Length != limit)).ToList<Hero>())
            {
                if (hero.VolunteerTypes.Length != limit)
                {
                    CharacterObject[] characterObjectArray = new CharacterObject[limit];
                    for (int index = 0; index < hero.VolunteerTypes.Length; ++index)
                    {
                        if (index < limit)
                            characterObjectArray[index] = hero.VolunteerTypes[index];
                    }
                    hero.VolunteerTypes = characterObjectArray;
                }
            }
        }
    }
}
