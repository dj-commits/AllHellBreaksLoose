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

    public TextMeshPro textField;

    private void Start()
    {
    }

    private void Awake()
    {
        
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
        // for some reason, getting the TMP_Text or TextMeshPro component at runtime results in a NullReferenceException. Setting it to public and in the
        // inspector fixes it. Not sure why.
        if(textField != null)
        {
            textField.SetText(damage.ToString());
        }
        else
        {
            Debug.Log("textMesh is null");
        }

    }

}
