using UnityEngine;
using System.Collections;

public class WorldManger : MonoBehaviour 
{
    public GameObject light;
    public float timeClose = 5f;
    public Material skyMaterial;
    public Material skyTemp;
    public EnemyControl enemyControl;

    delegate void openLightForEnemy();
    openLightForEnemy openlightforenemy;

    void Start()
    {
        openlightforenemy += enemyControl.AngryForLight;
    }

    public void OpenLight()
    {
        light.SetActive(true);
        RenderSettings.skybox = skyMaterial;
        openlightforenemy();

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
    }
}
