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

	void Update () 
	{
        m_transform.position = player.position + new Vector3(0, 10, 0);
	}
}
