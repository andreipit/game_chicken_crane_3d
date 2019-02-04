using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TCrane;

namespace TCrane
{
    public class Spawner : MonoBehaviour
    {
        //config
        public GameObject prefab;
        public Transform[] spawnPoints;
        public int maxAlive = 1;

        //state
        public List<GameObject> alive;
        
        //bookkeeping
        float lastRandom;

        void Update()
        {
            alive = alive.Where(item => item != null).ToList();
            if (alive.Count < maxAlive) Spawn();
        }

        void Spawn()
        {
            alive.Add( Instantiate(prefab, spawnPoints[GetNewRandom()].position, Quaternion.identity, transform) );
        }

        int GetNewRandom()
        {
            int random = Random.Range(0, spawnPoints.Length);
            while (random == lastRandom) random = Random.Range(0, spawnPoints.Length);
            lastRandom = random;
            return random;
        }
    }
}
