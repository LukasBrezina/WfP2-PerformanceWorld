public interface IPoolableObject
{
    void New();      // Objekt erstellen
    void Respawn();  // Setzt das Objekt zurück und macht es aktiv
}
