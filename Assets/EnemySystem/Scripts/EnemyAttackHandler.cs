using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackHandler : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] private float timeBetweenAttacks = 3f;
    [SerializeField] private float stoppingRadius = 1f;
    [SerializeField] private EnemyHandler enemyHandler;
    float attackTimer;

    private float rotateSpeed = 5f;
    
    public event EventHandler OnAttack;

    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHandler = GetComponent<EnemyHandler>();
    }
    private void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0) attackTimer = 0;
    }
    public void Attack(Transform target){

        if (Physics.CheckSphere(transform.position + enemyHandler.attackRangeOffset, stoppingRadius, enemyHandler.playerLayerMask))
        {
            // Stop moving
            navMeshAgent.speed = 0;
        }

        RotateTowardsTarget(target);
        
        if (attackTimer <= 0)
        {
            attackTimer = timeBetweenAttacks;

            navMeshAgent.speed = 0;
            // Sends OnAttack event and enemyAnimator will subscibe
            OnAttack?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void RotateTowardsTarget(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // Look Towards The Target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + enemyHandler.attackRangeOffset, stoppingRadius);
    }

}
