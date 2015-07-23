using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour 
{
    public TextAsset[] arrAsset;
    public int number;
    public int speedMul;
    public int attackOff;
    public int[] info = new int[3];
    public int num_level;
	void Start () 
	{
        LoadFromAsset(arrAsset[0]);
        num_level = arrAsset.Length;
	}

    public void SetLevel(int level)
    {
        LoadFromAsset(arrAsset[level]);
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
    }
}
