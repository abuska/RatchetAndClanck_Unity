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

    private AudioSource audioSource;

    private bool isAudioPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAudioPlaying)
        {
            // Sphere check for player collision, within pull distance move towards player,
            // within pickup distance, add to player inventory
            Collider[] hitColliders =
                Physics.OverlapSphere(transform.position, pullDistance);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Player"))
                {
                    // If within pickup distance, add to player inventory
                    //TODO FIX DISTANCE
                    if (
                        Vector3
                            .Distance(transform.position,
                            hitCollider.transform.position) <
                        2
                    )
                    {
                        // CollectibleType.PickUp();
                        if (audioSource != null)
                        {
                            GameObject
                                .FindGameObjectWithTag("Player")
                                .GetComponent<PlayerStats>()
                                .IncreaseBolts(1);
                            GameObject
                                .FindGameObjectWithTag("Player")
                                .GetComponent<PlayerStats>()
                                .IncreasePlayerExperience(1);
                            GetComponent<MeshRenderer>().enabled = false;

                            PlayAudio();
                            return; // Exit the method to wait for audio to finish playing
                        }
                    }
                }
            }
        }
    }

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
            isAudioPlaying = true;
            StartCoroutine(WaitForAudio());
        }
    }

    private IEnumerator WaitForAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        isAudioPlaying = false;
        Destroy (gameObject);
    }
}
