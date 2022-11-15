using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private int playerLayer;
    private int bulletLayer;
    private int enemyLayer;
    private int powerupLayer;

    [SerializeField]
    float powerupTime;

    // Components
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Camera cam;
    EnemyLogic enemyLogic;
    PlayerController playerController;

    public bool pickedUp;
    public bool activated;
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
    }

    public virtual void ActivatePower()
    {
        
    }

    public virtual void PickupPower()
    {
        Physics2D.IgnoreLayerCollision(powerupLayer, playerLayer, true);
        spriteRenderer.enabled = false;
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

    public IEnumerator setPowerupTime()
    {
        yield return new WaitForSeconds(powerupTime);
        this.getPlayerController().setCanDashTime(-1);
        Destroy(gameObject);
    }
}
