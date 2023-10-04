// Decompiled with JetBrains decompiler
// Type: HarmonyLib.BUTR.Extensions.SymbolExtensions2
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using System;
using System.Linq.Expressions;
using System.Reflection;


#nullable enable
namespace HarmonyLib.BUTR.Extensions
{
  internal static class SymbolExtensions2
  {
    public static ConstructorInfo? GetConstructorInfo<TResult>(
      Expression<Func<TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, TResult>(
      Expression<Func<T1, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, TResult>(
      Expression<Func<T1, T2, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, TResult>(
      Expression<Func<T1, T2, T3, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, TResult>(
      Expression<Func<T1, T2, T3, T4, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, T5, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, T5, T6, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
      Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetConstructorInfo(expression1) : (ConstructorInfo) null;
    }

    public static ConstructorInfo? GetConstructorInfo(LambdaExpression expression) => expression?.Body is NewExpression body && (object) body.Constructor != null ? body.Constructor : (ConstructorInfo) null;

    public static FieldInfo? GetFieldInfo<T>(Expression<Func<T>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetFieldInfo(expression1) : (FieldInfo) null;
    }

    public static FieldInfo? GetFieldInfo<T, TResult>(
      Expression<Func<T, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetFieldInfo(expression1) : (FieldInfo) null;
    }

    public static FieldInfo? GetFieldInfo(LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        FieldInfo member = body.Member as FieldInfo;
        if ((object) member != null)
          return member;
      }
      return (FieldInfo) null;
    }

    public static AccessTools.FieldRef<object, TField>? GetFieldRefAccess<TField>(
      Expression<Func<TField>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetFieldRefAccess<TField>(expression1) : (AccessTools.FieldRef<object, TField>) null;
    }

    public static AccessTools.FieldRef<object, TField>? GetFieldRefAccess<TField>(
      LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        FieldInfo member = body.Member as FieldInfo;
        if ((object) member != null)
          return !(member == (FieldInfo) null) ? AccessTools2.FieldRefAccess<object, TField>(member) : (AccessTools.FieldRef<object, TField>) null;
      }
      return (AccessTools.FieldRef<object, TField>) null;
    }

    public static AccessTools.FieldRef<TObject, TField>? GetFieldRefAccess<TObject, TField>(
      Expression<Func<TObject, TField>> expression)
      where TObject : class
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetFieldRefAccess<TObject, TField>(expression1) : (AccessTools.FieldRef<TObject, TField>) null;
    }

    public static AccessTools.FieldRef<TObject, TField>? GetFieldRefAccess<TObject, TField>(
      LambdaExpression expression)
      where TObject : class
    {
      if (expression?.Body is MemberExpression body)
      {
        FieldInfo member = body.Member as FieldInfo;
        if ((object) member != null)
          return !(member == (FieldInfo) null) ? AccessTools2.FieldRefAccess<TObject, TField>(member) : (AccessTools.FieldRef<TObject, TField>) null;
      }
      return (AccessTools.FieldRef<TObject, TField>) null;
    }

    public static MethodInfo? GetMethodInfo(Expression<Action> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1>(Expression<Action<T1>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2>(Expression<Action<T1, T2>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3>(
      Expression<Action<T1, T2, T3>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4>(
      Expression<Action<T1, T2, T3, T4>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5>(
      Expression<Action<T1, T2, T3, T4, T5>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6>(
      Expression<Action<T1, T2, T3, T4, T5, T6>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7>(
      Expression<Action<T1, T2, T3, T4, T5, T6, T7>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8>(
      Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
      Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<TResult>(Expression<Func<TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, TResult>(
      Expression<Func<T1, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, TResult>(
      Expression<Func<T1, T2, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, TResult>(
      Expression<Func<T1, T2, T3, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, TResult>(
      Expression<Func<T1, T2, T3, T4, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
      Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
      Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetMethodInfo(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetMethodInfo(LambdaExpression expression)
    {
      if (expression?.Body is MethodCallExpression body)
      {
        MethodInfo method = body.Method;
        if ((object) method != null)
          return method;
      }
      return (MethodInfo) null;
    }

    public static PropertyInfo? GetPropertyInfo<T>(Expression<Func<T>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertyInfo(expression1) : (PropertyInfo) null;
    }

    public static PropertyInfo? GetPropertyInfo<T, TResult>(
      Expression<Func<T, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertyInfo(expression1) : (PropertyInfo) null;
    }

    public static PropertyInfo? GetPropertyInfo(LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        PropertyInfo member = body.Member as PropertyInfo;
        if ((object) member != null)
          return member;
      }
      return (PropertyInfo) null;
    }

    public static MethodInfo? GetPropertyGetter<T>(Expression<Func<T>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertyGetter(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetPropertyGetter<T, TResult>(
      Expression<Func<T, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertyGetter(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetPropertyGetter(LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        PropertyInfo member = body.Member as PropertyInfo;
        if ((object) member != null)
          return member?.GetGetMethod(true);
      }
      return (MethodInfo) null;
    }

    public static MethodInfo? GetPropertySetter<T>(Expression<Func<T>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertySetter(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetPropertySetter<T, TResult>(
      Expression<Func<T, TResult>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetPropertySetter(expression1) : (MethodInfo) null;
    }

    public static MethodInfo? GetPropertySetter(LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        PropertyInfo member = body.Member as PropertyInfo;
        if ((object) member != null)
          return member?.GetSetMethod(true);
      }
      return (MethodInfo) null;
    }

    public static AccessTools.FieldRef<TField>? GetStaticFieldRefAccess<TField>(
      Expression<Func<TField>> expression)
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetStaticFieldRefAccess<TField>(expression1) : (AccessTools.FieldRef<TField>) null;
    }

    public static AccessTools.FieldRef<TField>? GetStaticFieldRefAccess<TField>(
      LambdaExpression expression)
    {
      if (expression?.Body is MemberExpression body)
      {
        FieldInfo member = body.Member as FieldInfo;
        if ((object) member != null)
          return !(member == (FieldInfo) null) ? AccessTools2.StaticFieldRefAccess<TField>(member) : (AccessTools.FieldRef<TField>) null;
      }
      return (AccessTools.FieldRef<TField>) null;
    }

    public static AccessTools.StructFieldRef<TObject, TField>? GetStructFieldRefAccess<TObject, TField>(
      Expression<Func<TField>> expression)
      where TObject : struct
    {
      LambdaExpression expression1 = (LambdaExpression) expression;
      return expression1 != null ? SymbolExtensions2.GetStructFieldRefAccess<TObject, TField>(expression1) : (AccessTools.StructFieldRef<TObject, TField>) null;
    }

    public static AccessTools.StructFieldRef<TObject, TField>? GetStructFieldRefAccess<TObject, TField>(
      LambdaExpression expression)
      where TObject : struct
    {
      if (expression?.Body is MemberExpression body)
      {
        FieldInfo member = body.Member as FieldInfo;
        if ((object) member != null)
          return !(member == (FieldInfo) null) ? AccessTools2.StructFieldRefAccess<TObject, TField>(member) : (AccessTools.StructFieldRef<TObject, TField>) null;
      }
      return (AccessTools.StructFieldRef<TObject, TField>) null;
    }
  }
}
