using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Wave", menuName = "Level Setup/Custom Wave", order = 1)]
public class CustomWave : ScriptableObject
{
    public List<EnemyBase> enemies;
}

[CustomEditor(typeof(CustomWave))]
[CanEditMultipleObjects]
public class CustomWaveEditor : Editor
{
    public EnemyBase test;
    CustomWave wave;
    public void OnEnable()
    {
        wave = (CustomWave)target;
    }
    public override void OnInspectorGUI()
    {
        test = EditorGUILayout.ObjectField("Quick Add Target", test, typeof(EnemyBase), false) as EnemyBase;
        if (GUILayout.Button("Add"))
        {
            if (wave.enemies != null)
                wave.enemies.Add(test);
            else wave.enemies = new List<EnemyBase>() { test };
        }

        base.OnInspectorGUI();
    }
}