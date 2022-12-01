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
    public TextMeshPro textField;
    [SerializeField]
    GameObject damageText;
    [SerializeField]
    [Range(.1f, 3f)]
    float yMovement;

    public bool gameOverMenuIsActive;
    public bool victoryIsActive;
    private GameObject gameOverMenu;
    private GameObject victoryMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu = GameObject.Find("GameOver");
        gameOverMenu.SetActive(false);
        gameOverMenuIsActive = false;
        victoryMenu = GameObject.Find("VictoryMenu");
        victoryMenu.SetActive(false);
        victoryIsActive = false;
    }

    // Update is called once per frame
    void Update()
    { 

    }



    public void CreateDamageText(Vector3 dmgTextSpawn, float damage)
    {
        float dmgTextSpawnXOffset = Random.Range(.001f, .5f);
        float dmgTextSpawnYOffset = Random.Range(.001f, .7f);
        dmgTextSpawn = new Vector3(dmgTextSpawn.x + dmgTextSpawnXOffset, dmgTextSpawn.y + dmgTextSpawnYOffset, dmgTextSpawn.z);
        textField.SetText(damage.ToString());
        GameObject dmgTextClone = Instantiate(damageText, dmgTextSpawn, Quaternion.identity);
    }

    public void ActivateGameOverMenu()
    {
        if (victoryIsActive == false)
        {
            gameOverMenu.SetActive(true);
            gameOverMenuIsActive = true;
            Cursor.visible = true;
        }

    }

    public void ActivateVictoryScreen()
    { 
        victoryMenu.SetActive(true);
        victoryIsActive = true;
        Cursor.visible = true;
    }

    public float GetYMoveSpeed()
    {
        return yMovement;
    }

    public float GetLifeTimer()
    {
        return lifeTimer;
    }
}
