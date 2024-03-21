using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    [SerializeField] private GameObject noteGameObject;

    private void Start()
    {
        noteGameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            noteGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent <Player>() != null)
        {
            noteGameObject.SetActive(false);
        }
    }
}
