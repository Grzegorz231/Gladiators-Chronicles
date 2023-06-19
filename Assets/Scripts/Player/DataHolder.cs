using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataHolder
{
    //собираем монетки
    public static int moneyToSave = 0;
    public static int livesToSave = 1;

    // атакуем
    public static bool playerHaveSpearToSave = false;
    public static float attackRangeToSave = 1;

    //дэшимся
    public static float dashLockToSave = 10;

    // прыгаем
    public static float jumpForceToSave = 220;
}