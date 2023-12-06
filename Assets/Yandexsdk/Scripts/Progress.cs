using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int spawnPointNumber;        //++
    public float musicVolume;          //++
    public float effectsVolume;        //++
    public bool[] areSpawnpointsSet = new bool[51];
    public int choosenCharacterNumber = 2;
    public int bestTimeSpeedrunMiliseconds = -1;
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
            yandexSDK.Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}



