using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour 
{
    public float speed = 5f;
    Transform transform_camera;
    Transform m_transform;
    Vector3 direction;
    void Start()
    {
        transform_camera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        m_transform = this.transform;
        GameObject.Destroy(this.gameObject, 5f);
        direction = transform_camera.TransformDirection(Vector3.forward);
        
    }

	void Update () 
	{
        m_transform.Translate(direction * Time.deltaTime * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.GetComponent<EnemyControl>().enemyState != EnemyControl.EnemyState.hurt && other.GetComponent<EnemyControl>().enemyState != EnemyControl.EnemyState.dead)
        {
            other.GetComponent<EnemyControl>().enemyState = EnemyControl.EnemyState.hurt;
            GameObject.Destroy(this.gameObject);
        }
    }
}
