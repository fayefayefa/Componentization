using System;
using System.Collections.Concurrent;

namespace Componentization.Runtime.Utils
{
    public static class ComponentFactory
    {
        private static ConcurrentDictionary<Type, ComponentPool> m_ComponentPools =
            new ConcurrentDictionary<Type, ComponentPool>();

        private static ComponentPool InternalGetPool(Type type)
        {
            if (!m_ComponentPools.TryGetValue(type, out var pool))
            {
                pool = new ComponentPool();
                m_ComponentPools[type] = pool;
            }
            return pool;
        }

        public static Component Acquire(Type type, object userdata = null)
        {
            var pool = InternalGetPool(type);
            return pool.Acquire(userdata);
        }

        public static Component Acquire(Type type, Component parent, object userdata)
        {
            var pool = InternalGetPool(type);
            return pool.Acquire(parent, userdata);
        }

        public static T Acquire<T>(object userdata = null) where T : Component
        {
            var pool = InternalGetPool(typeof(T));
            return pool.Acquire(userdata) as T;
        }

        public static T Acquire<T>(Component parent, object userdata = null) where T : Component
        {
            var pool = InternalGetPool(typeof(T));
            return pool.Acquire(parent, userdata) as T;
        }

        public static void Release(Component component)
        {
            var pool = InternalGetPool(component.GetType());
            pool.Release(component);
        }
    }
}