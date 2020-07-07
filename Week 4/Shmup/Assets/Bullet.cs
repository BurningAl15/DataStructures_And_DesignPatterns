using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : IntEventInvoker
{
    [SerializeField]
    GameObject prefabExplosion;

    private Rigidbody2D rgb;
    
    /// <summary>
    /// Initializes object. We don't use Start for this because
    /// we want to set up the points added event when we
    /// create the object in the french fries pool
    /// </summary>
    public void Initialize()
    {		
        rgb = GetComponent<Rigidbody2D>();

        // add as invoker for PointsAddedEvent
        // unityEvents.Add(EventName.PointsAddedEvent, new PointsAddedEvent());
        // EventManager.AddInvoker(EventName.PointsAddedEvent, this);
    }
    
    /// <summary>
    /// Starts the french fries moving
    /// </summary>
    public void StartMoving()
    {
        // apply impulse force to get projectile moving
        rgb.AddForce(
            new Vector2(0, ConfigurationUtils.FrenchFriesImpulseForce),
            ForceMode2D.Impulse);
    }
    
    
    /// <summary>
    /// Stops the french fries
    /// </summary>
    public void StopMoving()
    {
        rgb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Called when the french fries become invisible
    /// </summary>
    void OnBecameInvisible()
    {
        // return to the pool
        BulletPool.ReturnFrenchFries(gameObject);
    }

    /// <summary>
    /// Processes trigger collisions with other game objects
    /// </summary>
    /// <param name="other">information about the other collider</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if colliding with teddy bear, add score, destroy teddy bear,
        // and return self to pool
        if (other.gameObject.CompareTag("Enemy"))
        {
            // unityEvents[EventName.PointsAddedEvent].Invoke(ConfigurationUtils.BearPoints);
            Instantiate(prefabExplosion, 
                other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Instantiate(prefabExplosion, 
                transform.position, Quaternion.identity);
            BulletPool.ReturnFrenchFries(gameObject);
        }
        else if (other.gameObject.CompareTag ("EnemyProjectile"))
        {
            // if colliding with teddy bear projectile, destroy projectile and 
            // return self to pool
            Instantiate(prefabExplosion, 
                other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Instantiate(prefabExplosion, 
                transform.position, Quaternion.identity);
            BulletPool.ReturnFrenchFries(gameObject);
        }
    }
    
}
