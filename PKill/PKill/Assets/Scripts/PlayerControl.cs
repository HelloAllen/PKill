using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
    public float speedOfWalk = 4f;
    Vector3 direction = Vector3.zero;
    public JoyStick joyStick;
    public GameObject archer;
    public UISprite hurtSprite;
    public UIButton button;
    public UISlider slider;
    public bool isDead = false;
    public GrayscaleEffect garyEffect;
    public GameObject sky;
    PlayerLook[] arr_playerlook;

    void Start()
    {
        arr_playerlook = GetComponentsInChildren<PlayerLook>();
    }
    
	// Use this for initialization
	
	// Update is called once per frame
	void Update () 
	{
        if (isDead)
        {
            LoseTheGame();
        }
        if (!(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
        }
        else
         
        {
            direction = new Vector3(joyStick.position.x, 0, joyStick.position.y);
        }
        
        transform.Translate(direction * speedOfWalk * Time.deltaTime);
	}

    public void Fire()
    {
        GameObject archer_pre =  Instantiate(archer, this.transform.position, Quaternion.identity) as GameObject;
       // GameObject.Destroy(archer_pre, 5f);
    }

    public void BeAttack()
    {
        if (isDead)
            return;

        button.SendMessage("OnClick");
        slider.value -= 0.1f;
        if (slider.value == 0)
        {
            isDead = true;
        }

        Debug.Log("BeAtaack!" + Time.time);

    }

    IEnumerator Bianse()
    {
        hurtSprite.GetComponent<TweenColor>().enabled = false;
        yield return null;
        hurtSprite.GetComponent<TweenColor>().enabled = true;
    }

    void LoseTheGame()
    {
        Debug.Log("GG");
        isDead = true;
        garyEffect.enabled = true;
        foreach (PlayerLook item in arr_playerlook)
        {
            item.enabled = false;
        }
        Vector3 origin = transform.eulerAngles;
        float current = transform.eulerAngles.x;
        transform.LookAt(sky.transform.position);
        float destination = transform.eulerAngles.x;
        float next_x = Mathf.MoveTowardsAngle(current, destination, Time.deltaTime * 50);
        float next_y = Mathf.MoveTowardsAngle(current, destination, Time.deltaTime * 120);
        transform.eulerAngles = origin + new Vector3(next_x - current, next_y - current, 0);
    }
}
