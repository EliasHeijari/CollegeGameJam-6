using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public string GetInteractText()
    {
        return "Open/Close";
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransform)
    {
        animator.SetTrigger("OpenClose");
    }

}
