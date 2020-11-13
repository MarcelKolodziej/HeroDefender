using System.Collections;
using UnityEngine;
using GameData;
using TMPro;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager m_LevelManager;

    [Header("Level Settings")]
    [SerializeField] private int StartingIron;
    [SerializeField] private int StartingGold;
    [SerializeField] private float NewWaveDelay = 0.5f;
    [Space]
    [SerializeField] private WaveData[] Waves;
    [Header("GUIs")]
    [SerializeField] private TextMeshProUGUI WavesText;
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI IronText;
    [SerializeField] private TextMeshProUGUI GoldText;

    private IEnumerator WaveHandler;

    private float CurrentWaveTime;
    private int CurrentWave = 0;
    public int CurrentIron;
    public int CurrentGold;
    private WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

    private void Start()
    {
        m_LevelManager = this;

        CurrentIron = StartingIron;
        CurrentGold = StartingGold;

        UpdateResourcesGUI();
        UpdateCurrentWaveGUI();

        WaveHandler = HandleNewWave();
        StartCoroutine(WaveHandler);
    }

    private IEnumerator HandleNewWave()
    {
        while (CurrentWave <= Waves.Length-1)
        {
            CurrentWaveTime = NewWaveDelay;

            while (CurrentWaveTime > 0)
            {
                CurrentWaveTime -= Time.deltaTime;
                TimerText.text = "Next Wave\n" + CurrentWaveTime.ToString("F2");
                yield return WaitForEndOfFrame;
            }

            CurrentWaveTime = Waves[CurrentWave].WaveLenght;

            foreach (EnemieSpawner enemieSpawner in Waves[CurrentWave].EnemySpawners)
            {
                enemieSpawner.StartSpawningEnemies(Waves[CurrentWave].EnemySpawnRate);
            }

            while (CurrentWaveTime > 0)
            {
                CurrentWaveTime -= Time.deltaTime;
                TimerText.text = "Wave Over\n" + CurrentWaveTime.ToString("F2");
                yield return WaitForEndOfFrame;
            }

            foreach (EnemieSpawner enemieSpawner in Waves[CurrentWave].EnemySpawners)
            {
                enemieSpawner.StopSpawningEnemies();
            }

            CurrentWave++;
            UpdateCurrentWaveGUI();
        }
    }

    public void BuildingPurchased(int ironCost, int goldCost)
    {
        CurrentIron -= ironCost;
        CurrentGold -= goldCost;

        UpdateResourcesGUI();
    }

    private void UpdateCurrentWaveGUI()
    {
        WavesText.text = "Wave\n" + (CurrentWave+1) + "/" + Waves.Length;
    }

    private void UpdateResourcesGUI()
    {
        IronText.text = "Iron\n"+ CurrentIron;
        GoldText.text = "Gold\n"+ CurrentGold;
    }
}
