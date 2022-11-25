using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{

    // TODO: Player can potentially pick up infinite shields - whoops

    private int shieldLayer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Shield";
        shieldLayer = LayerMask.NameToLayer("Shield");
    }

    public override void Update()
    {
        base.Update();

        if (this.isActive && this.isPickedUp)
        {
            this.transform.position = this.getPlayerController().gameObject.transform.position;
        }
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        this.transform.position = this.getPlayerController().gameObject.transform.position;
        this.spriteRenderer.enabled = true;
        this.circleCollider2D.enabled = true;
        this.gameObject.layer = shieldLayer;
    }

    public override void PickupPower()
    {
        base.PickupPower();
        Physics2D.IgnoreCollision(this.circleCollider2D, playerController.getCollider(), true);
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Shield hit: " + other.gameObject.tag);

        if (other.gameObject.tag == "Player" && !isPickedUp)
        {
            PickupPower();
        }

        if (other.gameObject.tag == "Bullet" && isActive)
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Enemy" && isActive)
        {
            
        }
    }

}
