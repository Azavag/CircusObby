using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    bool isNormalSpawnAlreadySet;
    bool isSpeedrunSpawnAlreadySet;
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
        if (isNormalSpawnAlreadySet && spawnManager.GetGamemodeType() == Gamemode.normal)
            return;
        if (isSpeedrunSpawnAlreadySet && spawnManager.GetGamemodeType() == Gamemode.speedrun)
            return;
        AlreadySet(spawnManager.GetGamemodeType(), true);
        spawnManager.UpdatePointNumber(this);
        if(spawnManager.GetGamemodeType() == Gamemode.normal)
        {
            spawnManager.SaveSpawnpointState(this);
        }        
        soundController.Play("Success");
        spawnParticles.Play();
    }

    public void AlreadySet(Gamemode mode, bool state)
    {
        if(mode == Gamemode.normal)
            isNormalSpawnAlreadySet = state;
        else isSpeedrunSpawnAlreadySet = state;
    }

 
}
