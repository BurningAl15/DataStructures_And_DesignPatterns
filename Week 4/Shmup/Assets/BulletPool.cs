using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    static GameObject prefabBullet1;
    // static GameObject prefabBullet2;
    static List<GameObject> pool;

    
    /// <summary>
    /// Initializes the pool
    /// </summary>
    public static void Initialize()
    {
        // create and fill pool
        prefabBullet1 = Resources.Load<GameObject>("Bullet1");
        // prefabBullet1 = Resources.Load<GameObject>("Bullet2");
        pool = new List<GameObject>(2);
        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewFrenchFries());
        }
        
    }
    
    /// <summary>
    /// Gets a french fries object from the pool
    /// </summary>
    /// <returns>french fries</returns>
    public static GameObject GetFrenchFries()
    {
        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject frenchFries = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return frenchFries;
        }
        else
        {
            Debug.Log("Growing pool ...");

            // pool empty, so expand pool and return new object
            pool.Capacity++;
            return GetNewFrenchFries();
        }
    }
    
    /// <summary>
    /// Returns a french fries object to the pool
    /// </summary>
    /// <param name="frenchFries">french fries</param>
    public static void ReturnFrenchFries(GameObject frenchFries)
    {
        frenchFries.SetActive(false);
        frenchFries.GetComponent<Bullet>().StopMoving();
        pool.Add(frenchFries);
    }

    /// <summary>
    /// Gets a new french fries object
    /// </summary>
    /// <returns>french fries</returns>
    static GameObject GetNewFrenchFries()
    {
        GameObject frenchFries = GameObject.Instantiate(prefabBullet1);
        frenchFries.GetComponent<Bullet>().Initialize();
        frenchFries.SetActive(false);
        GameObject.DontDestroyOnLoad(frenchFries);
        return frenchFries;
    }
}
