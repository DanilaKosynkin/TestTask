using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap _spawnTilemap;
    [SerializeField] private int _countEnemy;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        SpawnEnemyRandomly();
    }

    public void SpawnEnemyRandomly()
    {
        BoundsInt bounds = _spawnTilemap.cellBounds;
        List<Vector3Int> possibleSpawnPositions = new List<Vector3Int>();

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (_spawnTilemap.HasTile(position))
            {
                possibleSpawnPositions.Add(position);
            }
        }

        if (possibleSpawnPositions.Count > 0)
        {
            for (int i = 0; i < _countEnemy; i++)
            {
                Vector3 worldPosition;
                while (true)
                {
                    int randomIndex = Random.Range(0, possibleSpawnPositions.Count);
                    Vector3Int randomPosition = possibleSpawnPositions[randomIndex];
                    worldPosition = _spawnTilemap.GetCellCenterWorld(randomPosition);
                    if (Vector3.Distance(worldPosition, _playerTransform.position) >= 10)
                        break;
                }
                
                Enemy enemy = Instantiate(_enemyPrefab, new Vector2(worldPosition.x, worldPosition.y), Quaternion.identity);
                enemy.Init(_playerTransform);
            }
        }
    }

}
