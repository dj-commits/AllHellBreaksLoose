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
        //Moved this to the base class - this.GetComponent<SpriteRenderer>().enabled = false;
        // Should we get the component off of the base class of the sub-class?
        boxCollider2D.enabled = false;
        this.pickedUp = true;
        this.getPlayerController().setPowerup(this.gameObject);

        //TO-DO create an activate power button started via playerController
        ActivatePower();
    }
}
