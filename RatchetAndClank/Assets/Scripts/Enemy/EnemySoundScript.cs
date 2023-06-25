using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundScript : MonoBehaviour
{
    [Header("Enemy SOUNDS")]
    [Tooltip("Sound when enemy gets hit.")]
    public AudioSource _damageSound;

    public void PlayDamageSound()
    {
        _damageSound.Play();
    }

    [Tooltip("Sound when enemy attacks.")]
    public AudioSource _attackSound;

    public void PlayAttackSound()
    {
        _attackSound.Play();
    }

    [Tooltip("Sound when enemy dies.")]
    public AudioSource _deathSound;

    public void PlayDeathSound()
    {
        _deathSound.Play();
    }
}
