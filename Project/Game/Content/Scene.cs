using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project
{
    public class Scene : GameObject
    {
        public int maxLayer = 8;
        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> removeObjects = new List<GameObject>();
        public override void Draw()
        {
            foreach (var gObject in objects)
                if(gObject.IsActive)
                    gObject.Draw();
            RemoveObjects();
        }
        public void TurnScene(bool active)
        {
            IsActive = active;
            objects.ForEach(x => x.IsActive = IsActive);
        }
        public void Add(GameObject obj)
        {
            if(!objects.Contains(obj))
            {
                objects.Add(obj);
                objects = new List<GameObject>(Sort());
            }
        }
        private void RemoveObjects()
        {
            removeObjects.ForEach(x => objects.Remove(x));
            removeObjects.ForEach(x => x.Destroy());
            removeObjects.Clear();
        }
        public void Destroy<T>() where T : GameObject
        {
            foreach (var gObject in objects)
                if (gObject.GetType() == typeof(T))
                    removeObjects.Add(gObject);
        }
        private IEnumerable<GameObject> Sort()
            => objects.Where(x => !(x is null)).OrderByDescending(x => -x.drawLayer);
        public override void Destroy()
        {
            objects.ForEach(x => x.Destroy());
        }
    }
}
