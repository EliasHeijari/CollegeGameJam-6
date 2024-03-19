using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private AudioClip EnemyChaseStartsClip;
    private AudioSource enemyAudioSource;

    private void Start()
    {
        enemyHandler = GetComponent<EnemyHandler>();
        enemyHandler.OnChaseStart += EnemyHandler_OnChaseStart;
        enemyAudioSource = GetComponent<AudioSource>();
        if (enemyAudioSource == null)
        {
            enemyAudioSource = transform.AddComponent<AudioSource>();
        }
        else
        {
            enemyAudioSource.clip = EnemyChaseStartsClip;
        }
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
