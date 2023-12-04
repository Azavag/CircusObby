using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacterModel : MonoBehaviour
{
    [SerializeField] GameObject[] characterModels;
    [SerializeField] SimpleCharacterController simpleCharacterController;

    public void MakeCurrentCharacterModelActive()
    {
        foreach (GameObject characterModel in characterModels)
            characterModel.SetActive(false);
        characterModels[Progress.Instance.playerInfo.choosenCharacterNumber].SetActive(true);
        simpleCharacterController.SetCurrentCharacterAnimator();
    }

}
