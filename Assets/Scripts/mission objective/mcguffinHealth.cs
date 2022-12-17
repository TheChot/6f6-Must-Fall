using UnityEngine;

public class mcguffinHealth : MonoBehaviour
{
    public int health;
    renderMapIcon rm;
    bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        enemyTargetManager.instance.addToMcguffinList(this);
        rm = GetComponent<renderMapIcon>();
    }

    // Update is called once per frame
    void Update()
    {
        rm.iconText.text = health.ToString();
        if (health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            scoreManager.instance.strikes++;
            enemyTargetManager.instance.destroyedMcguffins++;
            uicontroller.instance.noti = "TOOLS DESTROYED";
            uicontroller.instance.isNoti = true;
            Destroy(rm.theIcon);
            gameObject.SetActive(false);
            
        }
    }
}
