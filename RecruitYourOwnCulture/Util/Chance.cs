// Decompiled with JetBrains decompiler
// Type: RecruitYourOwnCulture.Util.Chance
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using System;
using System.Security.Cryptography;
using TaleWorlds.Library;


#nullable enable
namespace RecruitYourOwnCulture.Util
{
  internal class Chance
  {
    public static RandomNumberGenerator Generator = RandomNumberGenerator.Create();
    private static readonly uint Salt = 42949672;

    internal static bool getChance(float chance)
    {
      chance = MathF.Clamp(chance, 0.0f, 100f);
      int num = MathF.Round(chance * (float) Chance.Salt);
      byte[] data = new byte[4];
      Chance.Generator.GetBytes(data);
      return (ulong) BitConverter.ToUInt32(data, 0) < (ulong) num;
    }
  }
}
