using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

    public GameObject player;
    Transform transform_player;
    Transform transform_enmey;

    bool flag_attack = false;
    bool flag_firstattack = true;
    bool can_angry = true;

    public int attackOff = 5;
    public enum EnemyState
    {
        walk=0,
        angry,
        run,
        attack,
        attackidle,
        jump,
        hurt,
        dead,
        hide
    }

    public enum SkyState
    {
        night=0,
        day
    }
    public SkyState skyState = SkyState.night;
    public EnemyState enemyState = EnemyState.walk;
    NavMeshAgent navMeshAgent;
    public Animation animation;
    public float speed_walk;
    public float speed_run;
    public float time_attackidle = 0.5f;
    float timer = 0;
    SkinnedMeshRenderer[] arr_Skinrender;
    MeshRenderer[] arr_render;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindWithTag("Player");
        transform_player = player.transform;
        transform_enmey = this.transform;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        speed_walk = navMeshAgent.speed;
        speed_run = speed_walk * 10;
        arr_Skinrender = GetComponentsInChildren<SkinnedMeshRenderer>();
        arr_render = GetComponentsInChildren<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log(enemyState);
        switch (enemyState)
        {
            case EnemyState.walk:
                flag_firstattack = true;
                animation.CrossFade("G_war_walk");
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    navMeshAgent.enabled = true;
                    navMeshAgent.SetDestination(player.transform.position + player.transform.InverseTransformDirection(Vector3.forward)*3);
                    timer = 0;
                }
                break;

            case EnemyState.angry:
                animation.CrossFade("G_war_looking_around");
                navMeshAgent.enabled = false;
                StartCoroutine(timeForAfterAngry());
                break;

            case EnemyState.run:
                animation.CrossFade("G_war_run");
                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(player.transform.position + player.transform.InverseTransformDirection(Vector3.forward) * 3);
                navMeshAgent.speed = speed_run;
                break;

            case EnemyState.attack:
                animation.CrossFade("G_war_Digging");
                navMeshAgent.enabled = false;
                transform_enmey.LookAt(transform_player.position);
                if (!flag_attack)
                {
                    StartCoroutine(timeForAttack());
                    flag_attack = true;
                }
                break;

            case EnemyState.attackidle:
                animation.CrossFade("G_war_talking");
                navMeshAgent.enabled = false;
                transform_enmey.LookAt(transform_player.position);
                StartCoroutine(timeForAttackIdle());
                break;
            case EnemyState.jump:
                animation.CrossFade("G_war_jump_blow");
                StartCoroutine(timeForJump());
                break;

            case EnemyState.hurt:
                ShowSelf();
                navMeshAgent.enabled = false;
                animation.CrossFade("G_war_anger");
                StartCoroutine(timeForBeforeDaying());
                break;
            case EnemyState.dead:
                ShowSelf();
                animation.CrossFade("G_war_daying");
                StartCoroutine(timeForAfterDaying());
                break;
        }

        skyState = WorldManger.Instance.isNight ? SkyState.night : SkyState.day;

        if (enemyState == EnemyState.hurt || enemyState == EnemyState.dead)
            return;

        switch (skyState)
        {
            case SkyState.night:
                can_angry = true;
                foreach (SkinnedMeshRenderer item in arr_Skinrender)
                {
                    item.enabled = false;
                }
                foreach (MeshRenderer item in arr_render)
                {
                    item.enabled = false;
                }
                break;
            case SkyState.day:
                if (can_angry)
                {
                    can_angry = false;
                    StartCoroutine(timeForBeforeAngry());
                }

                ShowSelf();
                break;
        }

        CaculateDistance();
        
	}

    private void ShowSelf()
    {
        foreach (SkinnedMeshRenderer item in arr_Skinrender)
        {
            item.enabled = true;
        }
        foreach (MeshRenderer item in arr_render)
        {
            item.enabled = true;
        }
    }

    IEnumerator timeForAttackIdle()
    {
        yield return new WaitForSeconds(time_attackidle * attackOff);
        enemyState = EnemyState.walk;
    }

    IEnumerator timeForAttack()
    {
     //   if (flag_firstattack)
     //       yield return new WaitForSeconds(0.5f);
     //   else
            yield return new WaitForSeconds(1.167f);
        
        player.GetComponent<PlayerControl>().BeAttack();
        flag_attack = false;
        flag_firstattack = false;
     //   enemyState = EnemyState.attackidle;
    }

    IEnumerator timeForAfterDaying()
    {
        yield return new WaitForSeconds(2.16f);
        WorldManger.Instance.num_enemy -= 1;
        Destroy(this.gameObject);
    }

    IEnumerator timeForBeforeDaying()
    {
        yield return new WaitForSeconds(1.6f);
        enemyState = EnemyState.dead;

    }

    IEnumerator timeForJump()
    {
        yield return new WaitForSeconds(1f);
    //    Debug.Log(animation.clip + ":" + animation.clip.length);
        enemyState = EnemyState.walk;
    }

    void CaculateDistance()
    {
        Vector3 distance = transform_player.position - transform_enmey.position;
        if (distance.magnitude <= 10f && enemyState != EnemyState.hurt && enemyState!= EnemyState.dead && enemyState != EnemyState.attackidle)
        {
            enemyState = EnemyState.attack;
        }
        if (distance.magnitude > 15f && (enemyState == EnemyState.attack || enemyState == EnemyState.attackidle))
        {
            enemyState = EnemyState.jump;
        }
    }

    public void AngryForLight()
    {
        
    }

    IEnumerator timeForBeforeAngry()
    {

        yield return new WaitForSeconds(2f);
        enemyState = EnemyState.angry;
    }

    IEnumerator timeForAfterAngry()
    {
        yield return new WaitForSeconds(2f);
        enemyState = EnemyState.run;
    }
}
