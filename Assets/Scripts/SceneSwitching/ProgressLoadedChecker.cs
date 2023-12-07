using UnityEngine;

public class ProgressLoadedChecker : MonoBehaviour
{
    public static ProgressLoadedChecker instance;
    public bool isProgressLoaded;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    private void Start()
    {
#if UNITY_EDITOR
        isProgressLoaded = true;
#endif
    }
    //â jslib
    public void ConfirmProgressLoaded()
    {
        isProgressLoaded = true;
    }
}
