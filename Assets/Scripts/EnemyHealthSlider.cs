using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private float maxHealth = 100f;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    public void ChangeMaxHealth(float health)
    {
        maxHealth = health;
    }

    public void UpdateHealthSlider(float health)
    {
        healthSlider.value = health / maxHealth;
    }
}
