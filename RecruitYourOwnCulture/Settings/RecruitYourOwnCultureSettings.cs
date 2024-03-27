// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Settings.RecruitYourOwnCultureSettings
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using TaleWorlds.Localization;


#nullable enable
namespace RecruitYourOwnCulture.Settings
{
    internal class RecruitYourOwnCultureSettings :
      AttributeGlobalSettings<RecruitYourOwnCultureSettings>
    {
        public override string Id => "RecruitYourOwnCulture";

        public override string DisplayName
        {
            get
            {
                string str = "Recruit Your Own Culture {VERSION}";
                Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
                Dictionary<string, object> dictionary2 = dictionary1;
                string key = "VERSION";
                Version version = typeof(RecruitYourOwnCultureSettings).Assembly.GetName().Version;
                dictionary2.Add(key, (object)((version != (Version)null ? version.ToString(3) : string.Empty) ?? "ERROR"));
                return ((object)new TextObject(str, dictionary1)).ToString();
            }
        }

        public override string FolderName => "RecruitYourOwnCulture";

        public override string FormatType => "json";

        [SettingPropertyBool("Enable Recruit Only Same Culture", HintText = "Enable Recruit Only Same Culture Volunteer", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture", GroupOrder = 1)]
        public bool RecruitOnlyOwnCulture { get; set; } = true;

        [SettingPropertyBool("Apply To Ai", HintText = "Applies This Condition To Ai Party", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture")]
        public bool RecruitOnlySameCultureEnabledToAiLord { get; set; } = true;

        [SettingPropertyBool("Apply To Player", HintText = "Applies This Condition To Player", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture")]
        public bool RecruitOnlySameCultureEnabledToPlayer { get; set; } = true;

        [SettingPropertyBool("Clan Culture", HintText = "Hero Should recruit his/her Clan culture troops also.", Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture")]
        public bool RecruitOnlySameCultureKeepClanCulture { get; set; } = true;

        [SettingPropertyBool("Kingdom Culture", HintText = "Hero Should recruit his/her Kingdom culture troops also.", Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture")]
        public bool RecruitOnlySameCultureKeepKingdomCulture { get; set; } = true;

        [SettingPropertyBool("Recruit Prisoner", HintText = "Enable Condition in recruiting prisoner", IsToggle = true, Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture/Prisoner", GroupOrder = 1)]
        public bool AllowedRecruitPrisonerCulture { get; set; } = true;

        [SettingPropertyBool("Recruit Bandit", HintText = "Allowed Bandit To be recruited in party prison.", Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture/Prisoner")]
        public bool RecruitPrisonerAllowedBandit { get; set; } = true;

        [SettingPropertyBool("Same Culture Only", HintText = "Enable to allowed only with same culture in prisoner to be recruited.", IsToggle = true, Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture/Prisoner/Culture")]
        public bool RecruitPrisonerOnlySameCulture { get; set; } = true;

        [SettingPropertyBool("Add Kingdom Culture", HintText = "By Default Only Hero Culture Allowed to be recuited, You can Enable this to add Kingdom Culture", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture/Prisoner/Culture", GroupOrder = 1)]
        public bool AllowedRecruitPrisonerKingdomCulture { get; set; } = true;

        [SettingPropertyBool("Add Clan Culture", HintText = "By Default Only Hero Culture Allowed to be recuited, You can Enable this to add Clan Culture", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Recruit Only Same Culture/Prisoner/Culture", GroupOrder = 1)]
        public bool AllowedRecruitPrisonerClanCulture { get; set; } = true;

        [SettingPropertyBool("Enable Garrison Troop Conversion", HintText = "Enable Garrison Troop Conversation To Settlement Troop Culture", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Garrison", GroupOrder = 2)]
        public bool EnableGarrisonConversion { get; set; } = true;

        [SettingPropertyBool("Apply To Player Settlement", HintText = "Applies This Condition To Player Settlement", Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Garrison")]
        public bool EnableGarrisonConversionToPlayer { get; set; } = true;

        [SettingPropertyBool("Apply To Ai Settlement", HintText = "Applies This Condition To Ai Settlement", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Garrison")]
        public bool EnableGarrisonConversionToAi { get; set; } = true;

        [SettingPropertyBool("Include Bandit", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Garrison")]
        public bool EnableGarrisonConversionIncludeBandit { get; set; } = true;

        [SettingPropertyBool("Display Message Player Settlements", HintText = "Display Message Total of converted troops in Player settlements", Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Garrison")]
        public bool EnableGarrisonConversionDisplayMessageInPlayerSettlement { get; set; } = true;

        [SettingPropertyBool("Enable The Volunteer limit", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Volunteer Limit", GroupOrder = 3)]
        public bool VolunteerLimitEnable { get; set; } = true;

        [SettingPropertyInteger("Noble Volunteer Limit", 6, 20, "0", HintText = "Noble Volunteers Limit, Requires reloading. Native: 6", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Volunteer Limit")]
        public int VolunteerLimit { get; set; } = 6;
    }
}
