using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private Animation animationClipJump;
    private Animation animationClipIdle;
    public bool isJumping = false;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    void LateUpdate()
    {
        CharacterAnimate();
    }

    private void CharacterAnimate()
    {
        animator.SetBool("Jump", isJumping);
        isJumping = false;
    }
}
