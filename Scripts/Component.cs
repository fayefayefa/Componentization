using System;

namespace Componentization
{
    public class Component : IDisposable
    {
        /// <summary>
        /// Component`s Parent
        /// </summary>
        public Component Parent { get; internal set; }
        
        public long Id { get; internal set; }

        public bool IsDisposed => Id == 0;

        protected internal virtual void OnAwake(object userData = null)
        {
            
        }

        protected internal virtual void OnEnable(object userData = null)
        {
            
        }

        protected internal virtual void OnDisable(object userData = null)
        {
            
        }

        protected internal virtual void OnDestroy(object userData = null)
        {
            
        }
        
        public virtual void Dispose()
        {
        }
    }
}