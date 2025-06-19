using UnityEngine;

public class Spawner : MonoBehaviour
{
    public EnemyFactory factory;

    void Start()
    {
        factory.CreateEnemy(EnemyType.Zombie, new Vector3(0, 0, 0));
    }
}