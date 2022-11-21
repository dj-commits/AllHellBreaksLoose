using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerup : Powerup
{
    private bool shieldAcive;
    private GameObject playerShield;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Shield";
        this.lifeTimer = 5f;
        
    }

    public override void Update()
    {
        base.Update();
        if (shieldAcive) 
        { 
            playerShield.transform.position = this.getPlayerController().gameObject.transform.position;
        }
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        playerShield = this.getPlayerController().CreateShield();
        //this.getPlayerController().setDashRechargeTime(canDashTime);
       // this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.red;
        //this.getPlayerController().setPowerup(null);
        //StartCoroutine(setPowerupTimer());

    }

    public override void PickupPower()
    {
        base.PickupPower();
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        //this.getPlayerController().resetDashRechargeTime();
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.white;
    }
}
