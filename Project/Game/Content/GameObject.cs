using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class GameObject
    {
        public int drawLayer = 0;
        public bool IsActive = true;

        public virtual void Destroy()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
