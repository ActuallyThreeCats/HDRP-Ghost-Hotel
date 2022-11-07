/*This code and practically all code related to the level system in this package was created following 
Code Monkey's Level System Tutorial on youtube found here: https://www.youtube.com/watch?v=kKCLMvsgAR0 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Crafty.Systems.Character.LevelSystem
{
    public class LevelSystem
    {
        public event EventHandler OnExperienceChanged;
        public event EventHandler OnLevelChanged;
        private static readonly int[] experiencePerLevel = new[] { 100, 220, 360, 500, 650, 850 };
        private int level;
        private int experience;


        public LevelSystem()
        {
            level = 0;
            experience = 0;
 
        }

        public void AddExperience(int amount)
        {
            if (!IsMaxLevel())
            {
                experience += amount;
                while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level))
                {
                    Debug.Log("Adding Level");
                    experience -= GetExperienceToNextLevel(level);
                    level++;
                    OnLevelChanged?.Invoke(this, EventArgs.Empty);
                }
                Debug.Log("adding exp");
                OnExperienceChanged?.Invoke(this, EventArgs.Empty);
            }
            
        }

        public int GetLevelNumber()
        {
            return level;
        }

        public int GetExperience()
        {
            return experience;
        }
        public int GetExperienceToNextLevel(int level)
        {
            if (level < experiencePerLevel.Length)
            {
                return experiencePerLevel[level];

            }
            else
            {
                Debug.LogError("Level Invalid: " + level);
                return 100;
            }
            
        }

        public float GetExperienceNormalized()
        {
            if (IsMaxLevel())
            {
                Debug.Log("max?");
                return 1f;
            }
            else
            {
                return (float)experience / GetExperienceToNextLevel(level);

            }
        }

        public bool IsMaxLevel()
        {
            return IsMaxLevel(level);
        }

        public bool IsMaxLevel(int level)
        {
            return level == experiencePerLevel.Length - 1;
        }
    }
}

