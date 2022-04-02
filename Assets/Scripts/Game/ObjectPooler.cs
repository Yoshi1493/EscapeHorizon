using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler<T> : MonoBehaviour where T : Debris
{
    public static ObjectPooler<T> Instance { get; private set; }
    (T obj, Queue<T> queue) objectPool;

    [SerializeField] T objectToPool;

    void Awake()
    {
        Instance = this;

        objectPool = (objectToPool, new Queue<T>());
    }

    public T Get()
    {
        if (objectPool.queue.Count > 0)
        {
            return objectPool.queue.Dequeue();
        }
        else
        {
            T newObject = Instantiate(objectPool.obj, transform);
            newObject.gameObject.SetActive(false);
            newObject.enabled = false;

            return newObject;
        }
    }

    public void ReturnToPool(T returningObj)
    {
        returningObj.gameObject.SetActive(false);
        returningObj.enabled = false;

        objectPool.queue.Enqueue(returningObj);
    }
}