using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody2D rocket;				// Prefab of the rocket.
	public float speed = 50f;				// The speed the rocket will fire at.


	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.


	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

   // Rigidbody2D tempBullet;

    private const double fireRate = 0.25;
    private double lastShot = 0.0;
  

    void Update ()
	{
		// If the fire button is pressed...
		if(Input.GetButton("Fire1"))
		{
            Fire();
		}
	}

    void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;
            Rigidbody2D bulletInstance;
            // If the player is facing right...
            if (playerCtrl.facingRight)
            {
                Vector2 vel = GetVector(speed, playerCtrl.AimAngle);
                float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                //Quaternion rotation = Quaternion.Euler(0, 0, 0);
                // ... instantiate the rocket facing right and set it's velocity to the right. 
                bulletInstance = Instantiate(rocket, transform.position, rotation) as Rigidbody2D;
                bulletInstance.velocity = GetVector(speed, playerCtrl.AimAngle);
               
            }
            else
            {
                // Otherwise instantiate the rocket facing left and set it's velocity to the left.

                Vector2 vel = GetVector(-speed, -playerCtrl.AimAngle);
                float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //Quaternion rotation = Quaternion.Euler(0, 0, 180f);

                bulletInstance = Instantiate(rocket, transform.position, rotation) as Rigidbody2D;
                bulletInstance.velocity = vel;
            }

            //tempBullet = bulletInstance;
            
            // ... set the animator Shoot trigger parameter and play the audioclip.
            anim.SetTrigger("Shoot");
            GetComponent<AudioSource>().Play();
        }
    }

    

    Vector2 GetVector(float speed, float angle)
    {
        float angleRadians = angle * Mathf.Deg2Rad;
        float adjacent = Mathf.Cos(angleRadians) * speed;
        float opposite = Mathf.Sin(angleRadians) * speed;
        return new Vector2(adjacent, opposite);
    }

    
}
