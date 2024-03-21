using EvolveGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{

    private enum State
    {
        idle,
        walking,
        running,
        jumping,
        goingUp,
        falling,
        landing,
    }


    [SerializeField] private PlayerController playerController;
    [SerializeField] private Animator animator;
    [Header("Animator parameters")]
    private string isWalking = "IsWalking";
    private string isRunning = "IsRunning";
    private string isFalling = "Falling";
    private string isGoingUp = "GoingUp";
    private string isLanding = "Landing";
    private string isJump = "Jump";
    private string isAiming = "IsAiming";

    State state;

    bool playerIsJumping;
    bool playerIsFalling;

    private void Start()
    {
        playerController.OnJump += Player_OnJump;
    }

    private void Player_OnJump(object sender, EventArgs e)
    {
        playerIsJumping = true;
    }

    private void OnDisable()
    {
        playerController.OnJump -= Player_OnJump;
    }

    private void Update()
    {
        UpdatePlayerState();
        SetAnimatorValues();
    }

    private void SetAnimatorValues()
    {

        ResetAnimParameters();

        switch (state)
        {
            case State.idle:
                break;

            case State.walking:
                animator.SetBool(isWalking, true);
                animator.SetFloat("X", playerController.horizontal);
                animator.SetFloat("Y", playerController.vertical);
                break;

            case State.running:
                animator.SetBool(isRunning, true);
                break;

            case State.jumping:
                animator.SetBool(isJump, true);
                break;

            case State.goingUp:
                animator.SetBool(isGoingUp, true);
                break;

            case State.falling:
                animator.SetBool(isFalling, true);
                break;

            case State.landing:
                animator.SetBool(isLanding, true);
                break;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool(isAiming, true);
        }
        else if (animator.GetBool(isAiming))
        {
            animator.SetBool(isAiming, false);
        }
    }

    private void ResetAnimParameters()
    {
        animator.SetBool(isWalking, false);
        animator.SetBool(isRunning, false);
        animator.SetBool(isGoingUp, false);
        animator.SetBool(isFalling, false);
        animator.SetBool(isLanding, false);
        animator.SetBool(isJump, false);
    }

    private void UpdatePlayerState()
    {
        state = State.idle;
        if (playerController.IsWalking && playerController.isGrounded)
        {
            state = State.walking;
        }
        else if (playerController.isRunning && playerController.isGrounded)
        {
            state = State.running;
        }

        if (playerController.moveDirection.y > 0.1f)
        {
            state = State.goingUp;
        }

        if (playerController.moveDirection.y < 0.1f && !playerController.isGrounded)
        {
            state = State.falling;
            playerIsFalling = true;
        }

        if (playerIsFalling && playerController.isGrounded)
        {
            playerIsFalling = false;
            state = State.landing;
        }

        if (playerIsJumping)
        {
            playerIsJumping = false;
            state = State.jumping;
        }

    }
}
