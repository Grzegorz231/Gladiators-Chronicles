using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void Update()
    {
        Restart();
    }
    void Restart()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                DataHolder.livesToSave = DataHolder.livesToSaveChange;
                DataHolder.moneyToSave = DataHolder.moneyToSaveChange;
                DataHolder.jumpForceToSave = DataHolder.jumpForceToSaveChange;
                DataHolder.playerHaveSpearToSave = DataHolder.playerHaveSpearToSaveChange;
                DataHolder.attackRangeToSave = DataHolder.attackRangeToSaveChange;
                DataHolder.dashLockToSave = DataHolder.dashLockToSaveChange;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }
}
