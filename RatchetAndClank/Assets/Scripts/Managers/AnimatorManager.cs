using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is used to manage the animator
// This script is attached to the enemy
// editor variables
// +    anim: the animator component
// +    rb: the rigidbody component
// +    soundScript: the sound script component
public class AnimatorManager : MonoBehaviour
{
    public Animator anim;

    public Rigidbody rb;

    protected EnemySoundScript soundScript;
   
    // get sound script and rigidbody
    void Awake()
    {
        anim = GetComponent<Animator>();
        soundScript = GetComponent<EnemySoundScript>();
        rb = GetComponent<Rigidbody>();
    }

    // play the target animation
    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    public void SetWalk(bool isWalking)
    {
        anim.SetBool("_walk", isWalking);
    }

    public void PlayAttack()
    {
        SetWalk(false);
        anim.SetTrigger("_attack");
        soundScript.PlayAttackSound();  
    }

    public void PlayDamage()
    {
        SetWalk(false);
        anim.SetTrigger("_damage");
        soundScript.PlayDamageSound();
    }

    public void PlayDeath()
    {
        SetWalk(false);
        anim.SetTrigger("_die");
    }
}
