using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crafty.Systems.Character.HealthSystem
{
    public class HealthSystem
    {
        public event EventHandler OnHealthChanged;
        private int currentHP;
        private int maxHP;

        public HealthSystem(int maxHP)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
        }

        public int GetHealth()
        {
            return currentHP;
        }
        public int GetMaxHP()
        {
            return maxHP;
        }

        public void Damage(int damageAmount)
        {
            currentHP -= damageAmount;
            if(currentHP < 0)
            {
                currentHP = 0;
            }
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Heal(int healAmount)
        {
            currentHP += healAmount;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            OnHealthChanged?.Invoke(this, EventArgs.Empty);

        }

        public void SetMaxHP(int value)
        {
            maxHP = value;
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
        public void AddMaxHp(int value)
        {
            maxHP += value;
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
        public void SubtractMaxHP(int value)
        {
            maxHP -= value;
            if(maxHP <= 1)
            {
                maxHP = 1;
            }

            OnHealthChanged?.Invoke(this, EventArgs.Empty);

        }

        public float GetHealthPercent()
        {
            return (float)currentHP / maxHP;
        }
    }
}

