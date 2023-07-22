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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void Emit()
    {
        gameObject.GetComponentInChildren<ObjectEmitter>().Emit();
    }

    public void ExplodeBox()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Explode_Box");
    }

    public void ExplodeNearbyBoxes()
    {
        Debug.Log("ExplodeNearbyBoxes");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider collider in colliders)
        {
            GameObject OtherGameObject = collider.gameObject;
            if (
                OtherGameObject.CompareTag("Box") &&
                OtherGameObject.GetComponent<BoxScript>().boxType ==
                BoxTypeEnum.Explosive
            )
            {
                OtherGameObject.GetComponent<BoxScript>().ExplodeBox();
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
