using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private AudioClip EnemyChaseStartsClip;
    [SerializeField] private AudioSource enemyAudioSource;

    private void Start()
    {
        enemyHandler = GetComponent<EnemyHandler>();
        enemyHandler.OnChaseStart += EnemyHandler_OnChaseStart;
    }

    private void EnemyHandler_OnChaseStart(object sender, System.EventArgs e)
    {
        if (!enemyAudioSource.isPlaying)
        {
            if (EnemyChaseStartsClip != null)
            {
                enemyAudioSource.clip = EnemyChaseStartsClip;
                enemyAudioSource.Play();
            }
        }
    }
}
