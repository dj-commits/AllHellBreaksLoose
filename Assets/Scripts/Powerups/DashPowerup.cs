using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : Powerup
{
    [SerializeField]
    float canDashTime;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Dash";
        this.lifeTimer = 5f;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        this.getPlayerController().setDashRechargeTime(canDashTime);
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.red;
        this.getPlayerController().setPowerup(null);
        StartCoroutine(setPowerupTimer());

    }

    public override void PickupPower()
    {
        base.PickupPower();
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        this.getPlayerController().resetDashRechargeTime();
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.white;
    }
}
