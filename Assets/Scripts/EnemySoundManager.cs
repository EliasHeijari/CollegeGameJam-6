using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private AudioClip EnemyChaseStartsClip;
    [SerializeField] private EnemyAudioClipsSO enemyAudioClips;
    [SerializeField] private AudioSource enemyAudioSource;

    [SerializeField] float chaseAudioTimerMin = 3f;
    [SerializeField] float chaseAudioTimerMax = 8f;
    float chaseAudioTimer;
    AudioClip oldAudioClip;

    private void Start()
    {
        enemyHandler = GetComponent<EnemyHandler>();
        enemyHandler.OnChaseStart += EnemyHandler_OnChaseStart;

        chaseAudioTimer = chaseAudioTimerMin;
    }

    private void Update()
    {
        switch (enemyHandler.enemyState)
        {
            case EnemyHandler.State.Chase:
                UpdateChaseAudio();
                break;
        }
    }

    private void UpdateChaseAudio()
    {
        chaseAudioTimer -= Time.deltaTime;
        if (chaseAudioTimer < 0)
        {
            chaseAudioTimer = Random.Range(chaseAudioTimerMin, chaseAudioTimerMax);
            int enemyRandomChaseClipsIndex = Random.Range(0, enemyAudioClips.enemyChaseClips.Length);

            AudioClip audioChaseClip = enemyAudioClips.enemyChaseClips[enemyRandomChaseClipsIndex];

            while (oldAudioClip == audioChaseClip)
            {
                enemyRandomChaseClipsIndex = Random.Range(0, enemyAudioClips.enemyChaseClips.Length);

                audioChaseClip = enemyAudioClips.enemyChaseClips[enemyRandomChaseClipsIndex];
            }

            enemyAudioSource.clip = audioChaseClip;
            enemyAudioSource.Play();

            oldAudioClip = audioChaseClip;
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
