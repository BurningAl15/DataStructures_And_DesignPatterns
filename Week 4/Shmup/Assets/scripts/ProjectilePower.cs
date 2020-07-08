using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePower : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    float impulseForce;
    float homingDelay;
    Timer homingTimer;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {	
        // save values for efficiency
        homingDelay = ConfigurationUtils.GetHomingDelay(gameObject.tag);
        rigidbody2D = GetComponent<Rigidbody2D>();

        // create and start timer
        homingTimer = gameObject.AddComponent<Timer>();
        homingTimer.Duration = homingDelay;
        homingTimer.AddTimerFinishedEventListener(HandleHomingTimerFinishedEvent);
        homingTimer.Run();
    }

    /// <summary>
    /// Sets the impulse force
    /// </summary>
    /// <value>impulse force</value>
    public void SetImpulseForce(float impulseForce)
    {
        this.impulseForce = impulseForce;
    }

    /// <summary>
    /// Handles the homing timer finished event
    /// </summary>
    void HandleHomingTimerFinishedEvent()
    {
        // stop moving
        rigidbody2D.velocity = Vector2.zero;

        rigidbody2D.AddForce(new Vector2(-impulseForce,0),
            ForceMode2D.Impulse);

        // restart timer
        homingTimer.Run();
    }}
