using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFunctions : MonoBehaviour
{
    public float rotateSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //roate the object around the y axis every frame
        transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
    }
}
