using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

#nullable enable
namespace HarmonyLib.BUTR.Extensions
{
    [ExcludeFromCodeCoverage]
    internal readonly struct AccessCacheHandle
    {
        private static readonly AccessCacheHandle.AccessCacheCtorDelegate? AccessCacheCtorMethod = AccessTools2.GetDeclaredConstructorDelegate<AccessCacheHandle.AccessCacheCtorDelegate>("HarmonyLib.AccessCache");
        private static readonly AccessCacheHandle.GetFieldInfoDelegate? GetFieldInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetFieldInfoDelegate>("HarmonyLib.AccessCache:GetFieldInfo");
        private static readonly AccessCacheHandle.GetPropertyInfoDelegate? GetPropertyInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetPropertyInfoDelegate>("HarmonyLib.AccessCache:GetPropertyInfo");
        private static readonly AccessCacheHandle.GetMethodInfoDelegate? GetMethodInfoMethod = AccessTools2.GetDelegateObjectInstance<AccessCacheHandle.GetMethodInfoDelegate>("HarmonyLib.AccessCache:GetMethodInfo");
        private readonly object _accessCache;

        public static AccessCacheHandle? Create()
        {
            AccessCacheHandle.AccessCacheCtorDelegate accessCacheCtorMethod = AccessCacheHandle.AccessCacheCtorMethod;
            object accessCache = accessCacheCtorMethod != null ? accessCacheCtorMethod() : (object)null;
            return accessCache == null ? new AccessCacheHandle?() : new AccessCacheHandle?(new AccessCacheHandle(accessCache));
        }

        private AccessCacheHandle(object accessCache) => this._accessCache = accessCache;

        public FieldInfo? GetFieldInfo(
          Type type,
          string name,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false)
        {
            AccessCacheHandle.GetFieldInfoDelegate getFieldInfoMethod = AccessCacheHandle.GetFieldInfoMethod;
            return getFieldInfoMethod == null ? (FieldInfo)null : getFieldInfoMethod(this._accessCache, type, name, memberType, declaredOnly);
        }

        public PropertyInfo? GetPropertyInfo(
          Type type,
          string name,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false)
        {
            AccessCacheHandle.GetPropertyInfoDelegate propertyInfoMethod = AccessCacheHandle.GetPropertyInfoMethod;
            return propertyInfoMethod == null ? (PropertyInfo)null : propertyInfoMethod(this._accessCache, type, name, memberType, declaredOnly);
        }

        public MethodBase? GetMethodInfo(
          Type type,
          string name,
          Type[] arguments,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false)
        {
            AccessCacheHandle.GetMethodInfoDelegate methodInfoMethod = AccessCacheHandle.GetMethodInfoMethod;
            return methodInfoMethod == null ? (MethodBase)null : methodInfoMethod(this._accessCache, type, name, arguments, memberType, declaredOnly);
        }

        internal enum MemberType
        {
            Any,
            Static,
            Instance,
        }

        private delegate object AccessCacheCtorDelegate();

        private delegate FieldInfo GetFieldInfoDelegate(
          object instance,
          Type type,
          string name,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false);

        private delegate PropertyInfo GetPropertyInfoDelegate(
          object instance,
          Type type,
          string name,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false);

        private delegate MethodBase GetMethodInfoDelegate(
          object instance,
          Type type,
          string name,
          Type[] arguments,
          AccessCacheHandle.MemberType memberType = AccessCacheHandle.MemberType.Any,
          bool declaredOnly = false);
    }
}
