using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JumpScareTrigger : MonoBehaviour
{
    [SerializeField] private bool doOnce = true;
    [SerializeField] private GameObject jumpScareObject;
    [SerializeField] private AudioClip jumpScareSound;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private VolumeProfile darkProfile;
    VolumeProfile oldProfile;
    bool hasTriggered = false;
    private void Start()
    {
        oldProfile = globalVolume.profile;
        jumpScareObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (doOnce && hasTriggered) return;

        if (other.GetComponent<Player>()  != null)
        {
            jumpScareObject.SetActive(true);
            AudioSource.PlayClipAtPoint(jumpScareSound, jumpScareObject.transform.position);
            StartCoroutine(SetObjectActiveAfterTime(1f, jumpScareObject, false));
            StartCoroutine(SetProfileAfterTime(0, darkProfile));
            StartCoroutine(SetProfileAfterTime(0.1f, oldProfile));
            StartCoroutine(SetProfileAfterTime(0.2f, darkProfile));
            StartCoroutine(SetProfileAfterTime(0.3f, oldProfile));
            StartCoroutine(SetProfileAfterTime(0.4f, darkProfile));
            StartCoroutine(SetProfileAfterTime(0.5f, oldProfile));
            StartCoroutine(SetProfileAfterTime(0.6f, darkProfile));
            StartCoroutine(SetProfileAfterTime(0.8f, oldProfile));
            StartCoroutine(SetProfileAfterTime(0.9f, darkProfile));
            StartCoroutine(SetProfileAfterTime(1f, oldProfile));
            hasTriggered = true;
        }
    }

    IEnumerator SetObjectActiveAfterTime(float time, GameObject gameObject, bool active)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(active);
    }

    IEnumerator SetProfileAfterTime(float time, VolumeProfile volumeProfile)
    {
        yield return new WaitForSeconds(time);
        globalVolume.profile = volumeProfile;
    }
}
