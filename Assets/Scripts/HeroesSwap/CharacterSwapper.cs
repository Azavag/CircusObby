using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Button choosingButton;
    bool isOnRightBorder;
    bool isOnLeftBorder;
    [SerializeField] GameObject choosenCharacterLight;
    void Start()
    {
        characters = new CharacterStand[charactersTransform.childCount];
        int counter = 0;
        foreach (Transform character in charactersTransform)
        {
            characters[counter] = character.GetComponent<CharacterStand>();
            counter++;
        }
        if(Progress.Instance != null)
            currentCharacterNumber = Progress.Instance.playerInfo.choosenCharacterNumber;
        SetChoosedStateToCharacter();
        MoveCharactersPool(0);
        ScaleCurrentCharacter(0);
        ChangeSlideButtonsVisibility();
    }

    public void LeftHeroSlide()
    {
        if (CheckNumberOnMinBorder(currentCharacterNumber) || !canSwap)
            return;
        SwipeOut();
        currentCharacterNumber--;
        SwipeIn();
    }
    public void RightHeroSlide()
    {
        if (CheckNumberOnMaxBorder(currentCharacterNumber) || !canSwap)
            return;
        SwipeOut();
        currentCharacterNumber++;
        SwipeIn();  
    }
    bool SwipeOut()
    {     
        canSwap = false;
        characterNameText.text = "";
        choosenCharacterLight.SetActive(false);
        UnscaleCurrentCharacter();
        return true;
    }
    void SwipeIn()
    {
        CheckChoosingButtons();
        MoveCharactersPool(slideTime);
        ScaleCurrentCharacter(slideTime);
        ChangeSlideButtonsVisibility();
    }
    void ChangeSlideButtonsVisibility()
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
        characterNameText.text = characters[currentCharacterNumber].characterName;
        LightOn();
        canSwap = true;
    }
    public void LightOn()
    {
        if (isCurrentCharacterChoosed())
            choosenCharacterLight.SetActive(true);
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
    public bool isCurrentCharacterChoosed()
    {
        return characters[currentCharacterNumber].isChoosed;
    }
    public void SetChoosedStateToCharacter()
    {
        foreach (var character in characters)
            character.isChoosed = false;
        characters[currentCharacterNumber].isChoosed = true;
        LightOn();
        PlayChoosingAnimation();
        CheckChoosingButtons();
    }

    void PlayChoosingAnimation()
    {
        characters[currentCharacterNumber].animator.SetTrigger("choosed");
    }

    public int GetCurrentCharacterNumber()
    {
        return currentCharacterNumber;
    }
    void CheckChoosingButtons()
    {
        if (characters[currentCharacterNumber].isChoosed)
            choosingButton.gameObject.SetActive(false);
        else
        {
            choosingButton.gameObject.SetActive(true);
            choosingButton.GetComponentInChildren<TextMeshProUGUI>().text = "Выбрать";
        }
    }
}
