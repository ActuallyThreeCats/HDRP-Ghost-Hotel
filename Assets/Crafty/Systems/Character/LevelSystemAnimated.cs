/*This code and practically all code related to the level system in this package was created following 
Code Monkey's Level System Tutorial on youtube found here: https://www.youtube.com/watch?v=kKCLMvsgAR0 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.Character.LevelSystem;

namespace Crafty.Systems.Character.LevelSystem
{
    public class LevelSystemAnimated : MonoBehaviour
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;

        private LevelSystem levelSystem;
        [SerializeField] private bool isAnimating;

        private float updateTimer;
        private float updateTimerMax;

        private int level;
        [SerializeField] private int experience;
        private int experienceToNextLevel;

        public LevelSystemAnimated(LevelSystem levelSystem)
        {

            SetLevelSystem(levelSystem);

        }

        private void Start()
        {
            updateTimerMax = 0.016f;

        }

        public void SetLevelSystem(LevelSystem levelSystem)
        {
            //Debug.Log("Test");
            this.levelSystem = levelSystem;

            level = levelSystem.GetLevelNumber();
            experience = levelSystem.GetExperience();

            levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
            levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;

        }

        private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
        {
            isAnimating = true;
            
        }

        private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
        {
            isAnimating = true;
            
        }

        private void Update()
        {
            if (isAnimating)
            {
                updateTimer += Time.deltaTime;
                while (updateTimer > updateTimerMax)
                {
                    updateTimer -= updateTimerMax;
                    UpdateAddExperience();
                }


            }
        }

        private void UpdateAddExperience()
        {
            if (level < levelSystem.GetLevelNumber())
            {
                AddExperience();
            }
            else
            {
                if (experience < levelSystem.GetExperience())
                {
                    AddExperience();
                }
                else
                {
                    isAnimating = false;
                }
            }
        }
        private void AddExperience()
        {
            experience++;
            if (experience >= levelSystem.GetExperienceToNextLevel(level))
            {
                level++;
                experience -= levelSystem.GetExperienceToNextLevel(level-1);
                OnLevelChanged?.Invoke(this, EventArgs.Empty);
            }

            OnExperienceChanged?.Invoke(this, EventArgs.Empty);

        }
        public int GetLevelNumber()
        {
            return level;
        }
        public float GetExperienceNormalized()
        {
            if (levelSystem.IsMaxLevel(level))
            {
               
                return 1f;
            }
            else
            {
                
                return (float)experience / levelSystem.GetExperienceToNextLevel(level);

            }
        }
    }

}
