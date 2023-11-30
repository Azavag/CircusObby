using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector finalTimeline;
    [SerializeField] GameObject finalObjects;
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject playerCameraObject;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartTimeline();
        }
    }

    void StartTimeline()
    {
        playerObject.SetActive(false);
        playerCameraObject.SetActive(false);
        finalObjects.SetActive(true);
        finalTimeline.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            StartTimeline();
    }
}
