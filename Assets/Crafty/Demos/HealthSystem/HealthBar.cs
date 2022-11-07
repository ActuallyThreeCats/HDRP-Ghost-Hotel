using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Crafty.Systems.Character.HealthSystem;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Image bar;
    [SerializeField] private TextMeshProUGUI hpText;
    private int currentHP, maxHP;

    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        currentHP = healthSystem.GetHealth();
        maxHP = healthSystem.GetMaxHP();
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        bar.fillAmount = healthSystem.GetHealthPercent();
        currentHP = healthSystem.GetHealth();
        maxHP = healthSystem.GetMaxHP();
        
        hpText.text = currentHP + "/" + maxHP;

    }
}
