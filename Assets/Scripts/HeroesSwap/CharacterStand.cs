using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStand : MonoBehaviour
{
    public string characterName;
    public float xPosition;
    public bool isChoosed;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}
