using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyAudioClipsSO : ScriptableObject
{
    public AudioClip[] enemyChaseClips = new AudioClip[3];
    public AudioClip[] enemyAttackClips = new AudioClip[3];
    public AudioClip[] enemyPatrolClips = new AudioClip[3];
}
