using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Shield";
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
    }

    public override void PickupPower()
    {
        base.PickupPower();
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.white;
        //Destroy(this);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Shield Collision: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player" && !isPickedUp)
        {
            PickupPower();
        }

        if (other.gameObject.tag == "Bullet" && isPickedUp && isActive)
        {
            Debug.Log("Active shield hit bullet");
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Enemy" && isPickedUp && isActive)
        {
            Debug.Log("Active shield hit enemy");
            Destroy(other.gameObject);
        }
    }

}
