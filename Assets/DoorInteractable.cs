using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public string GetInteractText()
    {
        return "Open Door";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("You Opened " + gameObject.GetType().Name);
    }
}
