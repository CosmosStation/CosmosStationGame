using UnityEngine;
using UnityEngine.AI;

namespace AI.Monster
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;

        public Transform CurrentPoint => patrolPoints[_currentPoint];

        private int _currentPoint;

        /// <summary>
        /// Gets the next point to patrol to
        /// </summary>
        /// <returns></returns>
        public Transform GetNext()
        {
            var point = patrolPoints[_currentPoint];
            _currentPoint = (_currentPoint + 1) % patrolPoints.Length;
            return point;
        }

        /// <summary>
        /// Checks if destination reached
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public bool HasReached(NavMeshAgent agent)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}