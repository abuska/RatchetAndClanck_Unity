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
        //spherecheck for player collision
        Collider[] hitColliders =
            Physics.OverlapSphere(transform.position, 1f, 1 << 6);
    }
}
