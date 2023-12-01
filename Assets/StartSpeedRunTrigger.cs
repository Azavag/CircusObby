using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpeedRunTrigger : MonoBehaviour
{
    [SerializeField] SpeedRunLevelController speedRunLevelController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            speedRunLevelController.StartSpeedRun();
            gameObject.SetActive(false);
        }       
    }
}
