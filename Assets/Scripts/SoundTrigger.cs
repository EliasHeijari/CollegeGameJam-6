using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private Transform soundTransform;
    [SerializeField] private List<AudioClip> soundClips = new List<AudioClip>();
    [SerializeField] private bool doOnce = true;

    bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && !hasPlayed && soundClips.Count > 0)
        {
            int randomIndex = Random.Range(0, soundClips.Count);
            AudioClip randomClip = soundClips[randomIndex];
            AudioSource.PlayClipAtPoint(randomClip, soundTransform.position);
            hasPlayed = true;
            if (!doOnce) hasPlayed = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawIcon(soundTransform.position, "SpeakerIcon", true);
    }
}
