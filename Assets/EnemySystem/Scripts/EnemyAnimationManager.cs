using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHandler enemyHandler;

    private const string SPEED_PARAM = "Speed";

    [SerializeField] private string[] attackParamTriggers = new string[3];

    private void Start() {
        enemyHandler.attackHandler.OnAttack += AttackHandler_OnAttack;
        enemyHandler.GetComponent<Enemy>().OnEnemyDie += EnemyAnimationManager_OnEnemyDie;
    }

    private void EnemyAnimationManager_OnEnemyDie(object sender, System.EventArgs e)
    {
        transform.position -= Vector3.up;
        animator.SetTrigger("Dying");
        enemyHandler.GetComponent<Enemy>().OnEnemyDie -= EnemyAnimationManager_OnEnemyDie;
    }

    private void AttackHandler_OnAttack(object sender, System.EventArgs e)
    {
        int triggerIndex = Random.Range(0, attackParamTriggers.Length);

        animator.SetTrigger(attackParamTriggers[triggerIndex]);
    }

    private void Update()
    {
        animator.SetFloat(SPEED_PARAM, enemyHandler.movementHandler.Speed);
    }
}
