using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    public Hero player;
    private void OnTriggerEnter2D()
    {
        PickUpCoin();
    }
    void PickUpCoin()
    {
        player.money++;
        Destroy(this.gameObject);
    }
}
