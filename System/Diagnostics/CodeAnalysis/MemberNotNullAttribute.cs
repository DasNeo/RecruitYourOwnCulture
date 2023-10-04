// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.CodeAnalysis.MemberNotNullAttribute
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll


#nullable enable
namespace System.Diagnostics.CodeAnalysis
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
  [ExcludeFromCodeCoverage]
  [DebuggerNonUserCode]
  internal sealed class MemberNotNullAttribute : Attribute
  {
    public string[] Members { get; }

    public MemberNotNullAttribute(string member) => this.Members = new string[1]
    {
      member
    };

    public MemberNotNullAttribute(params string[] members) => this.Members = members;
  }
}
