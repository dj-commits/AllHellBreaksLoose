using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowerup : Powerup
{
    [SerializeField]
    float timeBetweenShots;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        this.powerUpType = "Gun";
        this.lifeTimer = 5f;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ActivatePower()
    {
        base.ActivatePower();
        this.getPlayerController().getGun().setTimeBetweenShots(timeBetweenShots);
        this.getPlayerController().getGun().GetComponent<SpriteRenderer>().color = Color.green;
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
        this.getPlayerController().getGun().resetTimeBetweenShots();
        this.getPlayerController().getGun().GetComponent<SpriteRenderer>().color = Color.white;
    }
}
