using UnityEngine;
using System.Collections;

public class EnemySound : MonoBehaviour 
{
    float timer = 10f;
    public  float INTERVAL = 10f;
 
    public bool isDead = false;
    public AudioClip deadSound;
    bool deadSoundFlag = false;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () 
	{
        if (!isDead)
        {
            Clock();
        }
        else
        {
           // StartCoroutine(Fadeout(2f));
            if (!deadSoundFlag)
            {
                audio.Stop();
                audio.clip = deadSound;
                audio.Play();
                StartCoroutine(Fadeout(2.5f));
                deadSoundFlag = !deadSoundFlag;
            }
        }
	}

    IEnumerator Fadeout(float duration)
    {
        float currentTime = 0;
        float waitTime = 0.02f;
        float firstVol = audio.volume;

        while (duration > currentTime)
        {
            audio.volume = Mathf.Lerp(firstVol, 0.0f, currentTime / duration);
            yield return new WaitForSeconds(waitTime);
            currentTime += waitTime;
        }
    }

    void Clock()
    {
        timer += Time.deltaTime;
        if (timer >= INTERVAL)
        {
            audio.Play();
            timer = 0;
        }
    }
}
