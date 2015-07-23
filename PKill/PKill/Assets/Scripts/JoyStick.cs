using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoyStick : MonoBehaviour 
{
    public float nguiScale = 400;
    Vector3 center, touchPos;
    Plane plane;
    [HideInInspector]
    public Vector2 position = Vector2.zero;
    float radius = 800f;

    public List<PlayerLook> lookScripts;
	// Use this for initialization
	void Start () 
	{
        center = transform.localPosition;
	}
    void update()
    {
    }

    void OnPress(bool pressed)
    {
        if (pressed)
        {
            touchPos = UICamera.lastHit.point;
            plane = new Plane(Vector3.back, touchPos);
            foreach (PlayerLook lookScript in lookScripts)
            {
                lookScript.touchIndex = UICamera.currentTouchID;
                
            }
        }
        else
        {
            transform.localPosition = center;
            position = Vector2.zero;
            foreach (PlayerLook lookScript in lookScripts)
            {
                lookScript.touchIndex = -1;
            }
        }
    }

    void OnDrag(Vector2 delta)
    {
        Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
        float dist = 0f;
        if (plane.Raycast(ray, out dist))
        {
            Vector3 currentPos = ray.GetPoint(dist);
            Vector3 off = currentPos - touchPos;
            touchPos = currentPos;

            off.z = 0;
            transform.position += off;
            float length = transform.localPosition.magnitude;
            if (length > radius)
            {
                transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, radius);
            }
            position = new Vector2((transform.localPosition.x - center.x) / radius, (transform.localPosition.y - center.y) / radius);
        }
    }
}
