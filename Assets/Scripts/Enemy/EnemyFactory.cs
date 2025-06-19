using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject bigZombiePrefab;
    public GameObject bossZombiePrefab;

    public GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject enemy = null;

        switch (type)
        {
            case EnemyType.Zombie:
                enemy = Instantiate(zombiePrefab, position, Quaternion.identity);
                break;
            case EnemyType.BigZombie:
                enemy = Instantiate(bigZombiePrefab, position, Quaternion.identity);
                break;
            case EnemyType.BossZombie:
                enemy = Instantiate(bossZombiePrefab, position, Quaternion.identity);
                break;
        }

        return enemy;
    }
}