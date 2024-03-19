using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private Transform soundTransform;
    [SerializeField] private AudioClip soundClip;
    [SerializeField] private bool doOnce = true;

    bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !hasPlayed)
        {
            AudioSource.PlayClipAtPoint(soundClip, soundTransform.position);
            Debug.Log("Sound played");
            hasPlayed = true;
            if (!doOnce) hasPlayed = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawCube(soundTransform.position, Vector3.one);
    }
}
