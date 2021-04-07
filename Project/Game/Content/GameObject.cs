using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class GameObject : IDrawable, IDisposable
    {
        public int drawLayer = 0;
        public bool IsActive = true;

        public virtual void Dispose()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
