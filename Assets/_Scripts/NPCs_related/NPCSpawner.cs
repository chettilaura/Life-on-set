using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject>  _agents;
    [SerializeField] private int _navMeshAgentsToSpawn = 10;
    [SerializeField] private Collider _groundCollider;
    private int _type;

    private static NPCSpawner _instance;
    public static NPCSpawner Instance => _instance;

    void Start()
    {
        _instance = this;
        SpawnNavMeshAgents();
    }

    private void SpawnNavMeshAgents()
    {
        for (int i = 0; i < _navMeshAgentsToSpawn; i++)
        {
            if (_agents.Count.Equals(1))
                _type = 0;
            else
                _type = Random.Range(0, _agents.Count -1 );
            GameObject agent = Instantiate(_agents[_type], GetRandomPositionOnGround(), Quaternion.identity);
        }
    }

    public Vector3 GetRandomPositionOnGround()
    {
        Vector3 min = _groundCollider.bounds.min;
        Vector3 max = _groundCollider.bounds.max;
        return new Vector3(Random.Range(min.x, max.x), 2f, Random.Range(min.z, max.z));
    }

    public void DestroyAgents()
    {
        for (int i = 0; i < _navMeshAgentsToSpawn -1; i++)
        {
            Destroy(_agents[i]);
        }
    }
}
