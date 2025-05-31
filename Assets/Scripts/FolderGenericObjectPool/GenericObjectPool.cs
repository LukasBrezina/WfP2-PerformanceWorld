using System.Collections.Generic;

// generic class for each class that implement IPoolAbleObject
public class GenericObjectPool<T> where T : IPoolableObject
{
    // stack as pool
    private Stack<T> _pool;

    public GenericObjectPool()
    {
        _pool = new Stack<T>();
    }

    public T Spawn()
    {
        // get object from pool if pool count > 0
        T obj = _pool.Count > 0 ? _pool.Pop() : default;
        obj?.Respawn();
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        // return object to pool
        _pool.Push(obj);
    }
}
