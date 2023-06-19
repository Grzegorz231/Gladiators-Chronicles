using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Hero hero;
    static bool isOn = false; // по умолчанию выключен
    float timer = 10.0f; // устанавливаем на 30 секунд
    public Text timerText; // куда выводить

    public static void TimerSwitch() // обязательно public, чтобы кнопка "увидела" эту функцию
    {
        isOn = !isOn; // вкл/выкл
    }

    void Update()
    {
        if (isOn) //если включен
        {
            timer -= Time.deltaTime; // отнимаем 1 секунду
            timerText.text = timer.ToString(); //вывод на экран
        }
        if (isOn && timer <= 0 && hero.dashLock == 10)
        {
            isOn = false;
            timer = 10;
            timerText.text = timer.ToString();
        }
        if (isOn && timer <=0 && hero.dashLock == 5)
        {
            isOn = false;
            timer = 5;
            timerText.text = timer.ToString();
        }
    }
}
