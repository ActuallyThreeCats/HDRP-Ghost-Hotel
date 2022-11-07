/*This code and practically all code related to the level system in this package was created following 
Code Monkey's Level System Tutorial on youtube found here: https://www.youtube.com/watch?v=kKCLMvsgAR0 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Crafty.Systems.Character.LevelSystem;

public class LevelWindow : MonoBehaviour
{
    private TextMeshProUGUI levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;


    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        experienceBarImage = transform.Find("ExperienceBar").Find("Bar").GetComponent<Image>();

        
    }
    private void SetExperieceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    public void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelsystem)
    {
        this.levelSystem = levelsystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;
       
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperieceBarSize(levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperieceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }

    public void GiveVariedExperience(int amount)
    {
        levelSystem.AddExperience(amount);
       
    }


}
