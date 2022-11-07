using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.Character.LevelSystem;
public class testing : MonoBehaviour
{
    //[SerializeField] private LevelSystemAnimated levelSystemAnimated;

    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private LevelSystemAnimated levelSystemAnimated;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        //LevelSystemAnimated levelSystemAnimated = gameObject.AddComponent(typeof(LevelSystem)) as LevelSystemAnimated;
        levelSystemAnimated.SetLevelSystem(levelSystem);
        levelWindow.SetLevelSystem(levelSystem);
        //player.SetLevelSystem(levelSystem);

        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
    }
}
