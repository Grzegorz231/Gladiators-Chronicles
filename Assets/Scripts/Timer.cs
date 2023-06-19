using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Hero hero;
    static bool isOn = false; // �� ��������� ��������
    float timer = 10.0f; // ������������� �� 30 ������
    public Text timerText; // ���� ��������

    public static void TimerSwitch() // ����������� public, ����� ������ "�������" ��� �������
    {
        isOn = !isOn; // ���/����
    }

    void Update()
    {
        if (isOn) //���� �������
        {
            timer -= Time.deltaTime; // �������� 1 �������
            timerText.text = timer.ToString(); //����� �� �����
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
