using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    public Text nameText;
    public Slider healthSlider;

    public Gradient gradient;

    public Image fill;

    public void SetHUD(Hero hero)
    {
        nameText.text = hero.heroName;
        healthSlider.maxValue = hero.maxHealth;
        healthSlider.value = hero.currentHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHP(int health)
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);

    }
}
