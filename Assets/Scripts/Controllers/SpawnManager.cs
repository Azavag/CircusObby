using MenteBacata.ScivoloCharacterControllerDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<SpawnPoint> spawnPointsList;
    [SerializeField] List<SpawnPoint> speedRunSpawnPointsList;
    int lastNormalSpawnPoint;                   //��������� � �����������
    int lastSpeedrunSpawnpoint;                 //��������� � ��������
    Gamemode gamemodeType;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject cameraObject;
    [SerializeField] AdvManager advManager;
    [SerializeField] InputGame inputGame;
    [SerializeField] SimpleCharacterController characterController;
    [SerializeField] NavigationController navigationController;
    [SerializeField] GameObject deathAlert;
    [SerializeField] SpeedRunLevelController speedRunLevelController;
    Animator deathAlertanimator;
    SoundController soundController;
    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();
    }
    private void Start()
    {
        lastNormalSpawnPoint = Progress.Instance.playerInfo.spawnPointNumber;
        
        for(int i = 0; i < spawnPointsList.Count; i++)
        {
            spawnPointsList[i].AlreadySet(Gamemode.normal, Progress.Instance.playerInfo.areSpawnpointsSet[i]);
        }

        deathAlertanimator = deathAlert.GetComponent<Animator>();
    }

    public void UpdatePointNumber(SpawnPoint point)
    {
        if (gamemodeType == Gamemode.normal)
            lastNormalSpawnPoint = spawnPointsList.IndexOf(point);
        else lastSpeedrunSpawnpoint = spawnPointsList.IndexOf(point);


        if (lastNormalSpawnPoint == spawnPointsList.Count - 1)
        {
            StartCoroutine(EndGameRoutine());
        }
    }
    IEnumerator EndGameRoutine()
    {
        soundController.Play("WinGame");
        yield return new WaitForSeconds(1.2f);
        navigationController.ShowEndGamePanel(true);
        ResetLastSpawnPoint();
        ResetNormalSpawnpoints();
    }
    public void UpdatePointNumber(int pointNumber)
    {
        lastNormalSpawnPoint = pointNumber;
    }
    //�� ������ ������������
    public void RespawnPlayer()
    {
        if (gamemodeType == Gamemode.normal)
            playerObject.transform.position = spawnPointsList[lastNormalSpawnPoint].spawnCoordinates.position;
        else playerObject.transform.position = speedRunSpawnPointsList[lastSpeedrunSpawnpoint].spawnCoordinates.position;

        soundController.MakeClickSound();
        speedRunLevelController.ToggleSpeedRunTimer(false);
        characterController.ResetPlayerState();
        deathMenu.SetActive(false);
        deathAlert.SetActive(false);
        playerObject.SetActive(true);
        inputGame.ShowCursorState(false);
        cameraObject.GetComponent<OrbitingCamera>().enabled = true;
        StartCoroutine(DeathProccess());
    }
    public void ResetLastSpawnPoint()
    {
        if (gamemodeType == Gamemode.normal)
        {
            lastNormalSpawnPoint = 0;
            Progress.Instance.playerInfo.spawnPointNumber = 0;
            YandexSDK.Save();
        }

        else lastSpeedrunSpawnpoint = 0;
    }

    public void BlockInput()
    {
        inputGame.ShowCursorState(true);
        playerObject.SetActive(false);
        cameraObject.GetComponent<OrbitingCamera>().enabled = false;       
    }
    public IEnumerator DeathProccess()
    {
        yield return new WaitUntil(() => characterController.isDead);
        speedRunLevelController.ToggleSpeedRunTimer(true);
        BlockInput();
        soundController.Play("Death");
        deathAlert.SetActive(true);
        deathAlertanimator.SetBool("isDeath", true);
        yield return new WaitForSeconds(1.4f);
        advManager.ShowAdv();        
        yield return new WaitForSeconds(0.2f);
        deathMenu.SetActive(true);
    }
    public void SaveSpawnpointState(SpawnPoint point)
    {
        int tempNumber = spawnPointsList.IndexOf(point);
        Progress.Instance.playerInfo.spawnPointNumber = tempNumber;
        Progress.Instance.playerInfo.areSpawnpointsSet[tempNumber] = true;
        YandexSDK.Save();
    }

    public void ResetNormalSpawnpoints()
    {
        foreach(SpawnPoint point in spawnPointsList) 
        {
            point.AlreadySet(Gamemode.normal, false);           
        }
        for (int i = 0; i < Progress.Instance.playerInfo.areSpawnpointsSet.Length; i++)
            Progress.Instance.playerInfo.areSpawnpointsSet[i] = false;
        YandexSDK.Save();
    }
    public void ResetSpeedrunSpawnpoints()
    {
        foreach (SpawnPoint point in speedRunSpawnPointsList)
        {
            point.AlreadySet(Gamemode.speedrun, false);
        }
    }

    public void SetGamemodeType(Gamemode gamemodeType)
    {
        this.gamemodeType = gamemodeType;
    }
    public Gamemode GetGamemodeType()
    {
        return gamemodeType;
    }
}
