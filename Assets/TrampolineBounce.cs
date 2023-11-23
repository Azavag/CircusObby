using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBounce : MonoBehaviour
{
    float bouncePower = 200f;
    SoundController soundController;
    void Start()
    {
        soundController = FindObjectOfType<SoundController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundController.Play("Spring");
            other.gameObject.GetComponent<Rigidbody>().AddForce(bouncePower * Vector3.up, ForceMode.Impulse);
        }

    }
}
