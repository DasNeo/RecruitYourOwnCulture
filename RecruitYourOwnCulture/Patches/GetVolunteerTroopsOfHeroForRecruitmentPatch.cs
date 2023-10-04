// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Patches.GetVolunteerTroopsOfHeroForRecruitmentPatch
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using HarmonyLib;
using Helpers;
using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;


#nullable enable
namespace RecruitYourOwnCulture.Patches
{
  [HarmonyPatch(typeof (HeroHelper))]
  internal class GetVolunteerTroopsOfHeroForRecruitmentPatch
  {
    [HarmonyPrefix]
    [HarmonyPatch]
    private static bool UpdateVolunteerTroopsPrefix(Hero hero, ref List<CharacterObject> __result)
    {
      List<CharacterObject> characterObjectList = new List<CharacterObject>();
      bool volunteerLimitEnable = GlobalSettings<RecruitYourOwnCultureSettings>.Instance.VolunteerLimitEnable;
      int num = 6;
      if (volunteerLimitEnable)
        num = hero.VolunteerTypes.Length;
      for (int index = 0; index < num; ++index)
        characterObjectList.Add(hero.VolunteerTypes[index]);
      __result = characterObjectList;
      return false;
    }
  }
}
