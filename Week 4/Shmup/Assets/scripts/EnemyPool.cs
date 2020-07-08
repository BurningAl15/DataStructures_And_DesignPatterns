using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    static GameObject prefabEnemy1;
    static List<GameObject> pool;

    
    /// <summary>
    /// Initializes the pool
    /// </summary>
    public static void Initialize()
    {
        // create and fill pool
        prefabEnemy1 = Resources.Load<GameObject>("enemyBlack1");
        pool = new List<GameObject>(20);
        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewEnemy());
        }
    }
    
    /// <summary>
    /// Gets a enemies object from the pool
    /// </summary>
    /// <returns>enemies</returns>
    public static GameObject GetEnemies()
    {
        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject enemy = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return enemy;
        }
        else
        {
            Debug.Log("Growing pool ...");

            // pool empty, so expand pool and return new object
            pool.Capacity++;
            return GetNewEnemy();
        }
    }
    
    /// <summary>
    /// Returns a enemies object to the pool
    /// </summary>
    /// <param name="enemy">enemy</param>
    public static void ReturnEnemies(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().StopMoving();
        enemy.SetActive(false);
        enemy.transform.position = Vector2.one * 50;
        pool.Add(enemy);
    }

    /// <summary>
    /// Gets a new enemies object
    /// </summary>
    /// <returns>enemy</returns>
    static GameObject GetNewEnemy()
    {
        // print("A");
        GameObject enemy = GameObject.Instantiate(prefabEnemy1);
        // enemy.GetComponent<Enemy>().Initialize();
        enemy.SetActive(false);
        GameObject.DontDestroyOnLoad(enemy);
        return enemy;
    }

}
