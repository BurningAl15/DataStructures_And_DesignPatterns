using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A collecting game object
/// </summary>
public class Collector : MonoBehaviour
{
	#region Fields

    // targeting support
    [SerializeField] SortedList<Target> targets = new SortedList<Target>();
    Target targetPickup = null;

    // movement support
	float BaseImpulseForceMagnitude = 2.0f;
    const float ImpulseForceIncrement = 0.3f;
	
	// saved for efficiency
    Rigidbody2D rb2d;

    #endregion
    
    HUDAddPoints hudAddPoints=new HUDAddPoints();

    #region Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
		// center collector in screen
		Vector3 position = transform.position;
		position.x = 0;
		position.y = 0;
		position.z = 0;
		transform.position = position;

		// save reference for efficiency
		rb2d = GetComponent<Rigidbody2D>();

        // add as listener for pickup spawned event
        EventManager.AddListener_Pickup(HandleTarget);
        EventManager.AddInvoker_AddPoints(this);
        // EventManager.AddListener(PrintMsg);
	}

   
    
    /// <summary>
    /// Called when another object is within a trigger collider
    /// attached to this object
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay2D(Collider2D other)
    {
        // only respond if the collision is with the target pickup
		if (other.gameObject == targetPickup.GameObject)
		{
			hudAddPoints.Invoke(1);

			BaseImpulseForceMagnitude += ImpulseForceIncrement;
	        // remove collected pickup from list of targets and game
            int targetPickupIndex = targets.IndexOf(targetPickup)!= -1 ? targets.IndexOf(targetPickup) : targets.Count - 1;
            GameObject deadTarget = targets[targetPickupIndex].GameObject;
            targets.RemoveAt(targetPickupIndex);
            Destroy(deadTarget);

			// go to next target if there is one
			rb2d.velocity=Vector2.zero;

			if (targets.Count > 0)
			{
				for (int i = 0; i < targets.Count; i++)
				{
					targets[i].UpdateDistance(transform.position);
				}
				targets.Sort();
				SetTarget(targets[targets.Count-1]);
			}
			else
			{
				targetPickup = null;
			}
        }
	}

    void HandleTarget(GameObject obj)
    {
	    //Adds new target to the list of targets
	    targets.Add(new Target(obj,this.transform.position));
	    // targets.Sort();

	    float currentDistance = 0;

	    if (targetPickup != null)
	    {
		    currentDistance = Vector3.Distance(targetPickup.GameObject.transform.position, transform.position);
	    }
	    else
	    {
		    currentDistance = float.MaxValue;
	    }

	    if (targets[targets.Count - 1].Distance < currentDistance)
	    {
		    SetTarget(targets[targets.Count-1]);
	    }
    }
    
	/// <summary>
	/// Sets the target pickup to the provided pickup
	/// </summary>
	/// <param name="pickup">Pickup.</param>
	void SetTarget(Target _pickup)
	{
		targetPickup = _pickup;
		GoToTargetPickup();
	}

	/// <summary>
	/// Starts the teddy bear moving toward the target pickup
	/// </summary>
	void GoToTargetPickup()
    {
        // calculate direction to target pickup and start moving toward it
		Vector2 direction = new Vector2(
			targetPickup.GameObject.transform.position.x - transform.position.x,
			targetPickup.GameObject.transform.position.y - transform.position.y);
		direction.Normalize();
		rb2d.velocity = Vector2.zero;
		rb2d.AddForce(direction * BaseImpulseForceMagnitude, 
			ForceMode2D.Impulse);
	}
	
	#endregion
	
	/// <summary>
	/// Adds the given listener for the pickup spawned event
	/// </summary>
	/// <param name="listener">listener</param>
	public void AddListener_AddPoints(UnityAction<int> listener)
	{
		hudAddPoints.AddListener(listener);
	}
}
