using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    SpawnManager spawnManager;
    SoundController soundController;
    SimpleCharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        soundController = FindObjectOfType<SoundController>();       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            characterController = FindObjectOfType<SimpleCharacterController>();
            soundController.Play("Death");
            characterController.isDead = true;
            StartCoroutine(spawnManager.DeathProccess());
        }
    }
}
