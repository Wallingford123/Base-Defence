using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyEvent : UnityEvent<EnemyBase> { }

public class LevelManager : MonoBehaviour
{
    public static EnemyEvent enemyKilled = new EnemyEvent();
    public static UnityEvent endLevel = new UnityEvent();

    public static List<EnemyBase> enemies;

    public static int currentLevel = 1;

    public static float perLevelScaling = 1.0275f;

    public static bool started = false;
    public static bool levelComplete = false;

    [SerializeField]
    private Text waveText, startText, continueText;
    [SerializeField]
    private LevelGenerator levelGenerator;
    [SerializeField]
    private float spawnDelay, waveDelay;
    [SerializeField]
    private List<CustomLevel> customLevels;

    private Level level;

    private int currentWave = 0;
    private void Awake()
    {
        enemies = new List<EnemyBase>();
        levelComplete = false;
        started = false;
    }

    private void Start()
    {
        StartLevel();
        enemyKilled.AddListener(EnemyKilled);
        waveText.text = "Wave " + (currentWave + 1) + "/" + level.waves.Count;
        endLevel.AddListener(EndLevel);


    }

    private void StartLevel()
    {
        bool isCustomLevel = false;
        foreach (CustomLevel l in customLevels)
        {
            if (currentLevel == l.level)
            {
                isCustomLevel = true;
                level = levelGenerator.GenerateLevel(l.waves.Count);
                for (int i = 0; i < l.waves.Count; i++)
                {
                    if (l.waves[i] != null)
                        level.waves[i] = ConvertCustomWave(l.waves[i]);
                }
            }
        }
        if(!isCustomLevel)
            level = levelGenerator.GenerateLevel(5);
    }

    Wave ConvertCustomWave(CustomWave wave)
    {
        Wave w = new Wave();
        w.enemies = new List<WaveEnemy>();
        foreach (EnemyBase e in wave.enemies)
        {
            w.enemies.Add(new WaveEnemy(e.gameObject));
        }
        return w;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !started) { started = true; startText.enabled = false; StartCoroutine(SpawnWaves()); }
        if (Input.GetMouseButtonDown(0) && levelComplete) { SceneManager.LoadScene(0); started = false; }
    }

    IEnumerator SpawnWaves()
    {
        foreach (WaveEnemy enemy in level.waves[currentWave].enemies)
        {
            enemies.Add(Instantiate(enemy.obj, gameObject.transform.position + new Vector3(0, Random.Range(-4f, 4f)), Quaternion.identity).GetComponent<EnemyBase>());
            yield return new WaitForSeconds(spawnDelay);
        }
        while (enemies.Count > 0) { yield return new WaitForEndOfFrame(); }
        currentWave++;
        if (currentWave < level.waves.Count)
        {
            yield return new WaitForSeconds(waveDelay);
            waveText.text = "Wave " + (currentWave + 1) + "/" + level.waves.Count;
            StartCoroutine(SpawnWaves());
        }
        else
        {
            continueText.enabled = true;
            levelComplete = true;
        }
    }
    private void EnemyKilled(EnemyBase _enemy)
    {
        enemies.Remove(_enemy);
    }

    public void EndLevel()
    {
        continueText.enabled = true;
        levelComplete = true;
        enemies.Clear();
        StopAllCoroutines();
    }
}