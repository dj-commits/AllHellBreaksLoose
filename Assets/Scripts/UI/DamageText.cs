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

    private TextMeshPro textMesh;

    private void Start()
    {
    }

    private void Update()
    {
        if (!started) {
            StartCoroutine(DamagedAnimTimer());
            started = true;
        }
        else
        {
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        }
    }

    IEnumerator DamagedAnimTimer()
    {
        yield return new WaitForSeconds(lifeTimer);
        Destroy(gameObject);
    }

    public void setText(float damage)
    {
        this.textMesh.SetText(damage.ToString());
    }
}
