using UnityEngine;
using UnityEngine.AI;

namespace Script
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField]
        [Tooltip("Range to start chasing the target without provoke")]
        private float chaseRange = 15f;
    
        private NavMeshAgent _navMeshAgent;
        private bool _isProvoked;
        private float _distanceToTarget;

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }

        // Update is called once per frame
        void Update()
        {
            _distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (_distanceToTarget <= chaseRange)
            {
                _isProvoked = true;
            }

            if (_isProvoked)
            {
                EngageTarget();
            }
        }

        private void EngageTarget()
        {
            if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
            {
                ChaseTarget();
            }

            if (_distanceToTarget <= _navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
        }

        private void AttackTarget()
        {
            Debug.Log(name +  " Attack " + target.name);
        }

        private void ChaseTarget()
        {
            _navMeshAgent.SetDestination(target.position);
        }
    }
}
