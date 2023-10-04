// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Util.TroopUtil
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;


#nullable enable
namespace RecruitYourOwnCulture.Util
{
  internal class TroopUtil
  {
    internal static CharacterObject tryToLevel(CharacterObject root, int tier)
    {
      CharacterObject level = root;
      while (level.Tier < tier && level.UpgradeTargets != null && level.UpgradeTargets.Length != 0)
        level = Extensions.GetRandomElement<CharacterObject>(level.UpgradeTargets);
      return level;
    }
  }
}
