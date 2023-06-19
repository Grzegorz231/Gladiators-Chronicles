using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int lives;
    public virtual void GetDamage()
    {
        lives--;
        if (lives < 1)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
}
