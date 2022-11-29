using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float gunDirectionOffset;

    [SerializeField]
    float timeBetweenShots;

    [SerializeField]
    float defaultTimeBetweenShots;

    [SerializeField]
    bool canShoot;

    Sprite defaultSprite;

    AudioManager audioManager;

    void Start()
    {
        canShoot = true;
        defaultSprite = this.GetComponent<SpriteRenderer>().sprite;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        // sprite rotation around Mouse
        // Get mouse position (in Vector 3)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Get direction of the angle between mousePosition and player
        Vector2 gunDirection = mousePosition - this.transform.position;
        // Get the angle between the "forward position" of the object (right in the case of this sprite), and direction (angle between mousePosition and player). Signed Angle instead of reg. Angle because it allows for full 360* of rotation, reg Angle only 180
        float angle = Vector2.SignedAngle(Vector2.right, gunDirection);
        // Actually change the transform to rotate, locking x and y because that can cause sprite to disappear.
        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Shoot()
    {
        if (canShoot)
        {
            audioManager.Play("laserShoot");
            CreateBullet();
            canShoot = false;
            StartCoroutine(shootWaitTimer());
        }
    }

    private void CreateBullet()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 gunPos = this.transform.position;
        Vector2 gunDirection = mousePos - gunPos;
        gunDirection.Normalize();
        //gunDirection += (Vector2)this.transform.right;

        float bulletAngle = Vector2.SignedAngle(Vector2.up, gunDirection);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, bulletAngle));

        // 2.5f is a decent position for the gunDirectionOffset, needs tweaking.
        GameObject bulletClone = Instantiate(bullet, gunPos + (gunDirection / gunDirectionOffset), rotation);

        bulletClone.GetComponent<Rigidbody2D>().velocity = gunDirection * bulletClone.GetComponent<BulletLogic>().GetBulletSpeed();

    }

    IEnumerator shootWaitTimer()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    public float getTimeBetweenShots()
    {
        return timeBetweenShots;
    }

    public void setTimeBetweenShots(float timeBetweenShots)
    {
        this.timeBetweenShots = timeBetweenShots;
    }

    public void resetTimeBetweenShots()
    {
        this.timeBetweenShots = this.defaultTimeBetweenShots;
    }
}
