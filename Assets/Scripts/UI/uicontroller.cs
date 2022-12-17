using UnityEngine;
using TMPro;

public class uicontroller : MonoBehaviour
{
    #region 
    public static uicontroller instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI jetpackText;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI amooCountText;
    public TextMeshProUGUI clipSizeText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI roundTimeText;
    public TextMeshProUGUI restText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strikeText;
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI noticeText;
    [HideInInspector] public string health;
    [HideInInspector] public string jetpackFuel;
    [HideInInspector] public string weaponName;
    [HideInInspector] public string amooCount;
    [HideInInspector] public string clipSize;
    [HideInInspector] public string hoverOverText;
    [HideInInspector] public string restMode;
    [HideInInspector] public string roundTime;
    [HideInInspector] public string currentRound;
    [HideInInspector] public string score;
    [HideInInspector] public string strikes;
    [HideInInspector] public string noti;
    public GameObject weaponSlot;

    public float notTime = 2f;
    float notTimeReset;
    public bool isNoti;

    public GameObject gameOverScreen;
    public GameObject playerInfo;

    public GameObject messageScreen;
    public GameObject inventoryScreen;
    public GameObject noticeScreen;
    public GameObject weaponScreen;
    public GameObject shopScreen;
    public GameObject upgradeScreen;
    public GameObject pauseMenu;
    public playerMovement pm;
    

    void Start()
    {
        notTimeReset = notTime;
        notificationText.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        healthText.text = health;
        jetpackText.text = jetpackFuel;
        weaponNameText.text = weaponName;
        amooCountText.text = amooCount;
        clipSizeText.text = clipSize;        
        
        
        
        
        if (isNoti)
        {
            notTime -= Time.deltaTime;
            
            if (notTime <= 0)
            {
                isNoti = false;
                notTime = notTimeReset;
                notificationText.gameObject.SetActive(false);

            }
        }
        
        gameOverScreen.SetActive(levelController.instance.isGameOver);
        //playerInfo.SetActive(!levelController.instance.isGameOver);

    }

    public void refreshNoti(string _notiText)
    {
        isNoti = true;
        notificationText.text = _notiText;
        notTime = notTimeReset;
        notificationText.gameObject.SetActive(true);
        Debug.Log("Notify " + _notiText);
    }

    public void showMessage(string _message)
    {
        messageScreen.SetActive(true);
        playerInfo.SetActive(false);
        messageText.text = _message;
        pm.canControl = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void dismissMessage()
    {
        messageScreen.SetActive(false);
        playerInfo.SetActive(true);
        pm.canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openInventoryScreen()
    {
        inventoryScreen.SetActive(true);
        playerInfo.SetActive(false);
        levelController.instance.pauseGame();
        Cursor.lockState = CursorLockMode.Confined;
        pm.canControl = false;
    }

    public void dismissInventoryMenu()
    {
        inventoryScreen.SetActive(false);
        playerInfo.SetActive(true);
        pm.canControl = true;
        levelController.instance.continueGame();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void openNoticeScreen(string _message)
    {
        noticeScreen.SetActive(true);
        noticeText.text = _message;
    }
    public void dismissNotice()
    {
        noticeScreen.SetActive(false);
    }

    public void openWeaponScreen()
    {
        weaponScreen.SetActive(true);
        playerInfo.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        pm.canControl = false;
    }

    public void dismissWeaponScreen()
    {
        weaponScreen.SetActive(false);
        playerInfo.SetActive(true);
        pm.canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openShopScreen()
    {
        shopScreen.SetActive(true);
        playerInfo.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        pm.canControl = false;
    }

    public void dismissShopScreen()
    {
        shopScreen.SetActive(false);
        playerInfo.SetActive(true);
        pm.canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void openUpgradeScreen()
    {
        upgradeScreen.SetActive(true);
        playerInfo.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        pm.canControl = false;
    }

    public void dismissUpgradeScreen()
    {
        upgradeScreen.SetActive(false);
        playerInfo.SetActive(true);
        pm.canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool displayPauseMenu()
    {
        if (!upgradeScreen.activeInHierarchy && !inventoryScreen.activeInHierarchy && !shopScreen.activeInHierarchy && !weaponScreen.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            playerInfo.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            pm.canControl = false;
            return true;
        }
        return false;
    }

    public void dismissPauseMenu()
    {
        pauseMenu.SetActive(false);
        playerInfo.SetActive(true);
        levelController.instance.continueGame();
        pm.canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
