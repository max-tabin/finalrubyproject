using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurHealthManager : MonoBehaviour
{

    public int MaxHealth;
    public int CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        MinotaurController j = GetComponent<MinotaurController>();
        if (CurrentHealth <=0)
        {
            //Destroy (gameObject);
            j.Fix();
        }
    }

    public void HurtEnemy (int damageToGive)
    {
        CurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }

}
