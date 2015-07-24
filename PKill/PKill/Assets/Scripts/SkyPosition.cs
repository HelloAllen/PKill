using UnityEngine;
using System.Collections;

public class SkyPosition : MonoBehaviour 
{
    Transform m_transform;
    public Transform player;
	void Start () 
	{
        m_transform = this.transform;
	}
    // 12:01 test
    // 12:04 test
    // this is just for git test
	void Update () 
	{
        m_transform.position = player.position + new Vector3(0, 10, 0);
	}
}
