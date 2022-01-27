using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level Setup/Custom Level", order = 2)]
public class CustomLevel : ScriptableObject
{
    public List<CustomWave> waves;
    public int level;
}
