using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    bool isSpawnAlreadySet;
    SpawnManager spawnManager;
    SoundController soundController;
    [SerializeField] public Transform spawnCoordinates;
    [SerializeField] ParticleSystem spawnParticles;
    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSpawnAlreadySet)
            return;
        AlreadySet(true);
        spawnManager.UpdatePointNumber(this);
        spawnManager.SaveSpawnpointState(this);
        soundController.Play("Success");
        spawnParticles.Play();
    }

    public void AlreadySet(bool state)
    {
        isSpawnAlreadySet = state;
    }
}
