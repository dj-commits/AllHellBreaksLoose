using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{

    public TextMeshPro textField;

    private float moveAmount;
    private Vector3 finalTextPos;

    UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        moveAmount = uiManager.GetYMoveSpeed();
        finalTextPos = transform.position * moveAmount;
        StartCoroutine(DamagedAnimTimer());
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(transform.position != finalTextPos)
        {
            MoveText(uiManager.GetYMoveSpeed(), finalTextPos);

        }
        
    }


    IEnumerator DamagedAnimTimer()
    {
        yield return new WaitForSeconds(uiManager.GetLifeTimer());
        Destroy(gameObject);
    }

    public void MoveText(float moveAmount, Vector3 finalTextPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, finalTextPos, moveAmount * Time.deltaTime);
    }

}
