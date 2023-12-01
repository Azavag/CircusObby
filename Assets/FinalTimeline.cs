using MenteBacata.ScivoloCharacterControllerDemo;
using UnityEngine;
using UnityEngine.Playables;


public class FinalTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector[] finalTimelineDirectors;
    PlayableDirector currentDirector;
    [SerializeField] GameObject finalObjects;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform finalPoint;
    [SerializeField] Transform TropheyTransform;
    [SerializeField] OrbitingCamera orbitingCamera;

    private void Awake()
    {
        finalObjects.SetActive(false);
        
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (currentDirector == aDirector)
        {
            playerObject.transform.position = finalPoint.position;
            playerObject.transform.rotation = TropheyTransform.rotation;
            orbitingCamera.SetStartCameraRotation();
            playerObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerObject.SetActive(false);
            StartTimeline(Progress.Instance.playerInfo.choosenCharacterNumber);
        }
    }

    void StartTimeline(int timelineNumber)
    {
        currentDirector = finalTimelineDirectors[timelineNumber];
        currentDirector.stopped += OnPlayableDirectorStopped;
        currentDirector.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
            StartTimeline(Progress.Instance.playerInfo.choosenCharacterNumber);
    }

    void OnDisable()
    {
        currentDirector.stopped -= OnPlayableDirectorStopped;
    }
}
