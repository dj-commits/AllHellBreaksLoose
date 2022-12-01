using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private CamManager camManager;
    private bool isLocked;
    private bool playedDoorAudio;
    private Vector3 finalDoorPos;
    [SerializeField]
    float doorOpenYOffset;

    EnemyManager em;

    AudioManager audioManager;

    private Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        camManager = GameObject.Find("CamManager").GetComponent<CamManager>();
        isLocked = true;
        finalDoorPos = new Vector3(transform.position.x, transform.position.y + doorOpenYOffset, transform.position.z);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        doorAnimator = GetComponent<Animator>();
        playedDoorAudio = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (em.GetEnemyCount() <= 0 && isLocked)
        {
            this.isLocked = false;
        }
        
        if (!isLocked)
        {
            OpeningDoor();
        }
    }

    public void OpeningDoor()
    {
        camManager.FocusOnDoor();
        StartCoroutine(CameraTransitionTimer());
    }

    IEnumerator CameraTransitionTimer()
    {
        yield return new WaitForSeconds(2);
        if (!playedDoorAudio) audioManager.Play("doorOpen");
        playedDoorAudio = true;
        doorAnimator.SetBool("isOpening", true);
        StartCoroutine(OpenningDoorTimer());
    }
    IEnumerator OpenningDoorTimer()
    {
        yield return new WaitForSeconds(2);
        camManager.FocusOnPlayer();
        Destroy(gameObject);
    }

    public bool GetDoorLockStatus()
    {
        return isLocked;
    }

    public void SetDoorLockStatus(bool tf)
    {
        
        this.isLocked = tf;
    }
}
