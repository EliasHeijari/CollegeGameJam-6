using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public bool hasMasterKey { get; set; } = false;
    [SerializeField] private VolumeProfile hitEffectVolumeProfile;
    [SerializeField] private Volume globalVolume;
    [SerializeField] private Image healthBar;
    private VolumeProfile oldVolumeProfile;
    [SerializeField] private GameObject dyingCanvas;

    private void Start()
    {
        health = maxHealth;
        oldVolumeProfile = globalVolume.profile;

        healthBar.fillAmount = (float)health / maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = (float)health / maxHealth;
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
        LoadCurrentSceneAfterTime(2f);
    }

    IEnumerator LoadCurrentSceneAfterTime(float time)
    {
        dyingCanvas.SetActive(true);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
