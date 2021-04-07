using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using SFML.Graphics;
namespace Project
{
    public class GameTime
    {
        private float deltaTime = 0f;
        private float timeScale = 1f;

        public float TimeScale
        {
            get => timeScale;
            set => timeScale = value;
        }
        public float DeltaTime
        {
            get => deltaTime * timeScale;
            set => deltaTime = value;
        }
        public float DeltaTimeUnscaled
        {
            get => deltaTime;
        }
        public float TotalAmountOfTime
        {
            get;
            private set;
        }
        public void Update(float deltaTime, float totalTimeElapsed)
        {
            this.deltaTime = deltaTime;
            TotalAmountOfTime = totalTimeElapsed;
        }
    }
}