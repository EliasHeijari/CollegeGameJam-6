using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    string interactText = "Open/Close";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        if (interactorTransform.GetComponent<Player>().hasMasterKey)
        {
            animator.SetTrigger("OpenClose");
        }
        else
        {
            interactText = "You Need The Master Key";
        }
    }
}
