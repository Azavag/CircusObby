using MenteBacata.ScivoloCharacterControllerDemo;
using UnityEngine;
using UnityEngine.Playables;


public class FinalTimeline : MonoBehaviour
{
    [SerializeField] PlayableDirector[] finalTimelineDirectors;
    [SerializeField] GameObject finalObjects;
    [SerializeField] GameObject playerObject;
    [SerializeField] Transform finalPoint;
    [SerializeField] Transform TropheyTransform;
    [SerializeField] OrbitingCamera orbitingCamera;
    PlayableDirector currentDirector;
    BoxCollider triggerCollider;

    private void Awake()
    {
        finalObjects.SetActive(false);
        triggerCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        currentDirector = finalTimelineDirectors[0];
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (currentDirector == aDirector)
        {
            SwitchTriggerCollider(false);
            playerObject.transform.position = finalPoint.position;
            playerObject.transform.rotation = TropheyTransform.rotation;
            orbitingCamera.SetStartCameraRotation();
            playerObject.SetActive(true);
        }
    }

    public void SwitchTriggerCollider(bool state)
    {
        triggerCollider.enabled = state;
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

    void OnDisable()
    {
        currentDirector.stopped -= OnPlayableDirectorStopped;
    }
}
