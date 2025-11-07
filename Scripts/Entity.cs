using System;
using System.Collections.Concurrent;

namespace Componentization
{
    public class Entity : Component
    {
        private ConcurrentDictionary<Type, Component> m_Children = new ConcurrentDictionary<Type, Component>();
    }
}