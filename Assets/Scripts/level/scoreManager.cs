using UnityEngine;

public class scoreManager : MonoBehaviour
{
    #region 
    public static scoreManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    [HideInInspector] public int strikes = 0;
    public int maxStrikes = 3;
    [HideInInspector]  public int score;
    public int scoreToAdd = 100;
    public bool ignoreStrikes;


    // Update is called once per frame
    void Update()
    {
        if(strikes == maxStrikes && !ignoreStrikes)
        {
            levelController.instance.isGameOver = true;
        }

        uicontroller.instance.score = score.ToString();
        uicontroller.instance.strikes = strikes.ToString();
    }
}
