using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSpeedRunTrigger : MonoBehaviour
{
    [SerializeField] SpeedRunLevelController speedRunLevelController;

    private void OnTriggerEnter(Collider other)
    {
        speedRunLevelController.EndSpeedRun();
        gameObject.SetActive(false);
    }
}
