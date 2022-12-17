using UnityEngine;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour
{

    #region 
    public static levelController instance;

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

    public GameObject thePlayer;
    public bool isGameOver;
    public int roomToLoad = 0;
    public int doorToEnter = 0;
    public roomManager rm;
    bool isReloaded;
    [HideInInspector]
    public int saveSlot = 0;
    public int testSaveSlot = 0;
    public bool loadFromSave = true;
    [HideInInspector]
    public saveData savedata;

    public int firstRoom = 0;
    public int firstDoor = 0;

    public bool goToNextRoom;
    public bool isPaused;

    void Start()
    {
        saveSlot = PlayerPrefs.GetInt("save slot", testSaveSlot);
        loadTheGame();
        //loadFromSave = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (uicontroller.instance.displayPauseMenu())
            {
                Time.timeScale = 0;
                isPaused = true;
            }
            
        }
        if (Input.GetKey(KeyCode.R) && isGameOver)
        {
            isReloaded = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //reload the scene in testing
        if (Input.GetKey(KeyCode.R))
        {
            reloadTheScene();
        }
        // test game over
        if (Input.GetKey(KeyCode.T))
        {
            isGameOver = true;
            reloadTheScene();
        }
        //reload a scene
        if (isReloaded && !isGameOver && !goToNextRoom)
        {
            rm.loadCurrentRoom(roomManager.instance.currentRoom, doorToEnter, false);
            isReloaded = false;
        }

        //reload a scene in to go to the next room
        if (isReloaded && !isGameOver && goToNextRoom)
        {
            rm.loadCurrentRoom(roomToLoad, doorToEnter, false);
            isReloaded = false;
            goToNextRoom = false;
        }

        //reload the scene after a gameover
        if (isReloaded && isGameOver)
        {
            loadTheGame();
            isReloaded = false;
            isGameOver = false;
        }
    }

    //For testing and use for the ui button on the game over screen
    public void reloadTheScene()
    {
        isReloaded = true;
        uicontroller.instance.gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    //For the public
    public void loadTheGame()
    {
        continueGame();
        savedata = saveManager.loadData(saveSlot);
        if (savedata != null && loadFromSave)
        {
            roomToLoad = savedata.roomNumber;
            rm.loadCurrentRoom(roomToLoad, doorToEnter, true);
            characterUpgrade.instance.setupPlayer(savedata);
            thePlayer.GetComponent<gunInventory>().refreshWeapons(savedata);
            thePlayer.GetComponent<inventoryPlayer>().loadPlayerInventory(savedata);
            treasureManager.instance.loadUnlockedStoreItems(savedata);
            gunManager.instance.loadUnlockedWeapons(savedata);
            thePlayer.GetComponent<statusController>().resetStatusEffects();
            Debug.Log("loaded from save");

        }
        else
        {
            rm.loadCurrentRoom(firstRoom, firstDoor, false);
            characterUpgrade.instance.setupPlayer();
            thePlayer.GetComponent<gunInventory>().refreshWeapons();
            thePlayer.GetComponent<inventoryPlayer>().loadPlayerInventory();
            thePlayer.GetComponent<statusController>().resetStatusEffects();
            Debug.Log("loaded not from save");

        }
        
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }
}
