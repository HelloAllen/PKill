using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour 
{
    public Transform Player;
    public GameObject EnemyPrefab;
    public TextAsset[] arrAsset;
    public int number;
    public int speedMul;
    public int attackOff;
    public int[] info = new int[3];
    public int num_level;
	void Start () 
	{
      //  LoadFromAsset(arrAsset[0]);
        num_level = arrAsset.Length;
	}

    public void SetLevel(int level)
    {
        Debug.Log("level:" + level);
        LoadFromAsset(arrAsset[level]);
        SpawnEnemy();
    }

    void LoadFromAsset(TextAsset asset)
    {
        string txtData = asset.text;
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;
        string[] lines = txtData.Split(new char[] { '\r', '\n' }, option);
        for (int i = 0; i < lines.Length; i++)
        {
            info[i] = int.Parse(lines[i]);
        }
        number = info[0];
        speedMul = info[1];
        Debug.Log("number:" + number);
        Debug.Log("speedmul:" + speedMul);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < number; i++)
        {
            float PorN = 1;
            if (Random.Range(0.5f, 1.5f) > 1f)
                PorN = 1;
            else
                PorN = -1;
            float x = Random.Range(50f, 200f) * PorN;

            if (Random.Range(0.5f, 1.5f) > 1f)
                PorN = 1;
            else
                PorN = -1;
            float z = Random.Range(50f, 200f) * PorN;

            Vector3 born = Player.position + new Vector3(x, 0, z);

         //   born.x = Mathf.Clamp(born.x, 20, 450);
         //   born.z = Mathf.Clamp(born.z, 20, 450);

            GameObject enemyObject =  Instantiate(EnemyPrefab, born, Quaternion.identity) as GameObject;
            EnemyControl enemy = enemyObject.GetComponent<EnemyControl>();
            Debug.Log("Spawn");
            enemy.speed_walk *= speedMul;
            enemy.speed_run *= speedMul;
        }
    }
}
