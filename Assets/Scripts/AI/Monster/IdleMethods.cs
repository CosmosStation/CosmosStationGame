    using UnityEngine;

namespace AI.Monster
{
    public class IdleMethods : MonoBehaviour
    {
        private float idleTime = 3f;
        private float idleTimer;

        void Start()
        {
            idleTimer = idleTime;
        }

        public void updateTimer()
        {
            idleTimer -= Time.deltaTime;
        }

        public bool checkIfEnded()
        {
            if (idleTimer <= 0f)
            {
                idleTimer = idleTime;
                return true;
            } 
            else return false;
        }
    }
}
