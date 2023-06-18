using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
    public Collectible collectible;

    public int collectibleCount = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Debug.Log("Emit Triggered");
        Emit();
    }

    /*function that when called, 
    creates N new object at in the near vicinity of the emitter
    */
    public void Emit()
    {
        Animator animator = GetComponentInParent<Animator>();
        animator.SetTrigger("IsBroken");
        for (int i = 0; i < collectibleCount; i++)
        {
            Vector3 position = transform.position;
            position.x += Random.Range(-2f, 2f);
            position.z += Random.Range(-2f, 2f);
            Instantiate(collectible, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
