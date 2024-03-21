using EvolveGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    private void Start()
    {
        playableDirector.stopped += PlayableDirector_stopped;
    }

    private void PlayableDirector_stopped(PlayableDirector obj)
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().enabled = false;
            playableDirector.Play();
        }
    }
}
