public interface IPoolableObject
{
    void New();      // create object
    void Respawn();  // reset object and set active
}
