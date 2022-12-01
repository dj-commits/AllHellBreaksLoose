using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private bool isLocked;
    private bool isOpen;
    private Vector3 finalDoorPos;
    [SerializeField]
    float doorOpenYOffset;

    EnemyManager em;

    AudioManager audioManager;

    private Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
        isOpen = false;
        finalDoorPos = new Vector3(transform.position.x, transform.position.y + doorOpenYOffset, transform.position.z);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        em = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        doorAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (em.GetEnemyCount() <= 0 && !isOpen && isLocked)
        {
            audioManager.Play("doorOpen");
            this.isLocked = false;
        }
        
        if (!isLocked && !isOpen)
        {
            OpenningDoor();
        }
        if(transform.position == finalDoorPos && !isOpen)
        {
            isOpen = true;
        }
    }

    public void OpenningDoor()
    {
        doorAnimator.SetBool("isOpenning", true);
        StartCoroutine(OpenningDoorTimer());
    }

    IEnumerator OpenningDoorTimer()
    {
        yield return new WaitForSeconds(2);
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

    public bool GetDoorOpenStatus()
    {
        return isOpen;
    }

    public void SetDoorOpenStatus(bool tf)
    {
        this.isOpen = tf;
    }
}
