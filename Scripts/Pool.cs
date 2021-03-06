using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    public List<GameObject> pool;
    private GameObject mainObject;

    //Pool settings
    private int poolSize;
    private bool initialState;
    private Transform parent;
    private Vector3? startPosition;
    private string pooledObjectName;

    //Runtime variables
    private int poolIndex = 0;

    public Pool(
        GameObject mainObject, 
        int poolSize=20, 
        bool initialState=false, 
        Transform parent=null, 
        Vector3? startPosition=null,
        string pooledObjectName="Pooled object"
    )
    {
        //Set object to pool
        this.mainObject = mainObject;

        //Set pool settings
        this.poolSize = poolSize;
        this.initialState = initialState;
        this.parent = parent;
        this.startPosition = startPosition;
        this.pooledObjectName = pooledObjectName;

        //Generate pool
        this.pool = generatePool();
    }

    public List<GameObject> generatePool()
    {
        List<GameObject> newPool = new List<GameObject>();
        GameObject newObject;

        for (int i = 0; i < poolSize; i++)
        {
            newObject = GameObject.Instantiate(
                mainObject,
                startPosition != null ? (Vector3)startPosition : Vector3.zero,
                Quaternion.identity,
                parent
            );

            newObject.name = pooledObjectName + " " + (i+1).ToString();
            newObject.SetActive(this.initialState);
            newPool.Add(newObject);
        }

        return newPool;
    }

    public List<GameObject> increasePoolSize(int increaseBy, bool initialState=false, Transform parent=null)
    {
        GameObject newObject;
        int newSize = poolSize + increaseBy;

        for (int i = poolSize; i < newSize; i++)
        {
            newObject = GameObject.Instantiate(mainObject, Vector3.zero, Quaternion.identity, parent);
            newObject.name = pooledObjectName + " " + (i+1).ToString();
            newObject.SetActive(initialState);
            pool.Add(newObject);
        }

        poolSize = newSize;

        return pool;
    }

    public GameObject spawnAt(Vector3 pos, bool newState = true)
    {
        if(poolIndex == poolSize-1)
        {
            poolIndex = 0;
        }

        pool[poolIndex].transform.position = pos;
        pool[poolIndex].SetActive(newState);
        poolIndex++;

        return pool[poolIndex];
    }

    public GameObject spawnAt(Vector3 pos, int index, bool newState=true)
    {
        pool[index].transform.position = pos;
        pool[index].SetActive(newState);

        return pool[index];
    }

    public GameObject spawnAt(Vector3 pos, GameObject obj, bool newState = true)
    {
        pool[pool.IndexOf(obj)].transform.position = pos;
        pool[pool.IndexOf(obj)].SetActive(newState);

        return pool[pool.IndexOf(obj)];
    }

    public GameObject nextObject(int step=1)
    {
        int index = poolIndex;

        for (int i = 0; i < step; i++)
        {
            index++;

            if(index >= poolSize)
            {
                index = 0;
            }
        }

        return pool[index];
    }

    public GameObject previousObject(int step=1)
    {
        int index = poolIndex;

        for (int i = 0; i < step; i++)
        {
            index--;

            if (index == poolSize-1)
            {
                index = poolSize;
            }
        }

        return pool[poolIndex];
    }

    public void disableAll()
    {
        foreach(GameObject obj in pool)
        {
            obj.SetActive(false);
        }

        poolIndex = 0;
    }

    public void disableAt(int index)
    {
        pool[index].SetActive(false);
    }

    public void disable(GameObject obj)
    {
        pool[pool.IndexOf(obj)].SetActive(false);
    }

    public void enableAll()
    {
        foreach (GameObject obj in pool)
        {
            obj.SetActive(true);
        }

        poolIndex = 0;
    }

    public void enableAt(int index)
    {
        pool[index].SetActive(true);
    }

    public void enable(GameObject obj)
    {
        pool[pool.IndexOf(obj)].SetActive(true);
    }
}
