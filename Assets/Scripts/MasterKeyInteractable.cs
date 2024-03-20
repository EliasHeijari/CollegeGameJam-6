using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterKeyInteractable : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "Get Master Key";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        interactorTransform.GetComponent<Player>().hasMasterKey = true;
        Destroy(gameObject);
    }
}
