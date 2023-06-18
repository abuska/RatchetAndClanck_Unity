using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int pullDistance = 10;

    public float pullSpeed = 10;

    public float rotationSpeed = 10;

    public float maxSpeed = 10;

    public CollectibleType CollectibleType;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //spherecheck for player collision, within pull distance move towards player,
        //within pickup distance, add to player inventory
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position, pullDistance);
        foreach (var hitCollider in hitColliders)
        if (hitCollider.gameObject.CompareTag("Player"))
        {
            //if within pickup distance, add to player inventory
            if (
                Vector3
                    .Distance(transform.position,
                    hitCollider.transform.position) <
                1
            )
            {
                //CollectibleType.PickUp();
                Destroy (gameObject);
            }
        }
    }
}
