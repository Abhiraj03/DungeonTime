using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    //FloatingTextManager reference here so we can create a showText method which allows all classes to reference to the GameManger directly using the static instead of creating a reference to FloatingText class in each class.
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int pesos = 0;
    public int experience = 0;

    //Floating Text
    public void ShowText(string msg, int fontSize, Vector3 motion, float duration, Vector3 position, Color color)
    {
        floatingTextManager.Show(msg, fontSize, motion, duration, position, color);
    }

    //Weapon Upgrade
    public bool TryUpgradeWeapon()
    {
        // is weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;

    }

    public void GrantXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        experience += xp;
        if (currentLevel < GetCurrentLevel())
        {
            GetLevelUp();
        }
    }

    public void GetLevelUp()
    {
        Debug.Log("LEVEL UP!");
        player.OnLevelUp();
        OnHitPointChange();
    }

    //Hitpoint Bar system
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitpoints / (float)player.maxHitpoints;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    // On Scene Load save the spawn point of the player.
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    //Death Menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
    /*
    Save State:
    INT preferedSkin
    INT pesos
    INT experience
    INT weaponLevel
    */
    public void SaveState()
    {
        //Create a string partitioned by | pipe with the data of the current state so it can be split and used to load new state.
        string s = "";
        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        //USe PlayerPrefs to set the string with name SaveState
        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        //Check if SaveState exists or not.
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        //String array to split the saved state data and initialize all the different varibles with the values
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //Load the prefered Skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        //Load the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
        Debug.Log("LoadState");
    }

}
