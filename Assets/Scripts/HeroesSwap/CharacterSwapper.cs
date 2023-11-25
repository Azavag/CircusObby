using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CharacterSwapper : MonoBehaviour
{
    [SerializeField] Transform charactersTransform;
    [SerializeField] CharacterStand[] characters;
    [SerializeField] int currentCharacterNumber;
    Transform currentCharacterTransform;
    [SerializeField] TextMeshProUGUI characterNameText;
    [Header("Анимация перехода")]
    [SerializeField] float slideTime;
    [SerializeField] float scaleMultiplier;
    bool canSwap;
    [Header("Кнопки")]
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    bool isOnRightBorder;
    bool isOnLeftBorder;
    void Start()
    {
        characters = new CharacterStand[charactersTransform.childCount];
        int counter = 0;
        foreach (Transform character in charactersTransform)
        {
            characters[counter] = character.GetComponent<CharacterStand>();
            counter++;
        }
        MoveCharactersPool(0);
        ScaleCurrentCharacter(0);
        SwapButtonsVisibility();
    }

    public void LeftHeroSlide()
    {
        if (CheckNumberOnMinBorder(currentCharacterNumber) || !canSwap)
            return;

        canSwap = false;
        characterNameText.text = "";
        UnscaleCurrentCharacter();
        currentCharacterNumber--;
        MoveCharactersPool(slideTime);        
        ScaleCurrentCharacter(slideTime);
        SwapButtonsVisibility();
    }
    public void RightHeroSlide()
    {
        if (CheckNumberOnMaxBorder(currentCharacterNumber) || !canSwap)
            return;

        canSwap = false;
        characterNameText.text = "";
        UnscaleCurrentCharacter();
        currentCharacterNumber++;
        MoveCharactersPool(slideTime);
        ScaleCurrentCharacter(slideTime);
        SwapButtonsVisibility();
    }

    void SwapButtonsVisibility()
    {
        if (isOnLeftBorder)
        {
            SwitchSliderButton(leftButton, true);
            isOnLeftBorder = false;
        }
        if (isOnRightBorder)
        {
            SwitchSliderButton(rightButton, true);
            isOnRightBorder = false;
        }
        if (CheckNumberOnMaxBorder(currentCharacterNumber))
        {
            isOnRightBorder = true;
            SwitchSliderButton(rightButton, false);
        }
        if (CheckNumberOnMinBorder(currentCharacterNumber))
        {
            isOnLeftBorder = true;
            SwitchSliderButton(leftButton, false);
        }
    }

    void MoveCharactersPool(float animTime)
    {
        charactersTransform.
            LeanMove(new Vector3(-characters[currentCharacterNumber].xPosition, 0, 0), animTime);
    }
    void ScaleCurrentCharacter(float animTime)
    {
        currentCharacterTransform = characters[currentCharacterNumber].transform;
        currentCharacterTransform.
            LeanScale(scaleMultiplier * currentCharacterTransform.localScale, animTime).setOnComplete(OnSlideEnd);
    }

    void OnSlideEnd()
    {
        characterNameText.text = characters[currentCharacterNumber].name;
        canSwap = true;
    }
    void UnscaleCurrentCharacter()
    {
        currentCharacterTransform.
            LeanScale(1 / scaleMultiplier * currentCharacterTransform.localScale, slideTime);
    }
    bool CheckNumberOnMaxBorder(int arrayNumber)
    {
        return arrayNumber == characters.Length - 1;
    }
    bool CheckNumberOnMinBorder(int arrayNumber)
    {
        return arrayNumber == 0;
    }
    void SwitchSliderButton(GameObject buttonObject, bool state)
    {
        buttonObject.SetActive(state);
    }
}
