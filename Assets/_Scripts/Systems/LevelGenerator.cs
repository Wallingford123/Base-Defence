using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<WaveEnemy> enemies;

    [SerializeField]
    private int baseWeight;

    private int totalWeight;
    private void Awake()
    {
        foreach (WaveEnemy e in enemies)
        {
            e.GetBrain();
        }
        totalWeight = Mathf.FloorToInt(baseWeight*(LevelManager.perLevelScaling*LevelManager.currentLevel));
    }
    private Wave GenerateWave()
    {

        Wave wave = new Wave();
        wave.enemies = new List<WaveEnemy>();

        int curWeight = 0;
        while (curWeight < totalWeight)
        {
            WaveEnemy temp = enemies[Random.Range(0, enemies.Count)];
            wave.enemies.Add(temp);
            curWeight += temp.brain.GetWeight();
        }
        return wave;
    }

    public Level GenerateLevel(int waves)
    {
        Level level = new Level();
        level.waves = new List<Wave>();
        for(int i = 0; i < waves; i++)
            level.waves.Add(GenerateWave());
        return level;
    }
}

[System.Serializable]
public class WaveEnemy
{
    public GameObject obj;
    [HideInInspector]
    public EnemyBase brain;

    public void GetBrain()
    {
        brain = obj.GetComponent<EnemyBase>();
    }

    public WaveEnemy(GameObject _obj)
    {
        obj = _obj;
        GetBrain();
    }
}