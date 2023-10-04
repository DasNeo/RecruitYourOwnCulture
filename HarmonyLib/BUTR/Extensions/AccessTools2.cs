// Decompiled with JetBrains decompiler
// Type: HarmonyLib.BUTR.Extensions.AccessTools2
// Assembly: RecruitYourOwnCulture, Version=0.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F41422C6-7B7C-4A42-A64D-241751A63AAF
// Assembly location: C:\Users\andre\Downloads\RecruitYourOwnCulture\bin\Win64_Shipping_Client\RecruitYourOwnCulture.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;


#nullable enable
namespace HarmonyLib.BUTR.Extensions
{
  internal static class AccessTools2
  {
    public static ConstructorInfo? DeclaredConstructor(
      Type type,
      Type[]? parameters = null,
      bool searchForStatic = false)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.DeclaredConstructor: 'type' is null");
        return (ConstructorInfo) null;
      }
      if (parameters == null)
        parameters = Type.EmptyTypes;
      BindingFlags bindingAttr = searchForStatic ? AccessTools.allDeclared & ~BindingFlags.Instance : AccessTools.allDeclared & ~BindingFlags.Static;
      return type.GetConstructor(bindingAttr, (Binder) null, parameters, new ParameterModifier[0]);
    }

    public static ConstructorInfo? Constructor(
      Type type,
      Type[]? parameters = null,
      bool searchForStatic = false)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.ConstructorInfo: 'type' is null");
        return (ConstructorInfo) null;
      }
      if (parameters == null)
        parameters = Type.EmptyTypes;
      BindingFlags flags = searchForStatic ? AccessTools.all & ~BindingFlags.Instance : AccessTools.all & ~BindingFlags.Static;
      return AccessTools2.FindIncludingBaseTypes<ConstructorInfo>(type, (Func<Type, ConstructorInfo>) (t => t.GetConstructor(flags, (Binder) null, parameters, new ParameterModifier[0])));
    }

    public static ConstructorInfo? DeclaredConstructor(
      string typeString,
      Type[]? parameters = null,
      bool searchForStatic = false)
    {
      if (string.IsNullOrWhiteSpace(typeString))
      {
        Trace.TraceError("AccessTools2.Constructor: 'typeString' is null or whitespace/empty");
        return (ConstructorInfo) null;
      }
      Type type = AccessTools2.TypeByName(typeString);
      return (object) type == null ? (ConstructorInfo) null : AccessTools2.DeclaredConstructor(type, parameters, searchForStatic);
    }

    public static ConstructorInfo? Constructor(
      string typeString,
      Type[]? parameters = null,
      bool searchForStatic = false)
    {
      if (string.IsNullOrWhiteSpace(typeString))
      {
        Trace.TraceError("AccessTools2.Constructor: 'typeString' is null or whitespace/empty");
        return (ConstructorInfo) null;
      }
      Type type = AccessTools2.TypeByName(typeString);
      return (object) type == null ? (ConstructorInfo) null : AccessTools2.Constructor(type, parameters, searchForStatic);
    }

    public static TDelegate? GetDeclaredConstructorDelegate<TDelegate>(Type type, Type[]? parameters = null) where TDelegate : Delegate
    {
      ConstructorInfo constructorInfo = AccessTools2.DeclaredConstructor(type, parameters);
      return (object) constructorInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(constructorInfo);
    }

    public static TDelegate? GetConstructorDelegate<TDelegate>(Type type, Type[]? parameters = null) where TDelegate : Delegate
    {
      ConstructorInfo constructorInfo = AccessTools2.Constructor(type, parameters);
      return (object) constructorInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(constructorInfo);
    }

    public static TDelegate? GetDeclaredConstructorDelegate<TDelegate>(
      string typeString,
      Type[]? parameters = null)
      where TDelegate : Delegate
    {
      ConstructorInfo constructorInfo = AccessTools2.DeclaredConstructor(typeString, parameters);
      return (object) constructorInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(constructorInfo);
    }

    public static TDelegate? GetConstructorDelegate<TDelegate>(string typeString, Type[]? parameters = null) where TDelegate : Delegate
    {
      ConstructorInfo constructorInfo = AccessTools2.Constructor(typeString, parameters);
      return (object) constructorInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(constructorInfo);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(PropertyInfo propertyInfo) where TDelegate : Delegate
    {
      MethodInfo getMethod = propertyInfo?.GetGetMethod(true);
      return (object) getMethod == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(getMethod);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(PropertyInfo propertyInfo) where TDelegate : Delegate
    {
      MethodInfo setMethod = propertyInfo?.GetSetMethod(true);
      return (object) setMethod == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(setMethod);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(
      object? instance,
      PropertyInfo propertyInfo)
      where TDelegate : Delegate
    {
      MethodInfo getMethod = propertyInfo?.GetGetMethod(true);
      return (object) getMethod == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, getMethod);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(
      object? instance,
      PropertyInfo propertyInfo)
      where TDelegate : Delegate
    {
      MethodInfo setMethod = propertyInfo?.GetSetMethod(true);
      return (object) setMethod == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, setMethod);
    }

    public static TDelegate? GetDeclaredPropertyGetterDelegate<TDelegate>(Type type, string name) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, name);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredPropertySetterDelegate<TDelegate>(Type type, string name) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, name);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(Type type, string name) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertyGetter(type, name);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(Type type, string name) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertySetter(type, name);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredPropertyGetterDelegate<TDelegate>(
      object? instance,
      Type type,
      string method)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(type, method);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDeclaredPropertySetterDelegate<TDelegate>(
      object? instance,
      Type type,
      string method)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(type, method);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(
      object? instance,
      Type type,
      string method)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertyGetter(type, method);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(
      object? instance,
      Type type,
      string method)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertySetter(type, method);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDeclaredPropertyGetterDelegate<TDelegate>(
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredPropertySetterDelegate<TDelegate>(
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(string typeColonPropertyName) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(string typeColonPropertyName) where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredPropertyGetterDelegate<TDelegate>(
      object? instance,
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertyGetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDeclaredPropertySetterDelegate<TDelegate>(
      object? instance,
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredPropertySetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetPropertyGetterDelegate<TDelegate>(
      object? instance,
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertyGetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetPropertySetterDelegate<TDelegate>(
      object? instance,
      string typeColonPropertyName)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.PropertySetter(typeColonPropertyName);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDelegate<TDelegate>(ConstructorInfo constructorInfo) where TDelegate : Delegate
    {
      if ((object) constructorInfo == null)
        return default (TDelegate);
      MethodInfo method = typeof (TDelegate).GetMethod("Invoke");
      if ((object) method == null)
        return default (TDelegate);
      if (!method.ReturnType.IsAssignableFrom(constructorInfo.DeclaringType))
        return default (TDelegate);
      ParameterInfo[] parameters = method.GetParameters();
      ParameterInfo[] constructorParameters = constructorInfo.GetParameters();
      if (parameters.Length - constructorParameters.Length != 0 && !AccessTools2.ParametersAreEqual(parameters, constructorParameters))
        return default (TDelegate);
      Expression.Parameter(typeof (object), "instance");
      List<ParameterExpression> list1 = ((IEnumerable<ParameterInfo>) parameters).Select<ParameterInfo, ParameterExpression>((Func<ParameterInfo, int, ParameterExpression>) ((pi, i) => Expression.Parameter(pi.ParameterType, string.Format("p{0}", (object) i)))).ToList<ParameterExpression>();
      List<Expression> list2 = list1.Select<ParameterExpression, Expression>((Func<ParameterExpression, int, Expression>) ((pe, i) => pe.IsByRef || pe.Type.Equals(constructorParameters[i].ParameterType) ? (Expression) pe : (Expression) Expression.Convert((Expression) pe, constructorParameters[i].ParameterType))).ToList<Expression>();
      UnaryExpression body = Expression.Convert((Expression) Expression.New(constructorInfo, (IEnumerable<Expression>) list2), method.ReturnType);
      try
      {
        return Expression.Lambda<TDelegate>((Expression) body, (IEnumerable<ParameterExpression>) list1).Compile();
      }
      catch (Exception ex)
      {
        Trace.TraceError(string.Format("AccessTools2.GetDelegate<{0}>: Error while compiling lambds expression '{1}'", (object) typeof (TDelegate).FullName, (object) ex));
        return default (TDelegate);
      }
    }

    public static TDelegate? GetDelegate<TDelegate>(object? instance, MethodInfo methodInfo) where TDelegate : Delegate
    {
      if ((object) methodInfo == null)
        return default (TDelegate);
      MethodInfo method = typeof (TDelegate).GetMethod("Invoke");
      if ((object) method == null)
        return default (TDelegate);
      if (!method.ReturnType.IsAssignableFrom(methodInfo.ReturnType))
        return default (TDelegate);
      ParameterInfo[] parameters1 = method.GetParameters();
      ParameterInfo[] methodParameters = methodInfo.GetParameters();
      bool flag1 = parameters1.Length - methodParameters.Length == 0 && AccessTools2.ParametersAreEqual(parameters1, methodParameters);
      bool flag2 = instance != null;
      bool flag3 = parameters1.Length - methodParameters.Length == 1 && (parameters1[0].ParameterType.IsAssignableFrom(methodInfo.DeclaringType) || methodInfo.DeclaringType.IsAssignableFrom(parameters1[0].ParameterType));
      if (!flag2 && !flag3 && !methodInfo.IsStatic)
        return default (TDelegate);
      if (flag2 && methodInfo.IsStatic)
        return default (TDelegate);
      if (flag2 && !methodInfo.IsStatic && !methodInfo.DeclaringType.IsAssignableFrom(instance.GetType()))
        return default (TDelegate);
      if (flag1 & flag3)
        return default (TDelegate);
      if (flag2 && (flag3 || !flag1))
        return default (TDelegate);
      if (flag3 && flag2 | flag1)
        return default (TDelegate);
      if (!flag3 && !flag2 && !flag1)
        return default (TDelegate);
      ParameterExpression instance1 = flag3 ? Expression.Parameter(parameters1[0].ParameterType, nameof (instance)) : (ParameterExpression) null;
      List<ParameterExpression> list1 = ((IEnumerable<ParameterInfo>) parameters1).Skip<ParameterInfo>(flag3 ? 1 : 0).Select<ParameterInfo, ParameterExpression>((Func<ParameterInfo, int, ParameterExpression>) ((pi, i) => Expression.Parameter(pi.ParameterType, string.Format("p{0}", (object) i)))).ToList<ParameterExpression>();
      List<Expression> list2 = list1.Select<ParameterExpression, Expression>((Func<ParameterExpression, int, Expression>) ((pe, i) => pe.IsByRef || pe.Type.Equals(methodParameters[i].ParameterType) ? (Expression) pe : (Expression) Expression.Convert((Expression) pe, methodParameters[i].ParameterType))).ToList<Expression>();
      MethodCallExpression methodCallExpression = flag2 ? (instance.GetType().Equals(methodInfo.DeclaringType) ? Expression.Call((Expression) Expression.Constant(instance), methodInfo, (IEnumerable<Expression>) list2) : Expression.Call((Expression) Expression.Convert((Expression) Expression.Constant(instance), instance.GetType()), methodInfo, (IEnumerable<Expression>) list2)) : (flag1 ? Expression.Call(methodInfo, (IEnumerable<Expression>) list2) : (flag3 ? (instance1.Type.Equals(methodInfo.DeclaringType) ? Expression.Call((Expression) instance1, methodInfo, (IEnumerable<Expression>) list2) : Expression.Call((Expression) Expression.Convert((Expression) instance1, methodInfo.DeclaringType), methodInfo, (IEnumerable<Expression>) list2)) : (MethodCallExpression) null));
      if (methodCallExpression == null)
        return default (TDelegate);
      UnaryExpression unaryExpression = Expression.Convert((Expression) methodCallExpression, method.ReturnType);
      try
      {
        UnaryExpression body = unaryExpression;
        IEnumerable<ParameterExpression> parameters2;
        if (!flag3)
          parameters2 = (IEnumerable<ParameterExpression>) list1;
        else
          parameters2 = new List<ParameterExpression>()
          {
            instance1
          }.Concat<ParameterExpression>((IEnumerable<ParameterExpression>) list1);
        return Expression.Lambda<TDelegate>((Expression) body, parameters2).Compile();
      }
      catch (Exception ex)
      {
        Trace.TraceError(string.Format("AccessTools2.GetDelegate<{0}>: Error while compiling lambds expression '{1}'", (object) typeof (TDelegate).FullName, (object) ex));
        return default (TDelegate);
      }
    }

    public static TDelegate? GetDelegate<TDelegate>(MethodInfo methodInfo) where TDelegate : Delegate => AccessTools2.GetDelegate<TDelegate>((object) null, methodInfo);

    public static TDelegate? GetDelegateObjectInstance<TDelegate>(MethodInfo methodInfo) where TDelegate : Delegate => AccessTools2.GetDelegate<TDelegate>(methodInfo);

    private static bool ParametersAreEqual(
      ParameterInfo[] delegateParameters,
      ParameterInfo[] methodParameters)
    {
      if (delegateParameters.Length - methodParameters.Length == 0)
      {
        for (int index = 0; index < methodParameters.Length; ++index)
        {
          if (delegateParameters[index].ParameterType.IsByRef != methodParameters[index].ParameterType.IsByRef || !delegateParameters[index].ParameterType.IsAssignableFrom(methodParameters[index].ParameterType))
            return false;
        }
        return true;
      }
      if (delegateParameters.Length - methodParameters.Length != 1)
        return false;
      for (int index = 0; index < methodParameters.Length; ++index)
      {
        if (delegateParameters[index + 1].ParameterType.IsByRef != methodParameters[index].ParameterType.IsByRef || !delegateParameters[index + 1].ParameterType.IsAssignableFrom(methodParameters[index].ParameterType))
          return false;
      }
      return true;
    }

    public static TDelegate? GetDelegate<TDelegate, TInstance>(
      TInstance instance,
      MethodInfo methodInfo)
      where TDelegate : Delegate
    {
      return AccessTools2.GetDelegate<TDelegate>((object) instance, methodInfo);
    }

    public static TDelegate? GetDeclaredDelegateObjectInstance<TDelegate>(
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDelegateObjectInstance<TDelegate>(
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredDelegateObjectInstance<TDelegate>(
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDelegateObjectInstance<TDelegate>(
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegateObjectInstance<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredDelegate<TDelegate>(
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDelegate<TDelegate>(
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredDelegate<TDelegate>(
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDelegate<TDelegate>(
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(methodInfo);
    }

    public static TDelegate? GetDeclaredDelegate<TDelegate, TInstance>(
      TInstance instance,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      if ((object) instance != null)
      {
        MethodInfo methodInfo = AccessTools2.DeclaredMethod(instance.GetType(), method, parameters, generics);
        if ((object) methodInfo != null)
          return AccessTools2.GetDelegate<TDelegate, TInstance>(instance, methodInfo);
      }
      return default (TDelegate);
    }

    public static TDelegate? GetDelegate<TDelegate, TInstance>(
      TInstance instance,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      if ((object) instance != null)
      {
        MethodInfo methodInfo = AccessTools2.Method(instance.GetType(), method, parameters, generics);
        if ((object) methodInfo != null)
          return AccessTools2.GetDelegate<TDelegate, TInstance>(instance, methodInfo);
      }
      return default (TDelegate);
    }

    public static TDelegate? GetDeclaredDelegate<TDelegate>(
      object? instance,
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDelegate<TDelegate>(
      object? instance,
      Type type,
      string method,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(type, method, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDeclaredDelegate<TDelegate>(
      object? instance,
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.DeclaredMethod(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static TDelegate? GetDelegate<TDelegate>(
      object? instance,
      string typeSemicolonMethod,
      Type[]? parameters = null,
      Type[]? generics = null)
      where TDelegate : Delegate
    {
      MethodInfo methodInfo = AccessTools2.Method(typeSemicolonMethod, parameters, generics);
      return (object) methodInfo == null ? default (TDelegate) : AccessTools2.GetDelegate<TDelegate>(instance, methodInfo);
    }

    public static FieldInfo? DeclaredField(Type type, string name)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.DeclaredField: 'type' is null");
        return (FieldInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.DeclaredField: type '{0}', 'name' is null", (object) type));
        return (FieldInfo) null;
      }
      FieldInfo field = type.GetField(name, AccessTools.allDeclared);
      if ((object) field != null)
        return field;
      Trace.TraceError(string.Format("AccessTools2.DeclaredField: Could not find field for type '{0}' and name '{1}'", (object) type, (object) name));
      return (FieldInfo) null;
    }

    public static FieldInfo? Field(Type type, string name)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.Field: 'type' is null");
        return (FieldInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.Field: type '{0}', 'name' is null", (object) type));
        return (FieldInfo) null;
      }
      FieldInfo includingBaseTypes = AccessTools2.FindIncludingBaseTypes<FieldInfo>(type, (Func<Type, FieldInfo>) (t => t.GetField(name, AccessTools.all)));
      if ((object) includingBaseTypes == null)
        Trace.TraceError(string.Format("AccessTools2.Field: Could not find field for type '{0}' and name '{1}'", (object) type, (object) name));
      return includingBaseTypes;
    }

    public static FieldInfo? DeclaredField(string typeColonFieldname)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonFieldname, out type, out name))
        return AccessTools2.DeclaredField(type, name);
      Trace.TraceError("AccessTools2.Field: Could not find type or field for '" + typeColonFieldname + "'");
      return (FieldInfo) null;
    }

    public static FieldInfo? Field(string typeColonFieldname)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonFieldname, out type, out name))
        return AccessTools2.Field(type, name);
      Trace.TraceError("AccessTools2.Field: Could not find type or field for '" + typeColonFieldname + "'");
      return (FieldInfo) null;
    }

    public static AccessTools.FieldRef<object, F>? FieldRefAccess<F>(
      string typeColonFieldname)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonFieldname, out type, out name))
        return AccessTools2.FieldRefAccess<F>(type, name);
      Trace.TraceError("AccessTools2.FieldRefAccess: Could not find type or field for '" + typeColonFieldname + "'");
      return (AccessTools.FieldRef<object, F>) null;
    }

    public static AccessTools.FieldRef<T, F>? FieldRefAccess<T, F>(string fieldName) where T : class
    {
      if (fieldName == null)
        return (AccessTools.FieldRef<T, F>) null;
      FieldInfo instanceField = AccessTools2.GetInstanceField(typeof (T), fieldName);
      return (object) instanceField == null ? (AccessTools.FieldRef<T, F>) null : AccessTools2.FieldRefAccessInternal<T, F>(instanceField, false);
    }

    public static AccessTools.FieldRef<object, F>? FieldRefAccess<F>(
      Type type,
      string fieldName)
    {
      if ((object) type == null)
        return (AccessTools.FieldRef<object, F>) null;
      if (fieldName == null)
        return (AccessTools.FieldRef<object, F>) null;
      FieldInfo fieldInfo = AccessTools2.Field(type, fieldName);
      if ((object) fieldInfo == null)
        return (AccessTools.FieldRef<object, F>) null;
      if (!fieldInfo.IsStatic)
      {
        Type declaringType = fieldInfo.DeclaringType;
        if ((object) declaringType != null)
        {
          if (!declaringType.IsValueType)
            return AccessTools2.FieldRefAccessInternal<object, F>(fieldInfo, true);
          Trace.TraceError("AccessTools2.FieldRefAccess<object, " + typeof (F).FullName + ">: FieldDeclaringType must be a class");
          return (AccessTools.FieldRef<object, F>) null;
        }
      }
      return (AccessTools.FieldRef<object, F>) null;
    }

    public static AccessTools.FieldRef<T, F>? FieldRefAccess<T, F>(FieldInfo fieldInfo) where T : class
    {
      if ((object) fieldInfo == null)
        return (AccessTools.FieldRef<T, F>) null;
      if (!fieldInfo.IsStatic)
      {
        Type declaringType = fieldInfo.DeclaringType;
        if ((object) declaringType != null)
        {
          if (declaringType.IsValueType)
          {
            Trace.TraceError("AccessTools2.FieldRefAccess<" + typeof (T).FullName + ", " + typeof (F).FullName + ">: FieldDeclaringType must be a class");
            return (AccessTools.FieldRef<T, F>) null;
          }
          bool? nullable = AccessTools2.FieldRefNeedsClasscast(typeof (T), declaringType);
          if (!nullable.HasValue)
            return (AccessTools.FieldRef<T, F>) null;
          bool valueOrDefault = nullable.GetValueOrDefault();
          return AccessTools2.FieldRefAccessInternal<T, F>(fieldInfo, valueOrDefault);
        }
      }
      return (AccessTools.FieldRef<T, F>) null;
    }

    private static AccessTools.FieldRef<T, F>? FieldRefAccessInternal<T, F>(
      FieldInfo fieldInfo,
      bool needCastclass)
      where T : class
    {
      if (!AccessTools2.Helper.IsValid())
        return (AccessTools.FieldRef<T, F>) null;
      if (fieldInfo.IsStatic)
      {
        Trace.TraceError("AccessTools2.FieldRefAccessInternal<" + typeof (T).FullName + ", " + typeof (F).FullName + ">: Field must not be static");
        return (AccessTools.FieldRef<T, F>) null;
      }
      if (!AccessTools2.ValidateFieldType<F>(fieldInfo))
        return (AccessTools.FieldRef<T, F>) null;
      Type type = typeof (T);
      Type declaringType = fieldInfo.DeclaringType;
      AccessTools2.DynamicMethodDefinitionHandle? nullable1 = AccessTools2.DynamicMethodDefinitionHandle.Create("__refget_" + type.Name + "_fi_" + fieldInfo.Name, typeof (F).MakeByRefType(), new Type[1]
      {
        type
      });
      AccessTools2.DynamicMethodDefinitionHandle valueOrDefault1;
      AccessTools2.ILGeneratorHandle? nullable2;
      if (!nullable1.HasValue)
      {
        nullable2 = new AccessTools2.ILGeneratorHandle?();
      }
      else
      {
        valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable2 = valueOrDefault1.GetILGenerator();
      }
      AccessTools2.ILGeneratorHandle? nullable3 = nullable2;
      if (!nullable3.HasValue)
        return (AccessTools.FieldRef<T, F>) null;
      AccessTools2.ILGeneratorHandle valueOrDefault2 = nullable3.GetValueOrDefault();
      valueOrDefault2.Emit(OpCodes.Ldarg_0);
      if (needCastclass)
        valueOrDefault2.Emit(OpCodes.Castclass, declaringType);
      valueOrDefault2.Emit(OpCodes.Ldflda, fieldInfo);
      valueOrDefault2.Emit(OpCodes.Ret);
      Delegate @delegate;
      if (!nullable1.HasValue)
      {
        @delegate = (Delegate) null;
      }
      else
      {
        valueOrDefault1 = nullable1.GetValueOrDefault();
        @delegate = valueOrDefault1.Generate()?.CreateDelegate(typeof (AccessTools.FieldRef<T, F>));
      }
      return @delegate as AccessTools.FieldRef<T, F>;
    }

    private static bool? FieldRefNeedsClasscast(Type delegateInstanceType, Type declaringType)
    {
      bool flag = false;
      if (delegateInstanceType != declaringType)
      {
        flag = delegateInstanceType.IsAssignableFrom(declaringType);
        if (!flag && !declaringType.IsAssignableFrom(delegateInstanceType))
        {
          Trace.TraceError(string.Format("AccessTools2.FieldRefNeedsClasscast: FieldDeclaringType must be assignable from or to T (FieldRefAccess instance type) - 'instanceOfT is FieldDeclaringType' must be possible, delegateInstanceType '{0}', declaringType '{1}'", (object) delegateInstanceType, (object) declaringType));
          return new bool?();
        }
      }
      return new bool?(flag);
    }

    public static AccessTools.FieldRef<object, TField>? FieldRefAccess<TField>(
      FieldInfo fieldInfo)
    {
      return (object) fieldInfo != null ? AccessTools.FieldRefAccess<object, TField>(fieldInfo) : (AccessTools.FieldRef<object, TField>) null;
    }

    public static MethodInfo? DeclaredMethod(
      Type type,
      string name,
      Type[]? parameters = null,
      Type[]? generics = null)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.DeclaredMethod: 'type' is null");
        return (MethodInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: type '{0}', 'name' is null", (object) type));
        return (MethodInfo) null;
      }
      MethodInfo methodInfo;
      if (parameters == null)
      {
        try
        {
          methodInfo = type.GetMethod(name, AccessTools.allDeclared);
        }
        catch (AmbiguousMatchException ex)
        {
          methodInfo = type.GetMethod(name, AccessTools.allDeclared, (Binder) null, Type.EmptyTypes, new ParameterModifier[0]);
          if ((object) methodInfo == null)
          {
            Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: Ambiguous match for type '{0}' and name '{1}' and parameters '{2}', '{3}'", (object) type, (object) name, parameters != null ? (object) GeneralExtensions.Description(parameters) : (object) (string) null, (object) ex));
            return (MethodInfo) null;
          }
        }
      }
      else
        methodInfo = type.GetMethod(name, AccessTools.allDeclared, (Binder) null, parameters, new ParameterModifier[0]);
      if ((object) methodInfo == null)
      {
        Trace.TraceError(string.Format("AccessTools2.DeclaredMethod: Could not find method for type '{0}' and name '{1}' and parameters '{2}'", (object) type, (object) name, parameters != null ? (object) GeneralExtensions.Description(parameters) : (object) (string) null));
        return (MethodInfo) null;
      }
      if (generics != null)
        methodInfo = methodInfo.MakeGenericMethod(generics);
      return methodInfo;
    }

    public static MethodInfo? Method(
      Type type,
      string name,
      Type[]? parameters = null,
      Type[]? generics = null)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.Method: 'type' is null");
        return (MethodInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.Method: type '{0}', 'name' is null", (object) type));
        return (MethodInfo) null;
      }
      MethodInfo methodInfo;
      if (parameters == null)
      {
        try
        {
          methodInfo = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Func<Type, MethodInfo>) (t => t.GetMethod(name, AccessTools.all)));
        }
        catch (AmbiguousMatchException ex)
        {
          methodInfo = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Func<Type, MethodInfo>) (t => t.GetMethod(name, AccessTools.all, (Binder) null, Type.EmptyTypes, new ParameterModifier[0])));
          if ((object) methodInfo == null)
          {
            object[] objArray = new object[4]
            {
              (object) type,
              (object) name,
              null,
              null
            };
            Type[] typeArray = parameters;
            objArray[2] = (object) (typeArray != null ? GeneralExtensions.Description(typeArray) : (string) null);
            objArray[3] = (object) ex;
            Trace.TraceError(string.Format("AccessTools2.Method: Ambiguous match for type '{0}' and name '{1}' and parameters '{2}', '{3}'", objArray));
            return (MethodInfo) null;
          }
        }
      }
      else
        methodInfo = AccessTools2.FindIncludingBaseTypes<MethodInfo>(type, (Func<Type, MethodInfo>) (t => t.GetMethod(name, AccessTools.all, (Binder) null, parameters, new ParameterModifier[0])));
      if ((object) methodInfo == null)
      {
        Type type1 = type;
        string str1 = name;
        Type[] typeArray = parameters;
        string str2 = typeArray != null ? GeneralExtensions.Description(typeArray) : (string) null;
        Trace.TraceError(string.Format("AccessTools2.Method: Could not find method for type '{0}' and name '{1}' and parameters '{2}'", (object) type1, (object) str1, (object) str2));
        return (MethodInfo) null;
      }
      if (generics != null)
        methodInfo = methodInfo.MakeGenericMethod(generics);
      return methodInfo;
    }

    public static MethodInfo? DeclaredMethod(
      string typeColonMethodname,
      Type[]? parameters = null,
      Type[]? generics = null)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonMethodname, out type, out name))
        return AccessTools2.DeclaredMethod(type, name, parameters, generics);
      Trace.TraceError("AccessTools2.Method: Could not find type or property for '" + typeColonMethodname + "'");
      return (MethodInfo) null;
    }

    public static MethodInfo? Method(
      string typeColonMethodname,
      Type[]? parameters = null,
      Type[]? generics = null)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonMethodname, out type, out name))
        return AccessTools2.Method(type, name, parameters, generics);
      Trace.TraceError("AccessTools2.Method: Could not find type or property for '" + typeColonMethodname + "'");
      return (MethodInfo) null;
    }

    public static PropertyInfo? DeclaredProperty(Type type, string name)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.DeclaredProperty: 'type' is null");
        return (PropertyInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.DeclaredProperty: type '{0}', 'name' is null", (object) type));
        return (PropertyInfo) null;
      }
      PropertyInfo property = type.GetProperty(name, AccessTools.allDeclared);
      if ((object) property == null)
        Trace.TraceError(string.Format("AccessTools2.DeclaredProperty: Could not find property for type '{0}' and name '{1}'", (object) type, (object) name));
      return property;
    }

    public static PropertyInfo? Property(Type type, string name)
    {
      if ((object) type == null)
      {
        Trace.TraceError("AccessTools2.Property: 'type' is null");
        return (PropertyInfo) null;
      }
      if (name == null)
      {
        Trace.TraceError(string.Format("AccessTools2.Property: type '{0}', 'name' is null", (object) type));
        return (PropertyInfo) null;
      }
      PropertyInfo includingBaseTypes = AccessTools2.FindIncludingBaseTypes<PropertyInfo>(type, (Func<Type, PropertyInfo>) (t => t.GetProperty(name, AccessTools.all)));
      if ((object) includingBaseTypes == null)
        Trace.TraceError(string.Format("AccessTools2.Property: Could not find property for type '{0}' and name '{1}'", (object) type, (object) name));
      return includingBaseTypes;
    }

    public static MethodInfo? DeclaredPropertyGetter(Type type, string name) => AccessTools2.DeclaredProperty(type, name)?.GetGetMethod(true);

    public static MethodInfo? DeclaredPropertySetter(Type type, string name) => AccessTools2.DeclaredProperty(type, name)?.GetSetMethod(true);

    public static MethodInfo? PropertyGetter(Type type, string name) => AccessTools2.Property(type, name)?.GetGetMethod(true);

    public static MethodInfo? PropertySetter(Type type, string name) => AccessTools2.Property(type, name)?.GetSetMethod(true);

    public static PropertyInfo? DeclaredProperty(string typeColonPropertyName)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonPropertyName, out type, out name))
        return AccessTools2.DeclaredProperty(type, name);
      Trace.TraceError("AccessTools2.DeclaredProperty: Could not find type or property for '" + typeColonPropertyName + "'");
      return (PropertyInfo) null;
    }

    public static PropertyInfo? Property(string typeColonPropertyName)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonPropertyName, out type, out name))
        return AccessTools2.Property(type, name);
      Trace.TraceError("AccessTools2.Property: Could not find type or property for '" + typeColonPropertyName + "'");
      return (PropertyInfo) null;
    }

    public static MethodInfo? DeclaredPropertySetter(string typeColonPropertyName) => AccessTools2.DeclaredProperty(typeColonPropertyName)?.GetSetMethod(true);

    public static MethodInfo? DeclaredPropertyGetter(string typeColonPropertyName) => AccessTools2.DeclaredProperty(typeColonPropertyName)?.GetGetMethod(true);

    public static MethodInfo? PropertyGetter(string typeColonPropertyName) => AccessTools2.Property(typeColonPropertyName)?.GetGetMethod(true);

    public static MethodInfo? PropertySetter(string typeColonPropertyName) => AccessTools2.Property(typeColonPropertyName)?.GetSetMethod(true);

    public static AccessTools.FieldRef<TField>? StaticFieldRefAccess<TField>(
      string typeColonFieldname)
    {
      Type type;
      string name;
      if (AccessTools2.TryGetComponents(typeColonFieldname, out type, out name))
        return AccessTools2.StaticFieldRefAccess<TField>(type, name);
      Trace.TraceError("AccessTools2.StaticFieldRefAccess: Could not find type or field for '" + typeColonFieldname + "'");
      return (AccessTools.FieldRef<TField>) null;
    }

    public static AccessTools.FieldRef<F>? StaticFieldRefAccess<F>(FieldInfo fieldInfo) => (object) fieldInfo == null ? (AccessTools.FieldRef<F>) null : AccessTools2.StaticFieldRefAccessInternal<F>(fieldInfo);

    public static AccessTools.FieldRef<TField>? StaticFieldRefAccess<TField>(
      Type type,
      string fieldName)
    {
      FieldInfo fieldInfo = AccessTools2.Field(type, fieldName);
      return (object) fieldInfo == null ? (AccessTools.FieldRef<TField>) null : AccessTools2.StaticFieldRefAccess<TField>(fieldInfo);
    }

    private static AccessTools.FieldRef<F>? StaticFieldRefAccessInternal<F>(
      FieldInfo fieldInfo)
    {
      if (!AccessTools2.Helper.IsValid())
        return (AccessTools.FieldRef<F>) null;
      if (!fieldInfo.IsStatic)
      {
        Trace.TraceError("AccessTools2.StaticFieldRefAccessInternal<" + typeof (F).FullName + ">: Field must be static");
        return (AccessTools.FieldRef<F>) null;
      }
      if (!AccessTools2.ValidateFieldType<F>(fieldInfo))
        return (AccessTools.FieldRef<F>) null;
      AccessTools2.DynamicMethodDefinitionHandle? nullable1 = AccessTools2.DynamicMethodDefinitionHandle.Create("__refget_" + (fieldInfo.DeclaringType?.Name ?? "null") + "_static_fi_" + fieldInfo.Name, typeof (F).MakeByRefType(), new Type[0]);
      AccessTools2.DynamicMethodDefinitionHandle valueOrDefault1;
      AccessTools2.ILGeneratorHandle? nullable2;
      if (!nullable1.HasValue)
      {
        nullable2 = new AccessTools2.ILGeneratorHandle?();
      }
      else
      {
        valueOrDefault1 = nullable1.GetValueOrDefault();
        nullable2 = valueOrDefault1.GetILGenerator();
      }
      AccessTools2.ILGeneratorHandle? nullable3 = nullable2;
      if (!nullable3.HasValue)
        return (AccessTools.FieldRef<F>) null;
      AccessTools2.ILGeneratorHandle valueOrDefault2 = nullable3.GetValueOrDefault();
      valueOrDefault2.Emit(OpCodes.Ldsflda, fieldInfo);
      valueOrDefault2.Emit(OpCodes.Ret);
      Delegate @delegate;
      if (!nullable1.HasValue)
      {
        @delegate = (Delegate) null;
      }
      else
      {
        valueOrDefault1 = nullable1.GetValueOrDefault();
        @delegate = valueOrDefault1.Generate()?.CreateDelegate(typeof (AccessTools.FieldRef<F>));
      }
      return @delegate as AccessTools.FieldRef<F>;
    }

    public static AccessTools.StructFieldRef<T, F>? StructFieldRefAccess<T, F>(
      string fieldName)
      where T : struct
    {
      if (string.IsNullOrEmpty(fieldName))
        return (AccessTools.StructFieldRef<T, F>) null;
      FieldInfo instanceField = AccessTools2.GetInstanceField(typeof (T), fieldName);
      return (object) instanceField == null ? (AccessTools.StructFieldRef<T, F>) null : AccessTools2.StructFieldRefAccessInternal<T, F>(instanceField);
    }

    public static AccessTools.StructFieldRef<T, F>? StructFieldRefAccess<T, F>(
      FieldInfo? fieldInfo)
      where T : struct
    {
      if ((object) fieldInfo == null)
        return (AccessTools.StructFieldRef<T, F>) null;
      return !AccessTools2.ValidateStructField<T, F>(fieldInfo) ? (AccessTools.StructFieldRef<T, F>) null : AccessTools2.StructFieldRefAccessInternal<T, F>(fieldInfo);
    }

    private static AccessTools.StructFieldRef<T, F>? StructFieldRefAccessInternal<T, F>(
      FieldInfo fieldInfo)
      where T : struct
    {
      if (!AccessTools2.ValidateFieldType<F>(fieldInfo))
        return (AccessTools.StructFieldRef<T, F>) null;
      AccessTools2.DynamicMethodDefinitionHandle? nullable = AccessTools2.DynamicMethodDefinitionHandle.Create("__refget_" + typeof (T).Name + "_struct_fi_" + fieldInfo.Name, typeof (F).MakeByRefType(), new Type[1]
      {
        typeof (T).MakeByRefType()
      });
      AccessTools2.ILGeneratorHandle? ilGenerator = (AccessTools2.ILGeneratorHandle?) nullable?.GetILGenerator();
      if (!ilGenerator.HasValue)
        return (AccessTools.StructFieldRef<T, F>) null;
      AccessTools2.ILGeneratorHandle valueOrDefault = ilGenerator.GetValueOrDefault();
      valueOrDefault.Emit(OpCodes.Ldarg_0);
      valueOrDefault.Emit(OpCodes.Ldflda, fieldInfo);
      valueOrDefault.Emit(OpCodes.Ret);
      return nullable?.Generate()?.CreateDelegate(typeof (AccessTools.StructFieldRef<T, F>)) as AccessTools.StructFieldRef<T, F>;
    }

    public static IEnumerable<Assembly> AllAssemblies() => ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (a => !a.FullName.StartsWith("Microsoft.VisualStudio")));

    public static IEnumerable<Type> AllTypes() => AccessTools2.AllAssemblies().SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>) (a => (IEnumerable<Type>) AccessTools2.GetTypesFromAssembly(a)));

    public static Type[] GetTypesFromAssembly(Assembly assembly)
    {
      if ((object) assembly == null)
        return Type.EmptyTypes;
      try
      {
        return assembly.GetTypes();
      }
      catch (ReflectionTypeLoadException ex)
      {
        Trace.TraceError(string.Format("AccessTools2.GetTypesFromAssembly: assembly {0} => {1}", (object) assembly, (object) ex));
        return ((IEnumerable<Type>) ex.Types).Where<Type>((Func<Type, bool>) (type => type != null)).ToArray<Type>();
      }
    }

    public static Type[] GetTypesFromAssemblyIfValid(Assembly assembly)
    {
      if ((object) assembly == null)
        return Type.EmptyTypes;
      try
      {
        return assembly.GetTypes();
      }
      catch (ReflectionTypeLoadException ex)
      {
        Trace.TraceError(string.Format("AccessTools2.GetTypesFromAssemblyIfValid: assembly {0} => {1}", (object) assembly, (object) ex));
        return Type.EmptyTypes;
      }
    }

    public static Type? TypeByName(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        Trace.TraceError("AccessTools2.TypeByName: 'name' is null or empty");
        return (Type) null;
      }
      Type type = Type.GetType(name, false);
      if ((object) type == null)
        type = AccessTools2.AllTypes().FirstOrDefault<Type>((Func<Type, bool>) (t => t.FullName == name));
      if ((object) type == null)
        type = AccessTools2.AllTypes().FirstOrDefault<Type>((Func<Type, bool>) (t => t.Name == name));
      if ((object) type == null)
        Trace.TraceError("AccessTools2.TypeByName: Could not find type named '" + name + "'");
      return type;
    }

    public static T? FindIncludingBaseTypes<T>(Type type, Func<Type, T> func) where T : class
    {
      if ((object) type == null || func == null)
        return default (T);
      do
      {
        T includingBaseTypes = func(type);
        if ((object) includingBaseTypes != null)
          return includingBaseTypes;
        type = type.BaseType;
      }
      while ((object) type != null);
      return default (T);
    }

    private static FieldInfo? GetInstanceField(Type type, string fieldName)
    {
      FieldInfo instanceField = AccessTools2.Field(type, fieldName);
      if ((object) instanceField == null)
        return (FieldInfo) null;
      if (!instanceField.IsStatic)
        return instanceField;
      Trace.TraceError(string.Format("AccessTools2.GetInstanceField: Field must not be static, type '{0}', fieldName '{1}'", (object) type, (object) fieldName));
      return (FieldInfo) null;
    }

    private static bool ValidateFieldType<F>(FieldInfo? fieldInfo)
    {
      if ((object) fieldInfo == null)
      {
        Trace.TraceError("AccessTools2.ValidateFieldType<" + typeof (F).FullName + ">: 'fieldInfo' is null");
        return false;
      }
      Type type = typeof (F);
      Type fieldType = fieldInfo.FieldType;
      if (type == fieldType)
        return true;
      if (fieldType.IsEnum)
      {
        Type underlyingType = Enum.GetUnderlyingType(fieldType);
        if (type != underlyingType)
        {
          Trace.TraceError(string.Format("AccessTools2.ValidateFieldType<{0}>: FieldRefAccess return type must be the same as FieldType or FieldType's underlying integral type ({1}) for enum types, fieldInfo '{2}'", (object) typeof (F).FullName, (object) underlyingType, (object) fieldInfo));
          return false;
        }
      }
      else
      {
        if (fieldType.IsValueType)
        {
          Trace.TraceError(string.Format("AccessTools2.ValidateFieldType<{0}>: FieldRefAccess return type must be the same as FieldType for value types, fieldInfo '{1}'", (object) typeof (F).FullName, (object) fieldInfo));
          return false;
        }
        if (!type.IsAssignableFrom(fieldType))
        {
          Trace.TraceError("AccessTools2.ValidateFieldType<" + typeof (F).FullName + ">: FieldRefAccess return type must be assignable from FieldType for reference types");
          return false;
        }
      }
      return true;
    }

    private static bool ValidateStructField<T, F>(FieldInfo? fieldInfo) where T : struct
    {
      if ((object) fieldInfo == null)
        return false;
      if (fieldInfo.IsStatic)
      {
        Trace.TraceError("AccessTools2.ValidateStructField<" + typeof (T).FullName + ", " + typeof (F).FullName + ">: Field must not be static");
        return false;
      }
      if (!(fieldInfo.DeclaringType != typeof (T)))
        return true;
      Trace.TraceError("AccessTools2.ValidateStructField<" + typeof (T).FullName + ", " + typeof (F).FullName + ">: FieldDeclaringType must be T (StructFieldRefAccess instance type)");
      return false;
    }

    private static bool TryGetComponents(string typeColonName, out Type? type, out string? name)
    {
      if (string.IsNullOrWhiteSpace(typeColonName))
      {
        Trace.TraceError("AccessTools2.TryGetComponents: 'typeColonName' is null or whitespace/empty");
        type = (Type) null;
        name = (string) null;
        return false;
      }
      string[] strArray = typeColonName.Split(':');
      if (strArray.Length != 2)
      {
        Trace.TraceError("AccessTools2.TryGetComponents: typeColonName '" + typeColonName + "', name must be specified as 'Namespace.Type1.Type2:Name");
        type = (Type) null;
        name = (string) null;
        return false;
      }
      type = AccessTools2.TypeByName(strArray[0]);
      name = strArray[1];
      return (object) type != null;
    }

    [ExcludeFromCodeCoverage]
    private readonly struct DynamicMethodDefinitionHandle
    {
      private readonly object _dynamicMethodDefinition;

      public static AccessTools2.DynamicMethodDefinitionHandle? Create(
        string name,
        Type returnType,
        Type[] parameterTypes)
      {
        return AccessTools2.Helper.DynamicMethodDefinitionCtor != null ? new AccessTools2.DynamicMethodDefinitionHandle?(new AccessTools2.DynamicMethodDefinitionHandle(AccessTools2.Helper.DynamicMethodDefinitionCtor(name, returnType, parameterTypes))) : new AccessTools2.DynamicMethodDefinitionHandle?();
      }

      public DynamicMethodDefinitionHandle(object dynamicMethodDefinition) => this._dynamicMethodDefinition = dynamicMethodDefinition;

      public AccessTools2.ILGeneratorHandle? GetILGenerator() => AccessTools2.Helper.GetILGenerator != null ? new AccessTools2.ILGeneratorHandle?(new AccessTools2.ILGeneratorHandle(AccessTools2.Helper.GetILGenerator(this._dynamicMethodDefinition))) : new AccessTools2.ILGeneratorHandle?();

      public MethodInfo? Generate() => AccessTools2.Helper.Generate != null ? AccessTools2.Helper.Generate(this._dynamicMethodDefinition) : (MethodInfo) null;
    }

    [ExcludeFromCodeCoverage]
    private readonly struct ILGeneratorHandle
    {
      private readonly object _ilGenerator;

      public ILGeneratorHandle(object ilGenerator) => this._ilGenerator = ilGenerator;

      public void Emit(OpCode opcode)
      {
        AccessTools2.Helper.Emit1Delegate emit1 = AccessTools2.Helper.Emit1;
        if (emit1 == null)
          return;
        emit1(this._ilGenerator, opcode);
      }

      public void Emit(OpCode opcode, FieldInfo field)
      {
        AccessTools2.Helper.Emit2Delegate emit2 = AccessTools2.Helper.Emit2;
        if (emit2 == null)
          return;
        emit2(this._ilGenerator, opcode, field);
      }

      public void Emit(OpCode opcode, Type type)
      {
        AccessTools2.Helper.Emit3Delegate emit3 = AccessTools2.Helper.Emit3;
        if (emit3 == null)
          return;
        emit3(this._ilGenerator, opcode, type);
      }
    }

    [ExcludeFromCodeCoverage]
    private static class Helper
    {
      public static readonly AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate? DynamicMethodDefinitionCtor = AccessTools2.GetDeclaredConstructorDelegate<AccessTools2.Helper.DynamicMethodDefinitionCtorDelegate>("MonoMod.Utils.DynamicMethodDefinition", new Type[3]
      {
        typeof (string),
        typeof (Type),
        typeof (Type[])
      });
      public static readonly AccessTools2.Helper.GetILGeneratorDelegate? GetILGenerator = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GetILGeneratorDelegate>("MonoMod.Utils.DynamicMethodDefinition:GetILGenerator", Type.EmptyTypes);
      public static readonly AccessTools2.Helper.Emit1Delegate? Emit1 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit1Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[1]
      {
        typeof (OpCode)
      });
      public static readonly AccessTools2.Helper.Emit2Delegate? Emit2 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit2Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[2]
      {
        typeof (OpCode),
        typeof (FieldInfo)
      });
      public static readonly AccessTools2.Helper.Emit3Delegate? Emit3 = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.Emit3Delegate>("System.Reflection.Emit.ILGenerator:Emit", new Type[2]
      {
        typeof (OpCode),
        typeof (Type)
      });
      public static readonly AccessTools2.Helper.GenerateDelegate? Generate = AccessTools2.GetDelegateObjectInstance<AccessTools2.Helper.GenerateDelegate>("MonoMod.Utils.DynamicMethodDefinition:Generate", Type.EmptyTypes);

      public static bool IsValid()
      {
        if (AccessTools2.Helper.DynamicMethodDefinitionCtor == null)
        {
          Trace.TraceError("AccessTools2.Helper.IsValid: DynamicMethodDefinitionCtor is null");
          return false;
        }
        if (AccessTools2.Helper.GetILGenerator == null)
        {
          Trace.TraceError("AccessTools2.Helper.IsValid: GetILGenerator is null");
          return false;
        }
        if (AccessTools2.Helper.Emit1 == null)
        {
          Trace.TraceError("AccessTools2.Helper.IsValid: Emit1 is null");
          return false;
        }
        if (AccessTools2.Helper.Emit2 == null)
        {
          Trace.TraceError("AccessTools2.Helper.IsValid: Emit2 is null");
          return false;
        }
        if (AccessTools2.Helper.Emit3 == null)
        {
          Trace.TraceError("AccessTools2.Helper.IsValid: Emit3 is null");
          return false;
        }
        if (AccessTools2.Helper.Generate != null)
          return true;
        Trace.TraceError("AccessTools2.Helper.IsValid: Generate is null");
        return false;
      }

      public delegate object DynamicMethodDefinitionCtorDelegate(
        string name,
        Type returnType,
        Type[] parameterTypes);

      public delegate object GetILGeneratorDelegate(object instance);

      public delegate void Emit1Delegate(object instance, OpCode opcode);

      public delegate void Emit2Delegate(object instance, OpCode opcode, FieldInfo field);

      public delegate void Emit3Delegate(object instance, OpCode opcode, Type type);

      public delegate MethodInfo GenerateDelegate(object instance);
    }
  }
}
