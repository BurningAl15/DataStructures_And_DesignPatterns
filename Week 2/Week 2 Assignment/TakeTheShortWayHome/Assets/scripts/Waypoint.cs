using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A waypoint
/// </summary>
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    int id;

    public bool wasVisited = false;
    public bool activate = false;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
    }

    /// <summary>
    /// Changes waypoint to green
    /// </summary>
    /// <param name="other">other collider</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.name != "Start" && this.name != "End")
        {
            // wasVisited = true;
            EventManager.AddPathTraversalCompleteListener(CallExplosion);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        // StartCoroutine(CallExplotion(.5f));
    }

    // private void Update()
    // {
    //     if (wasVisited && activate)
    //     {
    //         GetComponent<SpriteRenderer>().enabled = false;
    //         GetComponent<Collider2D>().enabled = false;
    //     
    //         Instantiate(explosion, this.transform.position, Quaternion.identity);
    //     }
    // }

    void CallExplosion()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }
    
    IEnumerator CallExplotion(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        // yield return new WaitUntil(()=>!particles.IsAlive());
    }
    

    /// <summary>
    /// Gets the position of the waypoint
    /// </summary>
    /// <value>position</value>
    public Vector2 Position
    {
        get { return transform.position; }
    }

    /// <summary>
    /// Gets the unique id for the waypoint
    /// </summary>
    /// <value>unique id</value>
    public int Id
    {
        get { return id; }
    }
}
