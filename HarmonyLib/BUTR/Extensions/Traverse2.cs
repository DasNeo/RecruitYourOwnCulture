// Decompiled with JetBrains decompiler
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;


#nullable enable
namespace HarmonyLib.BUTR.Extensions
{
    internal class Traverse2
    {
        private static readonly AccessCacheHandle? Cache;
        private readonly System.Type? _type;
        private readonly object? _root;
        private readonly MemberInfo? _info;
        private readonly MethodBase? _method;
        private readonly object[]? _params;
        public static Action<Traverse2, Traverse2> CopyFields = (Action<Traverse2, Traverse2>)((from, to) =>
        {
            if (from == null || to == null)
                return;
            to.SetValue(from.GetValue());
        });

        [MethodImpl(MethodImplOptions.Synchronized)]
        static Traverse2()
        {
            if (Traverse2.Cache.HasValue)
                return;
            Traverse2.Cache = AccessCacheHandle.Create();
        }

        public static Traverse2 Create(System.Type? type) => new Traverse2(type);

        public static Traverse2 Create<T>() => Traverse2.Create(typeof(T));

        public static Traverse2 Create(object? root) => new Traverse2(root);

        public static Traverse2 CreateWithType(string name) => new Traverse2(AccessTools2.TypeByName(name));

        private Traverse2()
        {
        }

        public Traverse2(System.Type? type) => this._type = type;

        public Traverse2(object? root)
        {
            this._root = root;
            this._type = root?.GetType();
        }

        private Traverse2(object? root, MemberInfo info, object[]? index)
        {
            this._root = root;
            System.Type type = root?.GetType();
            if ((object)type == null)
                type = AccessTools.GetUnderlyingType(info);
            this._type = type;
            this._info = info;
            this._params = index;
        }

        private Traverse2(object? root, MethodInfo method, object[]? parameter)
        {
            this._root = root;
            this._type = method.ReturnType;
            this._method = (MethodBase)method;
            this._params = parameter;
        }

        public object? GetValue()
        {
            FieldInfo info1 = this._info as FieldInfo;
            if ((object)info1 != null)
                return info1.GetValue(this._root);
            PropertyInfo info2 = this._info as PropertyInfo;
            if ((object)info2 != null)
                return info2.GetValue(this._root, AccessTools.all, (Binder)null, this._params, CultureInfo.CurrentCulture);
            MethodBase method = this._method;
            if ((object)method != null)
                return method.Invoke(this._root, this._params);
            return this._root == null && (object)this._type != null ? (object)this._type : this._root;
        }

        public T? GetValue<T>() => this.GetValue() is T obj ? obj : default(T);

        public object? GetValue(params object[] arguments) => this._method?.Invoke(this._root, arguments);

        public T? GetValue<T>(params object[] arguments) => this._method?.Invoke(this._root, arguments) is T obj ? obj : default(T);

        public Traverse2 SetValue(object value)
        {
            if (this._root == null)
                return this;
            (this._info as FieldInfo)?.SetValue(this._root, value, AccessTools.all, (Binder)null, CultureInfo.CurrentCulture);
            (this._info as PropertyInfo)?.SetValue(this._root, value, AccessTools.all, (Binder)null, this._params, CultureInfo.CurrentCulture);
            return this;
        }

        public System.Type? GetValueType()
        {
            FieldInfo info = this._info as FieldInfo;
            if ((object)info != null)
                return info.FieldType;
            return (this._info as PropertyInfo)?.PropertyType;
        }

        private Traverse2 Resolve()
        {
            if (this._root == null)
            {
                FieldInfo info1 = this._info as FieldInfo;
                if ((object)info1 != null && info1.IsStatic)
                    return new Traverse2(this.GetValue());
                PropertyInfo info2 = this._info as PropertyInfo;
                if ((object)info2 != null && info2.GetGetMethod().IsStatic)
                    return new Traverse2(this.GetValue());
                MethodBase method = this._method;
                if ((object)method != null && method.IsStatic)
                    return new Traverse2(this.GetValue());
                if ((object)this._type != null)
                    return this;
            }
            return new Traverse2(this.GetValue());
        }

        public Traverse2 Type(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new Traverse2();
            if ((object)this._type == null)
                return new Traverse2();
            System.Type type = AccessTools.Inner(this._type, name);
            return (object)type == null ? new Traverse2() : new Traverse2(type);
        }

        public Traverse2 Field(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new Traverse2();
            Traverse2 traverse2 = this.Resolve();
            if ((object)traverse2._type == null)
                return new Traverse2();
            ref readonly AccessCacheHandle? local = ref Traverse2.Cache;
            FieldInfo info = local.HasValue ? local.GetValueOrDefault().GetFieldInfo(traverse2._type, name) : (FieldInfo)null;
            if ((object)info == null)
                return new Traverse2();
            return !info.IsStatic && traverse2._root == null ? new Traverse2() : new Traverse2(traverse2._root, (MemberInfo)info, (object[])null);
        }

        public Traverse2<T> Field<T>(string name) => new Traverse2<T>(this.Field(name));

        public List<string> Fields() => AccessTools.GetFieldNames(this.Resolve()._type);

        public Traverse2 Property(string name, object[]? index = null)
        {
            if (string.IsNullOrEmpty(name))
                return new Traverse2();
            Traverse2 traverse2 = this.Resolve();
            if ((object)traverse2._type == null)
                return new Traverse2();
            ref readonly AccessCacheHandle? local = ref Traverse2.Cache;
            PropertyInfo info = local.HasValue ? local.GetValueOrDefault().GetPropertyInfo(traverse2._type, name) : (PropertyInfo)null;
            return (object)info == null ? new Traverse2() : new Traverse2(traverse2._root, (MemberInfo)info, index);
        }

        public Traverse2<T> Property<T>(string name, object[]? index = null) => new Traverse2<T>(this.Property(name, index));

        public List<string> Properties() => AccessTools.GetPropertyNames(this.Resolve()._type);

        public Traverse2 Method(string name, params object[] arguments)
        {
            if (string.IsNullOrEmpty(name))
                return new Traverse2();
            Traverse2 traverse2 = this.Resolve();
            if ((object)traverse2._type == null)
                return new Traverse2();
            System.Type[] types = AccessTools.GetTypes(arguments);
            ref readonly AccessCacheHandle? local = ref Traverse2.Cache;
            MethodInfo method = (local.HasValue ? local.GetValueOrDefault().GetMethodInfo(traverse2._type, name, types) : (MethodBase)null) as MethodInfo;
            return (object)method == null ? new Traverse2() : new Traverse2(traverse2._root, method, arguments);
        }

        public Traverse2 Method(string name, System.Type[] paramTypes, object[]? arguments = null)
        {
            if (string.IsNullOrEmpty(name))
                return new Traverse2();
            Traverse2 traverse2 = this.Resolve();
            if ((object)traverse2._type == null)
                return new Traverse2();
            ref readonly AccessCacheHandle? local = ref Traverse2.Cache;
            MethodInfo method = (local.HasValue ? local.GetValueOrDefault().GetMethodInfo(traverse2._type, name, paramTypes) : (MethodBase)null) as MethodInfo;
            return (object)method == null ? new Traverse2() : new Traverse2(traverse2._root, method, arguments);
        }

        public List<string> Methods() => AccessTools.GetMethodNames(this.Resolve()._type);

        public bool FieldExists() => this._info is FieldInfo;

        public bool PropertyExists() => this._info is PropertyInfo;

        public bool MethodExists() => (object)this._method != null;

        public bool TypeExists() => (object)this._type != null;

        public static void IterateFields(object source, Action<Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            AccessTools.GetFieldNames(source).ForEach((Action<string>)(f => action(sourceTrv.Field(f))));
        }

        public static void IterateFields(
          object source,
          object target,
          Action<Traverse2, Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            Traverse2 targetTrv = Traverse2.Create(target);
            AccessTools.GetFieldNames(source).ForEach((Action<string>)(f => action(sourceTrv.Field(f), targetTrv.Field(f))));
        }

        public static void IterateFields(
          object source,
          object target,
          Action<string, Traverse2, Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            Traverse2 targetTrv = Traverse2.Create(target);
            AccessTools.GetFieldNames(source).ForEach((Action<string>)(f => action(f, sourceTrv.Field(f), targetTrv.Field(f))));
        }

        public static void IterateProperties(object source, Action<Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            AccessTools.GetPropertyNames(source).ForEach((Action<string>)(f => action(sourceTrv.Property(f))));
        }

        public static void IterateProperties(
          object source,
          object target,
          Action<Traverse2, Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            Traverse2 targetTrv = Traverse2.Create(target);
            AccessTools.GetPropertyNames(source).ForEach((Action<string>)(f => action(sourceTrv.Property(f), targetTrv.Property(f))));
        }

        public static void IterateProperties(
          object source,
          object target,
          Action<string, Traverse2, Traverse2> action)
        {
            if (action == null)
                return;
            Traverse2 sourceTrv = Traverse2.Create(source);
            Traverse2 targetTrv = Traverse2.Create(target);
            AccessTools.GetPropertyNames(source).ForEach((Action<string>)(f => action(f, sourceTrv.Property(f), targetTrv.Property(f))));
        }

        public override string? ToString() => ((object)this._method ?? this.GetValue())?.ToString();
    }
}
