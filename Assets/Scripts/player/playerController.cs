using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int health = 5;
    [HideInInspector]
    public int maxHealth;
    public int shield = 2;
    public bool shieldActive;
    
    public bool godMode;
    Animator anim;
    [HideInInspector]
    public string currentGun;
    string prevGun;

    public Animator uianim;
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGun != prevGun)
        {
            anim.SetBool(prevGun, false);
            prevGun = currentGun;
        }

        

        anim.SetBool(currentGun, true);

        uicontroller.instance.health = health.ToString();

        if(!godMode && health <= 0)
        {
            levelController.instance.isGameOver = true;
        }
        
    }

    public void damageThePlayer(int _dmg)
    {
        if (!godMode)
        {
            uianim.SetTrigger("ishit"); //use different color for shield hit
            if (shield > 0 && shieldActive)
            {
                shield -= _dmg;
            }
            else
            {
                health -= _dmg;
            }
        }        
    }

    public void healPlayer(int _health)
    {
        health += _health;
    }
}
