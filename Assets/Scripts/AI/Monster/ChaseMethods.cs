using UnityEngine;
using UnityEngine.AI;

namespace AI.Monster
{
    public class ChaseMethods : MonoBehaviour
    {
        public Transform Monster;

        [SerializeField] private LayerMask _ignoreMask;

        private Ray _ray;

        public bool CheckIfPlayerInSight(Transform Player)
        {
            if (Player == null)
                return false;

            _ray = new Ray(Monster.position, Player.position - Monster.transform.position);

            if (Vector3.Dot(_ray.direction, Monster.transform.forward) < 0)
                return false;

            if (!Physics.Raycast(_ray, out var hit, 200, ~_ignoreMask))
            {
                return false;
            }

            if (hit.collider.tag == "Player")
            {
                return true;
            }

            return false;
        }

        public void chasePlayer(Transform Player, NavMeshAgent navMeshAgent)
        {
            navMeshAgent.SetDestination(Player.position);
        }

        public void stopChase(NavMeshAgent navMeshAgent)
        {
            navMeshAgent.SetDestination(Monster.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_ray.origin, _ray.origin + _ray.direction * 100);
        }
    }
}
