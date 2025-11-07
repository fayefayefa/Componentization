using System;

namespace Componentization
{
    public class Component : IDisposable
    {
        /// <summary>
        /// Component`s Parent
        /// </summary>
        public Component Parent { get; set; }
        
        public long Id { get; internal set; }

        public bool IsDisposed => Id == 0;
        
        public bool IsFromPool { get; internal set; }
        
        public virtual void Dispose()
        {
            Id = 0;
        }
    }
}