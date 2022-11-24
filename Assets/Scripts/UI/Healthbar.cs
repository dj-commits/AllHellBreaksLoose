using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider slider;
    float playerStartHealth;

    PlayerController playerController;

    private void Start()
    {
        slider = GetComponent<Slider>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerStartHealth = playerController.GetPlayerHealth();
        slider.maxValue = playerStartHealth;
        SetHealth(playerStartHealth);

    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
