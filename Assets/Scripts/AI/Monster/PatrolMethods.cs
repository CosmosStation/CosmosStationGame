using UnityEngine;
using UnityEngine.AI;

namespace AI.Monster
{
    public class PatrolMethods : MonoBehaviour
    {
        [SerializeField] private PatrolPoints patrolPoints;
        [SerializeField] private NavMeshAgent navMeshAgent;

        /// <summary>
        /// Moves the agent to next target
        /// </summary>
        /// <returns></returns>
        public void MoveToNextTarget()
        {
            Transform point = patrolPoints.GetNext();
            navMeshAgent.SetDestination(point.position);
        }

        public bool HasReached()
        {
            return patrolPoints.HasReached(navMeshAgent);
        }
    }
}