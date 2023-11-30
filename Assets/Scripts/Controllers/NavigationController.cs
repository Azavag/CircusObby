using MenteBacata.ScivoloCharacterControllerDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    [Header("Канвасы")]
    [SerializeField] GameObject ingameCanvas;
    [SerializeField] GameObject shopCanvas;
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject alertCanvas;
    [SerializeField] GameObject settingsCanvas;
    [Header("Панели в главном меню")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject gamemmodesPanel;

    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject pauseButton;
    GameObject prevPageObject;
    [SerializeField] SpawnManager spawnManager;
    [SerializeField] AdvManager advManager;
    [SerializeField] InputGame inputGame;
    [SerializeField] GameObject levelsNavAlert;     //Удалить?
    bool isPause;
    bool isSettings;
    bool isGame;

    [SerializeField] SoundController soundController;
    [SerializeField] SwapCharacterModel swapCharacter;
    ChoosingGamemode choosingGamemode;
    [SerializeField] SpeedRunLevelController speedRunLevelController;
    LevelLoadAnimator levelLoadAnimator;
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
        settingsCanvas = soundController.transform.GetChild(0).gameObject;
        choosingGamemode = GetComponent<ChoosingGamemode>();
        levelLoadAnimator = FindObjectOfType<LevelLoadAnimator>();
    }
    void Start()
    {
        startCanvas.SetActive(true);
        ingameCanvas.SetActive(false);
        alertCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        EnableCharacterControl(isGame);
        deathMenu.SetActive(isGame);
        pauseMenu.SetActive(isPause);
        pauseButton.SetActive(!isPause);
        levelsNavAlert.SetActive(false);
        gamemmodesPanel.SetActive(false);
    }

    
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

    public void StartNormalGamemode()
    {
        ToggleMenu_Ingame();
        spawnManager.SetGamemodeType(Gamemode.normal);
        speedRunLevelController.SetupSpeedRun(false);
        spawnManager.ResetSpeedrunSpawnpoints();
        spawnManager.RespawnPlayer();
    }
    public void StartSpeedrunGamemode()
    {
        ToggleMenu_Ingame();
        spawnManager.SetGamemodeType(Gamemode.speedrun);
        spawnManager.ResetLastSpawnPoint();
        spawnManager.ResetSpeedrunSpawnpoints();
        speedRunLevelController.SetupSpeedRun(true);
        spawnManager.RespawnPlayer();
    }

    public void ToggleMenu_Ingame()
    {
        isGame = !isGame;
        inputGame.ShowCursorState(!isGame);
        swapCharacter.MakeCurrentCharacterModelActive();
        EnableCharacterControl(isGame);
        isPause = false;
        pauseMenu.SetActive(isPause);
        pauseButton.SetActive(isGame);
        deathMenu.SetActive(false);
        startCanvas.SetActive(!isGame);
        ingameCanvas.SetActive(isGame);
        soundController.MakeClickSound();
    }  
    public void SwapChooseGamemodeToMainPanel()
    {
        soundController.MakeClickSound();
        gamemmodesPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    
    public void SwapMainToChooseGamemode()
    {
        soundController.MakeClickSound();
        mainPanel.SetActive(false);
        gamemmodesPanel.SetActive(true);
        choosingGamemode.ChangeStartGameText();
        choosingGamemode.ChangeBestTimeText();
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

    public void ShowCharactersSwapScene()
    {
        soundController.MakeClickSound();
        levelLoadAnimator.LoadNewScene("SwapHeroesScene");
    }

    public void ShowSettingMenu()
    {
        soundController.MakeClickSound();
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
