using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Link : MonoBehaviour
{  
    //Получение домена
    [DllImport("__Internal")]
    private static extern string GetDomainExtern();

    public static Link Instance;
    public string currentDomain;
    string link = "";
    SoundController soundController;
    private void Awake()
    {      
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        soundController = FindObjectOfType<SoundController>();
    }

    //По кнопке
    public void GotoDeveloperPage()
    {
#if !UNITY_EDITOR
            currentDomain = GetDomainExtern();
#endif
        soundController.MakeClickSound();
        link = string.Format("https://yandex.{0}/games/developer?name=DemiGames", currentDomain);
        Application.OpenURL(link);
    }
}
