using HarmonyLib;
using Helpers;
using MCM.Abstractions.Base.Global;
using RecruitYourOwnCulture.Settings;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;

namespace RecruitYourOwnCulture.Patches
{
    [HarmonyPatch(typeof(HeroHelper), "GetVolunteerTroopsOfHeroForRecruitment")]
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
