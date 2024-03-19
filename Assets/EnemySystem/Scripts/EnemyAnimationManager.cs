using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHandler enemyHandler;

    private const string SPEED_PARAM = "Speed";

    [SerializeField] private string[] attackParamTriggers = new string[3];

    private void Start() {
        enemyHandler.attackHandler.OnAttack += AttackHandler_OnAttack;
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
