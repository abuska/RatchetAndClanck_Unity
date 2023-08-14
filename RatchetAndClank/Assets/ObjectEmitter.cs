using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEmitter : MonoBehaviour
{
    public Collectible collectible;

    public int collectibleCount = 10;

    public bool canBeTriggered = true;

    public bool destroyAfterEmit = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    /*function that when called, 
    creates N new object at in the near vicinity of the emitter
    */
    public void Emit()
    {
        Animator animator = GetComponentInParent<Animator>();

        for (int i = 0; i < collectibleCount; i++)
        {
            Vector3 position = transform.position;
            position.x += Random.Range(-2f, 2f);
            position.z += Random.Range(-2f, 2f);
            Instantiate(collectible, position, Quaternion.identity);
        }

        //wait for animation to finish
        //destroy parent after emitting
        if (destroyAfterEmit)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canBeTriggered)
        {
            if (!other.gameObject.CompareTag("Weapon")) return;
            Emit();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
