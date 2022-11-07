using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafty.Systems.Character.HealthSystem;

public class HealthSystemTest : MonoBehaviour
{
    public HealthBar healthBar;
    private HealthSystem healthSystem;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem = new HealthSystem(100);

        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int value)
    {
        healthSystem.Damage(value);
    }

    public void HealPlayer(int value)
    {
        healthSystem.Heal(value);
    }


    public void AddMaxHP(int value)
    {
        healthSystem.AddMaxHp(value);
    }

    public void SetMaxHP(int value)
    {
        healthSystem.SetMaxHP(value);
    }
    public void SubtractMaxHP(int value)
    {
        healthSystem.SubtractMaxHP(value);
    }
}
