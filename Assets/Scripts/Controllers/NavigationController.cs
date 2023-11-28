using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject ingameCanvas;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject alertCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera shopCamera;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    GameObject prevPageObject;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] AdvManager advManager;
    [SerializeField] InputGame inputGame;
    [SerializeField] GameObject levelsNavAlert;
    [SerializeField] GameObject levelsNavPanel;
    bool isPause;
    bool isShop;
    bool isSettings;
    bool isGame;

    [SerializeField] SoundController soundController;
    [SerializeField] SwapCharacterModel swapCharacter;

    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        settingsCanvas = soundController.transform.GetChild(0).gameObject;
    }
    void Start()
    {
        startCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        alertCanvas.SetActive(false);
        shopCanvas.SetActive(isShop);
        settingsCanvas.SetActive(false);
        EnableCharacterControl(isGame);
        deathMenu.SetActive(isGame);
        pauseMenu.SetActive(isPause);
        pauseButton.SetActive(!isPause);
        levelsNavAlert.SetActive(false);
        levelsNavPanel.SetActive(false);
        shopCamera.gameObject.SetActive(isShop);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (!isPause)
            {
                soundController.MakeClickSound();
                ShowPauseMenu();
            }
        }
    }
    public void ShowGame()
    {
        isGame = !isGame;
        if (isGame)
            spawnManager.RespawnPlayer();

        inputGame.ShowCursorState(!isGame);
        swapCharacter.MakeCurrentCharacterModelActive();
        EnableCharacterControl(isGame);
        isPause = false;
        pauseMenu.SetActive(isPause);
        pauseButton.SetActive(isGame);
        deathMenu.SetActive(false);
        startCanvas.SetActive(!isGame);
        ingameCanvas.SetActive(isGame);
    }

    public void ShowGamemodeChoosing()
    {

    }
    public void ShowPauseMenu()
    {
        isPause = !isPause;       
        pauseMenu.SetActive(isPause);
        pauseButton.SetActive(!isPause);
        
        if (!deathMenu.activeSelf)
        {
            EnableCharacterControl(!isPause);
            inputGame.ShowCursorState(isPause);
        }    
    }
    public void EnableCharacterControl(bool state)
    {
        mainCamera.GetComponent<OrbitingCamera>().enabled = state;
        playerObject.GetComponent<SimpleCharacterController>().enabled = state;
    }
    public void ShowShopMenu() 
    {      
        isShop = !isShop;     
        shopCamera.gameObject.SetActive(isShop);
        shopCanvas.SetActive(isShop);
        prevPageObject.SetActive(!isShop);
    }

    public void ShowCharactersSwapScene()
    {
        SceneManager.LoadScene("SwapHeroesScene");
    }

    public void ShowSettingMenu()
    {
        isSettings = !isSettings;
        settingsCanvas.SetActive(isSettings);
       
    }
    public void SetPrevPage(GameObject objectToHide)
    {
        prevPageObject = objectToHide;
    }

    public void ShowLevelsNavHint(bool state)
    {
        levelsNavAlert.SetActive(state);
        EnableCharacterControl(!state);
        inputGame.ShowCursorState(state);
    }
}
