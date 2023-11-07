using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int spawnPointNumber = 0;        //++
    public int moneyCount = 0;              //++
    public float musicVolume = 1;           //++
    public float effectsVolume = 1;         //++
    public int runsCount = 1;               //++
    public bool[] areCoinsCollect = new bool[68];       //++
    public bool[] colorsPantsBuyState = new bool[9];    //++
    public bool[] colorsShirtBuyState = new bool[9];    //++
    public bool[] specialsBuyState = new bool[7];       //++
    public string choosedPantsColor;        //++
    public string choosedShirtColor;        //++
    public string choosedSpecialColor;      //++
    public bool isNavButtonActive;          //++
    public bool[] areSpawnpointsSet = new bool[51];
}


public class Progress : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public static Progress Instance;
    [SerializeField] YandexSDK yandexSDK;
   
    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Debug.Log("Progress");
            yandexSDK.Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       
    }

}



