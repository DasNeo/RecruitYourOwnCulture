// Decompiled with JetBrains decompiler
// Type: HarmonyLib.BUTR.Extensions.Traverse2`1
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using System;


#nullable enable
namespace HarmonyLib.BUTR.Extensions
{
  internal class Traverse2<T>
  {
    private readonly Traverse2 _traverse;

    public T? Value
    {
      get => this._traverse.GetValue<T>();
      set => this._traverse.SetValue((object) value);
    }

    private Traverse2() => this._traverse = new Traverse2((Type) null);

    public Traverse2(Traverse2 traverse) => this._traverse = traverse;
  }
}
