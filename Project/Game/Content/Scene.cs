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
        public void DestroyObjects<T>() where T : GameObject
        {
            foreach (var gObject in objects)
                if (gObject.GetType() == typeof(T))
                    removeObjects.Add(gObject);
        }
        public void DestroyObject(GameObject obj)
        {
            foreach (var gObject in objects)
                if (gObject == obj)
                    removeObjects.Add(gObject);
        }
        public T Find<T>() where T : GameObject
        {
            foreach (GameObject gObject in objects)
                if (gObject.GetType() == typeof(T))
                    return (T)gObject;
            return null;
        }
        public List<T> FindAll<T>() where T : GameObject
        {
            List<T> needfulObject = new List<T>();
            foreach (GameObject gObject in objects)
                if (gObject.GetType() == typeof(T))
                    needfulObject.Add((T)gObject);
            return needfulObject;
        }
        private IEnumerable<GameObject> Sort()
            => objects.Where(x => !(x is null)).OrderByDescending(x => -x.drawLayer);
        public override void Destroy()
        {
            objects.ForEach(x => x.Destroy());
        }
    }
}
