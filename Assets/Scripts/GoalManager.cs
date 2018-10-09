using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {

    public const float height = 1;

    public float range;
    public AudioClip coinSFX;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        audioSource.PlayOneShot(coinSFX);
        transform.position = RandomLocation();
    }

    private Vector3 RandomLocation() {
        return new Vector3(Random.Range(-range, range), height, Random.Range(-range, range));
    }
}
