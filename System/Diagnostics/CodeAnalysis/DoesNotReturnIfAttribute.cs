// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.CodeAnalysis.DoesNotReturnIfAttribute
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

namespace System.Diagnostics.CodeAnalysis
{
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [ExcludeFromCodeCoverage]
  [DebuggerNonUserCode]
  internal sealed class DoesNotReturnIfAttribute : Attribute
  {
    public bool ParameterValue { get; }

    public DoesNotReturnIfAttribute(bool parameterValue) => this.ParameterValue = parameterValue;
  }
}
