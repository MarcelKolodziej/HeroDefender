using UnityEngine;

namespace GameData
{
    [System.Serializable]
    public class WaveData
    {
        public float WaveLenght = 1f;
        public int NumberOfEnemies;
        public GameObject[] EnemiePrefabs;
        public GameObject[] SpawnPoints;
    }
}
