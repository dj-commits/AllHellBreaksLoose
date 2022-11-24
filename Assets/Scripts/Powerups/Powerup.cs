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
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider2D;
    protected CircleCollider2D circleCollider2D;
    protected SpriteRenderer spriteRenderer;
    protected Camera cam;
    protected PlayerController playerController;

    public bool isPickedUp;
    public bool isActive;
    public float lifeTimer;

    // Start is called before the first frame update
    public virtual void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        circleCollider2D = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (this.isPickedUp == false)
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
        this.getPlayerController().setPowerup(null);
        StartCoroutine(setPowerupTimer());
    }

    public virtual void PickupPower()
    {
        Debug.Log("pickup power");
        if (this.isPickedUp == false)
        {
            if (spriteRenderer != null) spriteRenderer.enabled = false;
            if (boxCollider2D != null) boxCollider2D.enabled = false;
            if (circleCollider2D != null) circleCollider2D.enabled = false;
            this.isPickedUp = true;
            this.getPlayerController().setPowerup(this);
        }
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
