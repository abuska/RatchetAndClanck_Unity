using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    private void Awake()
    {
        anim = GetComponent<Animator>();
        soundScript = GetComponent<EnemySoundScript>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;

        if (
            !GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .GetIsGamePause() &&
            !GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .GetIsGameOver()
        )
        {
            if (delta != 0)
            {
                rb.drag = 0;

                Vector3 deltaPosition = anim.deltaPosition;
                deltaPosition.y = 0;
                Vector3 velocity = deltaPosition / delta;
                rb.velocity = velocity;
            }
        }
    }
}
