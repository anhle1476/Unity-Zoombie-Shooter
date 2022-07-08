using Script.Player.Fighting;
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

        [SerializeField]
        private float turnSpeed = 5f;
        
        private NavMeshAgent _navMeshAgent;
        private float _distanceToTarget;

        private Animator _animator;
        private static readonly int Move = Animator.StringToHash("Move");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Attack = Animator.StringToHash("Attack");

        public Transform Target => target;

        public bool IsProvoked { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            if (!target)
            {
                target = FindObjectOfType<PlayerHealth>().transform;
            }
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
                IsProvoked = true;
            }

            if (IsProvoked)
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
            else
            {
                StopAttackTarget();
            }
        }

        
        private void AttackTarget()
        {
            _animator.SetBool(Attack, true);
            // manually update the rotation since we don't set the target for navMeshAgent
            Vector3 targetDirection = (target.position - transform.position).normalized;
            Quaternion facingTargetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, facingTargetRotation, Time.deltaTime * turnSpeed);
        }
        
        private void StopAttackTarget()
        {
            _animator.SetBool(Attack, false);
        }

        private void ChaseTarget()
        {
            _animator.SetTrigger(Move);
            _navMeshAgent.SetDestination(target.position);
        }
    }
}
