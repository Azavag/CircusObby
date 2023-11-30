using MenteBacata.ScivoloCharacterControllerDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<SpawnPoint> spawnPointsList;
    [SerializeField] List<SpawnPoint> speedRunSpawnPointsList;
    int lastNormalSpawnPoint;       //Чекпоинты в прохождении
    int lastSpeedrunSpawnpoint;     //ЧекпоинтЫ в спидране
    Gamemode gamemodeType;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject cameraObject;
    [SerializeField] AdvManager advManager;
    [SerializeField] InputGame inputGame;
    [SerializeField] SimpleCharacterController characterController;
    [SerializeField] HardLevelsNavigation levelsNavigation;
    [SerializeField] NavigationController navigationController;
    [SerializeField] GameObject deathAlert;
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
            //Кат-сцена

            //navigationController.ShowLevelsNavHint(true);
        }
        //Progress.Instance.playerInfo.spawnPointNumber = lastNormalSpawnPoint;
    }
    public void UpdatePointNumber(int pointNumber)
    {
        lastNormalSpawnPoint = pointNumber;
    }
 
    //По кнопке перерождения
    public void RespawnPlayer()
    {
        if (gamemodeType == Gamemode.normal)
            playerObject.transform.position = spawnPointsList[lastNormalSpawnPoint].spawnCoordinates.position;
        else playerObject.transform.position = speedRunSpawnPointsList[lastSpeedrunSpawnpoint].spawnCoordinates.position;
       
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
            lastNormalSpawnPoint = 0;
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
        BlockInput();
        soundController.Play("Death");
        deathAlert.SetActive(true);
        deathAlertanimator.SetBool("isDeath", true);
        yield return new WaitForSeconds(1.6f);
        deathMenu.SetActive(true);
        advManager.ShowAdv();        
    }
    public void SaveSpawnpointState(SpawnPoint point)
    {
        int tempNumber = spawnPointsList.IndexOf(point);
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
