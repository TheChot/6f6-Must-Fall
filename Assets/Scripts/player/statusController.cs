using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statusController : MonoBehaviour
{
    [System.Serializable]
    public class useableItems
    {
        public string name;
        public string itemType; //antidote, health, burn heal, immunity
        public int effectAmount;
        public float toxicAdd;
    }

    public useableItems[] useList;

    public bool isPoisoned;
    public bool isBurning;
    public bool isStunned;
    public bool isImmune;
    public bool isBurnImmune;
    public bool isPoisonImmune;
    public bool isStunImmune;

    public float poisonInterval = 2.5f;
    float poisonReset;
    public float burningInterval = 1.7f;
    float burningReset;
    public float stunTime = 5.0f;
    float stunReset;
    public float immuneTime = 10.0f;
    float immuneReset;
    public int playerPain = 1;
    public bool isInCloud;
    public float cloudInterval = 2.0f;
    float cloudIntervalReset;
    public string gasMaskName;

    public float toxicity;
    public float maxToxicity;
    public bool toxic;
    public float toxicTimer = 4;
    float toxicTimerReset;
    

    public playerController pc;
    public playerMovement pm;
    public inventoryPlayer ip;

    void Start()
    {
        poisonReset = poisonInterval;
        burningReset = burningInterval;
        stunReset = stunTime;
        immuneReset = immuneTime;
        cloudIntervalReset = cloudInterval;
        toxicTimerReset = toxicTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isImmune)
        {
            immuneTime -= Time.deltaTime;
            isPoisoned = false;
            poisonInterval = poisonReset;
            isStunned = false;
            stunTime = stunReset;
            isBurning = false;
            burningInterval = burningReset;
            if (immuneTime <= 0)
            {
                immuneTime = immuneReset;
                isImmune = false;
            }
        }

        if (isPoisonImmune)
        {
            immuneTime -= Time.deltaTime;
            isPoisoned = false;
            poisonInterval = poisonReset;
            if (immuneTime <= 0)
            {
                immuneTime = immuneReset;
                isPoisonImmune = false;
            }
        }

        if (isStunImmune)
        {
            immuneTime -= Time.deltaTime;
            isStunned = false;
            stunTime = stunReset;
            if (immuneTime <= 0)
            {
                immuneTime = immuneReset;
                isStunImmune = false;
            }
        }

        if (isBurnImmune)
        {
            immuneTime -= Time.deltaTime;
            isBurning = false;
            burningInterval = burningReset;
            if (immuneTime <= 0)
            {
                immuneTime = immuneReset;
                isBurnImmune = false;
            }
        }

        if (toxicity >= maxToxicity)
        {
            toxic = true;
            toxicity = 0;
        }

        if (toxic)
        {
            toxicTimer -= Time.deltaTime;
            if (toxicTimer <= 0)
            {
                pc.damageThePlayer(playerPain);
                toxicTimer = toxicTimerReset;
                uicontroller.instance.refreshNoti("You are Toxic");
            }
        }

        if (isBurning)
        {
            burningInterval -= Time.deltaTime;

            if (burningInterval <= 0)
            {
                pc.damageThePlayer(playerPain);
                burningInterval = burningReset;
                uicontroller.instance.refreshNoti("You are Burning");
            }
        }

        if (isPoisoned)
        {
            poisonInterval -= Time.deltaTime;

            if (poisonInterval <= 0)
            {
                pc.damageThePlayer(playerPain);
                poisonInterval = poisonReset;
                uicontroller.instance.refreshNoti("You are Poisoned");
            }
        }

        if (isStunned)
        {
            pm.canControl = false;
            stunTime -= Time.deltaTime;

            if (stunTime <= 0)
            {
                pm.canControl = true;
                stunTime = stunReset;
                isStunned = false;
                uicontroller.instance.refreshNoti("Un Stunned");
            }
        }

        if (isInCloud)
        {
            cloudInterval -= Time.deltaTime;

            if (cloudInterval <= 0)
            {
                if (ip.searchAndSpend(gasMaskName, 1))
                {
                    uicontroller.instance.refreshNoti("Gas Mask Used");
                }
                else
                {
                    pc.damageThePlayer(playerPain);
                }
                
                cloudInterval = cloudIntervalReset;
            }
        }

        
    }

    public void consumeItem(string _itemName)
    {
        for (int i = 0; i < useList.Length; i++)
        {
            if (useList[i].name == _itemName)
            {
                if (useList[i].itemType == "health")
                {
                    pc.healPlayer(useList[i].effectAmount);
                    if (pc.health > pc.maxHealth)
                    {
                        pc.health = pc.maxHealth;
                    }
                    uicontroller.instance.refreshNoti("Health Recovered " + useList[i].effectAmount.ToString());

                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "antidote")
                {
                    if (isPoisoned)
                    {
                        uicontroller.instance.refreshNoti("Cured");
                    }
                    else
                    {
                        uicontroller.instance.refreshNoti("Wasted");
                    }
                    isPoisoned = false;
                    poisonInterval = poisonReset;
                    toxicity += useList[i].toxicAdd;
                    
                    
                }

                if (useList[i].itemType == "burn")
                {
                    if (isPoisoned)
                    {
                        uicontroller.instance.refreshNoti("Burn Healed");
                    }
                    else
                    {
                        uicontroller.instance.refreshNoti("Wasted");
                    }
                    isBurning = false;
                    burningInterval = burningReset;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "paralyze")
                {
                    if (isStunned)
                    {
                        uicontroller.instance.refreshNoti("Stun Cured");
                    }
                    else
                    {
                        uicontroller.instance.refreshNoti("Wasted");
                    }
                    isStunned = false;
                    stunTime = stunReset;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "poison immunity")
                {
                    isImmune = true;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "burn immunity")
                {
                    isImmune = true;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "stun immunity")
                {
                    isImmune = true;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "immunity")
                {
                    isImmune = true;
                    toxicity += useList[i].toxicAdd;
                }

                if (useList[i].itemType == "toxic heal")
                {
                    toxic = false;
                    toxicTimer = toxicTimerReset;
                }

                if (useList[i].itemType == "water")
                {                    
                    toxicity -= 2;
                    if (toxicity < 0)
                    {
                        toxicity = 0;
                    }
                }

                break;
            }
        }
    }

    public void hitPlayer(string _hitStatus)
    {
        switch (_hitStatus)
        {
            case "brn":
                isBurning = true;
                uicontroller.instance.refreshNoti("You are Burning");
                break;
            case "psn":
                isPoisoned = true;
                uicontroller.instance.refreshNoti("You are Poisoned");
                break;
            case "stun":
                isStunned = true;
                stunTime = stunReset;
                uicontroller.instance.refreshNoti("You are Stunned");
                break;
            default:
                break;
        }
    }

    public void resetStatusEffects()
    {
        toxicTimer = toxicTimerReset;
        burningInterval = burningReset;
        poisonInterval = poisonReset;
        stunTime = stunReset;
        isStunned = false;
        isBurning = false;
        isPoisoned = false;
        toxic = false;

    }

}
