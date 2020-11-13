using UnityEngine;

namespace GameData
{
    [System.Serializable]
    public class WaveData
    {
        [Header("Wave Settings")]
        public float WaveLenght = 1f;
        public float EnemySpawnRate = 0.5f;
        [Space]
        public EnemieSpawner[] EnemySpawners;
    }
}
