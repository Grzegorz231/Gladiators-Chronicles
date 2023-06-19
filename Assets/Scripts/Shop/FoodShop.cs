using UnityEngine;

public class FoodShop : MonoBehaviour
{
    public Hero player;
    public GameObject foodShop;
    public bool inShop;
    public Transform shopCheck;
    public LayerMask _layerFoodShop;
    public void Update()
    {
        CheckingShop();
        BuyFood();
    }
    public void BuyFood()
    {
        if (Input.GetKey(KeyCode.E) && player.money >= 2 && inShop)
        {
            player.dashLock /= 2;
            player.money -= 2;
            foodShop.SetActive(false);
        }
    }
    void CheckingShop()
    {
        inShop = Physics2D.OverlapCircle(shopCheck.position, 0.3f, _layerFoodShop);
    }
}
