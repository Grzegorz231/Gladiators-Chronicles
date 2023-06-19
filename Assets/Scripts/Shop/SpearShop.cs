using UnityEngine;

public class SpearShop : MonoBehaviour
{
    public Hero player;
    public GameObject spearShop;
    public bool inShop;
    public Transform shopCheck;
    public LayerMask _layerSpearShop;
    public void Update()
    {
        CheckingShop();
        BuySpear();
    }
    public void BuySpear()
    {
        if (Input.GetKey(KeyCode.E) && player.money >= 3 && inShop)
        {
            player.playerHaveSpear = true;
            player.money -= 3;
            spearShop.SetActive(false);
        }
    }
    void CheckingShop()
    {
        inShop = Physics2D.OverlapCircle(shopCheck.position, 0.3f, _layerSpearShop);
    }
}
