using System.Collections.Generic;

// generische Klasse für alle Klassen die IPoolableObject implementieren
public class GenericObjectPool<T> where T : IPoolableObject
{
    // Pool als Stack erstellen
    private Stack<T> _pool;

    public GenericObjectPool()
    {
        _pool = new Stack<T>();
    }

    public T Spawn()
    {
        // holt Objekt aus Pool wenn vorhanden (inhalt > 0) mit pop()
        T obj = _pool.Count > 0 ? _pool.Pop() : default;
        obj?.Respawn();
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        // zurück in den Pool geben
        _pool.Push(obj);
    }
}
