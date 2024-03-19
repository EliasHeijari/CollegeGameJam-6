using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    private int health = 100;
    [SerializeField] private VolumeProfile hitEffectVolumeProfile;
    [SerializeField] private Volume globalVolume;
    private VolumeProfile oldVolumeProfile;

    private void Start()
    {
        oldVolumeProfile = globalVolume.profile;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(HitEffectVolume());
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    IEnumerator HitEffectVolume()
    {
        globalVolume.profile = hitEffectVolumeProfile;
        yield return new WaitForSeconds(2.5f);
        globalVolume.profile = oldVolumeProfile;
    }

    private void Die()
    {
        // Dying logic here
        Debug.Log("Player Died");
    }
}
