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
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        
        this.getPlayerController().setCanDashTime(canDashTime);
        this.getPlayerController().setPowerup(null);
        StartCoroutine(setPowerupTime());
    }

    public override void PickupPower()
    {
        base.PickupPower();
        Debug.Log("Dash Power Picked Up");
        //this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.pickedUp = true;
        this.getPlayerController().setPowerup(this.gameObject);

        //TO-DO create an activate power button started via playerController
        ActivatePower();
    }
}
