using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public enum BoxTypeEnum
    {
        Normal,
        Explosive,
        Ammo,
        Health
    }

    public BoxTypeEnum boxType;

    public float explosionRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponentInChildren<ParticleSystem>() != null)
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }



    /*void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 3f);
    }*/
    private void DestroyBox()
    {
        Destroy (gameObject);
    }

    private void EnambleParticleSystem()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void PlayAudioSource(AudioClip audioClip)
    {
        gameObject.GetComponentInChildren<AudioSource>().clip = audioClip;
        gameObject.GetComponentInChildren<AudioSource>().Play();
    }

    private void Emit()
    {
        gameObject.GetComponentInChildren<ObjectEmitter>().Emit();
    }

    public void ExplodeBox()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Explode_Box");
    }

    public void DamagePlayer()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider collider in colliders)
        {
            GameObject OtherGameObject = collider.gameObject;
            if (OtherGameObject.CompareTag("Player"))
            {
                OtherGameObject.GetComponent<PlayerStats>().DecreaseHealth(1);
            }
        }
    }

    public void ExplodeNearbyBoxes()
    {
        Collider[] colliders =
            Physics.OverlapSphere(transform.position, explosionRange);

        foreach (Collider collider in colliders)
        {
            GameObject OtherGameObject = collider.gameObject;
            if (OtherGameObject.CompareTag("Box"))
            {
                if (
                    OtherGameObject.GetComponent<BoxScript>().boxType ==
                    BoxTypeEnum.Explosive
                )
                {
                    OtherGameObject.GetComponent<BoxScript>().ExplodeBox();
                }
                else
                {
                    OtherGameObject.GetComponent<BoxScript>().Emit();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (boxType)
        {
            case BoxTypeEnum.Explosive:
                if (
                    !other.gameObject.CompareTag("Weapon") &&
                    !other.gameObject.CompareTag("Player")
                ) return;

                //todo damage player/enemies in animation
                ExplodeBox();

                //todo play explosion sound
                //todo add chain reaction
                break;
            case BoxTypeEnum.Normal:
            default:
                if (!other.gameObject.CompareTag("Weapon")) return;
                Emit();
                break;
        }
    }
}
