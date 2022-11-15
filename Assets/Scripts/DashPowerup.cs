using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : Powerup
{
    [SerializeField]
    float canDashTime;

    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        this.powerUpType = "Dash";
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        this.getPlayerController().setCanDash(true);
        this.getPlayerController().setCanDashTime(canDashTime);
        this.getPlayerController().setPowerup(null);
        StartCoroutine(setPowerupTime());

    }

    public override void PickupPower()
    {
        if (this.pickedUp == false)
        {
            base.PickupPower();
            boxCollider2D.enabled = false;
            this.pickedUp = true;
            this.getPlayerController().setPowerup(this.gameObject);
        }
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        this.getPlayerController().setCanDash(false);
    }
}
