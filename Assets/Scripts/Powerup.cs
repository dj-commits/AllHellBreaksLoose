using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private int bulletLayer;
    private int enemyLayer;

    [SerializeField]
    float powerupTime;

    [SerializeField]
    public string powerUpType;

    [SerializeField]
    protected float chanceToDrop;

    // Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Camera cam;
    EnemyLogic enemyLogic;
    PlayerController playerController;

    public bool pickedUp;
    public bool isActive;
    public float lifeTimer;

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
       
        if (this.pickedUp == false)
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0)
            {
                DespawnPower();
            }
        }
    }

    public virtual void ActivatePower()
    {
        this.isActive = true;
        
        
    }

    public virtual void PickupPower()
    {
        spriteRenderer.enabled = false;
    }

    public virtual void DeactivatePower()
    {
        this.isActive = false;
    }

    public virtual void DespawnPower()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickupPower();
        }
    }

    public PlayerController getPlayerController()
    {
        return playerController;
    }

    public float getPowerupTime()
    {
        return powerupTime;
    }

    public void setPowerupTime(float powerupTime)
    {
        this.powerupTime = powerupTime;
    }

    public IEnumerator setPowerupTimer()
    {
        yield return new WaitForSeconds(powerupTime);
        DeactivatePower();
        Destroy(gameObject);
    }

    public float getChanceToDrop()
    {
        return chanceToDrop;
    }
}
