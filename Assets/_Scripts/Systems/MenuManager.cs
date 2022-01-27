using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private Text levelText;

    private void Start()
    {
        levelText.text = LevelManager.currentLevel.ToString();
    }
    public void PlusLevel()
    {
        LevelManager.currentLevel++;
        levelText.text = LevelManager.currentLevel.ToString();
    }
    public void MinusLevel()
    {
        LevelManager.currentLevel--;
        if (LevelManager.currentLevel < 1) LevelManager.currentLevel = 1;
        levelText.text = LevelManager.currentLevel.ToString();
    }
}
