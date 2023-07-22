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
                gameObject.GetComponent<Animator>().SetTrigger("Explode_Box");
                break;
            case BoxTypeEnum.Normal:
            default:
                if (!other.gameObject.CompareTag("Weapon")) return;
                Emit();
                break;
        }
    }
}
