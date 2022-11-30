using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // DamageText vars
    [SerializeField]
    float lifeTimer;
    [SerializeField]
    float moveYSpeed;
    public TextMeshPro textField;
    [SerializeField]
    GameObject damageText;
    [SerializeField]
    [Range(.1f, 3f)]
    float yMovement;
    bool isCreated;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isCreated)
        {
            MoveText(damageText, yMovement);
            isCreated = false;
        }

    }

    IEnumerator DamagedAnimTimer(GameObject go)
    {
        yield return new WaitForSeconds(lifeTimer);
        Destroy(go);
    }

    public void MoveText(GameObject go, float moveAmount)
    {
        go.transform.position = Vector3.MoveTowards(go.transform.position, go.transform.position * moveAmount, lifeTimer * Time.deltaTime);
        StartCoroutine(DamagedAnimTimer(go));
    }

    public void CreateDamageText(Vector3 dmgTextSpawn, float damage)
    {
        float dmgTextSpawnXOffset = Random.Range(.001f, .5f);
        float dmgTextSpawnYOffset = Random.Range(.001f, .7f);
        dmgTextSpawn = new Vector3(dmgTextSpawn.x + dmgTextSpawnXOffset, dmgTextSpawn.y + dmgTextSpawnYOffset, dmgTextSpawn.z);
        textField.SetText(damage.ToString());
        Instantiate(damageText, dmgTextSpawn, Quaternion.identity);
        isCreated = true;

    }
}
