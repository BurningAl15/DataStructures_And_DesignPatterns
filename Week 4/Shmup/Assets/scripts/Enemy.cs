using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
	[FormerlySerializedAs("prefabExplosion")] [SerializeField]
	GameObject prefabProjectile;

	Timer shootTimer;
	const float TeddyBearProjectilePositionOffset = 0.5f;

	[SerializeField] Rigidbody2D rgb;
	
	private void OnEnable()
	{
		Initialize();
		Move();
	}

	public void Move()
	{
		// apply impulse force to get projectile moving
		rgb.AddForce(
			new Vector2(-ConfigurationUtils.TeddyBearProjectileImpulseForce/2,0),
			ForceMode2D.Impulse);
		
		// create and start timer
		shootTimer.AddTimerFinishedEventListener(HandleTimerFinishedEvent);
		StartRandomTimer();
	}

	public void Initialize()
	{
		if(rgb==null)
			rgb = GetComponent<Rigidbody2D>();
		shootTimer = gameObject.AddComponent<Timer>();
	}
	
	public void StopMoving()
	{
		rgb.velocity = Vector2.zero;
	}

    /// <summary>
    /// Shoots a teddy bear projectile, resets the timer
    /// duration, and restarts the timer
    /// </summary>
    void HandleTimerFinishedEvent()
    {
	    // shoot a teddy bear projectile
	    Vector3 projectilePos = transform.position;
	    projectilePos.x -= TeddyBearProjectilePositionOffset;
	    Instantiate(prefabProjectile, projectilePos, 
		    prefabProjectile.transform.rotation);
	    AudioManager.Play(AudioClipName.TeddyShot);
		
	    // change timer duration and restart
	    StartRandomTimer();
    }

    /// <summary>
    /// Starts the timer with a random duration
    /// </summary>
    void StartRandomTimer()
    {
	    shootTimer.Duration = Random.Range(ConfigurationUtils.BearMinShotDelay, 
		    ConfigurationUtils.BearMaxShotDelay);
	    shootTimer.Run();
    }
    
    void OnBecameInvisible()
    {
	    // destroy the game object
	    // Destroy(gameObject);
	    EnemyPool.ReturnEnemies(gameObject);
    }
}
