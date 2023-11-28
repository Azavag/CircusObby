using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBox : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform playerBody;
    [SerializeField] Vector3 scaleDown = new Vector3 (1.2f, 0.8f, 1.2f);
    [SerializeField] Vector3 scaleUp = new Vector3(0.8f, 1.2f, 0.8f);
    [SerializeField] Vector3 startScale;
    [SerializeField] float scaleKoeficcient;
    [SerializeField] float rotationKoeficcient;
    void Start()
    {
        startScale = playerBody.localScale;
        transform.position = playerBody.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePosition = playerTransform.InverseTransformPoint(transform.position);
        float intepolant = relativePosition.y * scaleKoeficcient;
        Vector3 scale = Lerp3(scaleDown, startScale, scaleUp, intepolant);
        playerBody.localScale = scale;
        playerBody.localEulerAngles = new Vector3(relativePosition.z, 0, -relativePosition.x) * rotationKoeficcient;
    }

    Vector3 Lerp3(Vector3 a, Vector3 b, Vector3 c, float time)
    {
        if(time < 0) 
            return Vector3.LerpUnclamped(a, b, time + 1f);
        else
            return Vector3.LerpUnclamped(b, c, time);
    }
}
