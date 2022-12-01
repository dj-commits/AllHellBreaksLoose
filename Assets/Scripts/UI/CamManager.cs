using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera mainCam;
    [SerializeField]
    CinemachineVirtualCamera bossCam;
    [SerializeField]
    float cameraScrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("CMvCam").GetComponent<CinemachineVirtualCamera>();
        bossCam = GameObject.Find("CMBossDoor").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(Vector3 origin, Vector3 destination)
    {

    }

    public void FocusOnDoor()
    {
        bossCam.MoveToTopOfPrioritySubqueue();
    }

    public void FocusOnPlayer()
    {
        mainCam.MoveToTopOfPrioritySubqueue();
    }
}
