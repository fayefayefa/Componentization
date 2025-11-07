using System;
using System.Collections.Generic;
using Componentization.Utils;

namespace Componentization
{
    public class Entity : Component
    {
        private Dictionary<Type, List<Component>> m_Children = new Dictionary<Type, List<Component>>();

        public override void Dispose()
        {
            base.Dispose();

            foreach (var components in m_Children.Values)
            {
                foreach (var component in components)
                {
                    ComponentFactory.Release(component);
                }
                components.Clear();
            }
            m_Children.Clear();
        }

        /// <summary>
        /// 添加自组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AddComponent<T>(object userData = null) where T : Component
        {
            var component = ComponentFactory.Acquire<T>(this, userData);
            if (!m_Children.ContainsKey(typeof(T)))
            {
                m_Children.Add(typeof(T), new List<Component>() { component });
            }
            else
            {
                m_Children[typeof(T)].Add(component);
            }

            return component;
        }

        /// <summary>
        /// 添加子组件
        /// </summary>
        /// <param name="component"></param>
        /// <exception cref="Exception"></exception>
        public void AddComponent(Component component)
        {
            component.Parent = this;
            if (!m_Children.ContainsKey(component.GetType()))
            {
                m_Children.Add(component.GetType(), new List<Component>() { component });
            }
            else
            {
                if (m_Children[component.GetType()].Contains(component))
                {
                    throw new Exception($"Component {component.GetType()} is already registered");
                }
                m_Children[component.GetType()].Add(component);
            }
        }
        
        /// <summary>
        /// 移除子组件
        /// </summary>
        /// <param name="component"></param>
        /// <param name="isRelease"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveComponent(Component component, bool isRelease = true)
        {
            if (!m_Children.ContainsKey(component.GetType()))
            {
                throw new Exception($"Component {component.GetType()} is not registered");
            }
            else if (!m_Children[component.GetType()].Contains(component))
            {
                throw new Exception($"Component {component.GetType()} is not registered");
            }
            m_Children[component.GetType()].Remove(component);
            component.Parent = null;

            if (isRelease)
            {
                ComponentFactory.Release(component);
            }
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public Component GetComponent(Type componentType)
        {
            if (!m_Children.ContainsKey(componentType))
            {
                return null;
            }
            else if (m_Children[componentType].Count == 0)
            {
                return null;
            }
            else
            {
                return m_Children[componentType][0];
            }
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public Component GetComponent(Type componentType, int index)
        {
            if (!m_Children.ContainsKey(componentType))
            {
                return null;
            }
            else if (m_Children[componentType].Count == 0)
            {
                return null;
            }
            else
            {
                return m_Children[componentType][index];
            }
        }

        /// <summary>
        /// 获取指定类型的第一个子组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            return GetComponent(typeof(T)) as T;
        }

        /// <summary>
        /// 获取指定类型的第index个子组件
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>(int index) where T : Component
        {
            return GetComponent(typeof(T), index) as T;
        }
    }
}