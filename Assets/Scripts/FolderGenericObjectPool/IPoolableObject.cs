public interface IPoolableObject
{
    void New();      // Erstellt das Objekt
    void Respawn();  // Setzt das Objekt zurück und macht es aktiv
}
