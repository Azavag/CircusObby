using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartReciever : MonoBehaviour
{
    static public LevelStartReciever instance;
    public bool canRunLevel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    
}
