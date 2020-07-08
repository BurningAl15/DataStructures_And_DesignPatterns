using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    static GameObject prefabBullet1;
    static List<GameObject> pool;

    
    /// <summary>
    /// Initializes the pool
    /// </summary>
    public static void Initialize()
    {
        // create and fill pool
        prefabBullet1 = Resources.Load<GameObject>("Bullet1");
        pool = new List<GameObject>(30);
        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewBullet());
        }
        
    }
    
    /// <summary>
    /// Gets a bullet object from the pool
    /// </summary>
    /// <returns>french fries</returns>
    public static GameObject GetBullets()
    {
        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject bullet = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return bullet;
        }
        else
        {
            Debug.Log("Growing pool ...");

            // pool empty, so expand pool and return new object
            pool.Capacity++;
            return GetNewBullet();
        }
    }
    
    /// <summary>
    /// Returns a bullets object to the pool
    /// </summary>
    /// <param name="bullet">french fries</param>
    public static void ReturnBullets(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.GetComponent<Bullet>().StopMoving();
        pool.Add(bullet);
    }

    /// <summary>
    /// Gets a new bullets object
    /// </summary>
    /// <returns>french fries</returns>
    static GameObject GetNewBullet()
    {
        GameObject bullet = GameObject.Instantiate(prefabBullet1);
        bullet.GetComponent<Bullet>().Initialize();
        bullet.SetActive(false);
        GameObject.DontDestroyOnLoad(bullet);
        return bullet;
    }
}
