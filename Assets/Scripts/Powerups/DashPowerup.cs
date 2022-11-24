using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : Powerup
{
    // Timer vars
    [SerializeField]
    float dashSpeed;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Dash";
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.getPlayerController().setMoveSpeedMultiplier(dashSpeed);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            this.getPlayerController().setMoveSpeedMultiplier(this.getPlayerController().getDefaultMoveSpeedMultiplier());
        }
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.red;
    }

    public override void PickupPower()
    {
        base.PickupPower();
    }

    public override void DeactivatePower()
    {
        base.DeactivatePower();
        this.getPlayerController().setMoveSpeedMultiplier(this.getPlayerController().getDefaultMoveSpeedMultiplier());
        this.getPlayerController().GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(this);
    }
}
