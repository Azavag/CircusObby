using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject controllers, canvases, sdk, level, player, setup;
    [SerializeField] bool inEditor;
    private void Start()
    {
        sdk.SetActive(true);
        if(inEditor) 
        { LoadGame(); }
    }
    public void LoadGame()
    {
        Debug.Log("LoadGame");
        setup.SetActive(true);
        //sdk.SetActive(true);
        controllers.SetActive(true);
        canvases.SetActive(true);
        level.SetActive(true);
        player.SetActive(true);
    }
}
