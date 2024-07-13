using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolManager<T> where T : Component
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T prefab;
    private readonly Transform parentTransform;

    public PoolManager(T prefab, int initialSize, Transform parentTransform = null)
    {
        this.prefab = prefab;
        this.parentTransform = parentTransform;
        for (int i = 0; i < initialSize; i++)
        {
            AddToPool(CreateNewInstance());
        }
    }

    public T GetItem(float returnTime = 0)
    {
        if (pool.Count == 0)
        {
            AddToPool(CreateNewInstance());
        }

        T item = pool.Dequeue();
        item.gameObject.SetActive(true);

        if (returnTime > 0)
        {
            MonoBehaviour instance = item.GetComponent<MonoBehaviour>();
            if (instance != null)
            {
                instance.StartCoroutine(ReturnToPoolAfterTime(item, returnTime));
            }
        }

        return item;
    }

    public void ReturnItem(T item)
    {
        item.gameObject.SetActive(false);
        AddToPool(item);
    }

    private void AddToPool(T item)
    {
        item.gameObject.SetActive(false);
        pool.Enqueue(item);
    }

    private T CreateNewInstance()
    {
        T newItem = UnityEngine.Object.Instantiate(prefab, parentTransform);
        newItem.gameObject.SetActive(false);
        return newItem;
    }

    private IEnumerator ReturnToPoolAfterTime(T item, float time)
    {
        yield return new WaitForSeconds(time);
        ReturnItem(item);
    }
}