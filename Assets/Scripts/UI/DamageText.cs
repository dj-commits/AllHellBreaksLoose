using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    float lifeTimer;

    [SerializeField]
    float moveYSpeed;

    private bool started;

    public TextMeshPro textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position * 2, lifeTimer * Time.deltaTime);
        StartCoroutine(DamagedAnimTimer());
    }

    IEnumerator DamagedAnimTimer()
    {
        yield return new WaitForSeconds(lifeTimer);
        Destroy(gameObject);
    }

    public void SetText(float damage)
    {
        if(textMesh != null)
        {
            Debug.Log("textMesh ain't null!");
            textMesh.SetText(damage.ToString());
        }
        else
        {
            Debug.Log("textMesh is TOTALLY null");
        }

    }

    public void CreateDamageText(float damage)
    {

    }
}
