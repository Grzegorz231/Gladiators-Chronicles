using UnityEngine;

public class HealthShop : MonoBehaviour
{
    public Hero player;
    public GameObject healthShop;
    public bool inShop;
    public Transform shopCheck;
    public LayerMask _layerHealthShop;
    public void Update()
    {
        CheckingShop();
        BuyHealth();
    }
    public void BuyHealth()
    {
        if (Input.GetKey(KeyCode.E) && player.money >= 1 && inShop)
        {
            player.lives++;
            player.money--;
            healthShop.SetActive(false);
        }
    }
    void CheckingShop()
    {
        inShop = Physics2D.OverlapCircle(shopCheck.position, 0.3f, _layerHealthShop);
    }
}
