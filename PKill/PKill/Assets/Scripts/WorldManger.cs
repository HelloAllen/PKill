using UnityEngine;
using System.Collections;

public class WorldManger : MonoBehaviour 
{
    public GameObject light;
    public float timeClose = 5f;
    public Material skyMaterial;
    public Material skyTemp;
    public EnemyControl enemyControl;
    public EnemySpawn enemySpawn;
    public int num_enemy;
    int level = 0;
    delegate void openLightForEnemy();
    openLightForEnemy openlightforenemy;
    public static WorldManger Instance;
    public bool isNight = true;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
   //     openlightforenemy += enemyControl.AngryForLight;
        NextLevel();
    }

    void Update()
    {
        if (num_enemy == 0)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        if (level == 10)
        {
            GameWinner();
            return;
        }
        enemySpawn.SetLevel(level);
        num_enemy = enemySpawn.number;
        Debug.Log("Stage  " + level);
        level++;
    }

    void GameWinner()
    {
        Debug.Log("Winner!");
    }

    public void OpenLight()
    {
        light.SetActive(true);
        RenderSettings.skybox = skyMaterial;
        isNight = false;
        StartCoroutine(CaculateTimeOfCloseLight()); 
    }

    IEnumerator CaculateTimeOfCloseLight()
    {
        yield return new WaitForSeconds(timeClose);
        CloseLight();
    }

    void CloseLight()
    {
        light.SetActive(false);
        RenderSettings.skybox = skyTemp;
        isNight = true;
    }
}
