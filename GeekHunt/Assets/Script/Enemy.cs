using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public float moveTime = 0.1f;
    public bool moving = false;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;

    public int attack = 5;
    private Animator animator;

    private Transform target;
    private bool skipMove = false;                      //2ターンに一回動く


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        GameManager.instance.AddEnemy(this);
    }


    public void ATMove(int horizontal, int vertical)
    {
        RaycastHit2D hit;

        bool canMove = Move(horizontal, vertical, out hit);     //out <- 必ず値が入るという意味

        if (hit.transform == null)
        {
            GameManager.instance.playerTurn = false;
            return;
        }

        Player hitComponent = hit.transform.GetComponent<Player>();       //ぶつかった場所の位置情報を格納しDestroy.csの処理を行う

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);

        GameManager.instance.playerTurn = false;
    }

    public bool Move(int horizontal, int vertical, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(horizontal, vertical);

        boxCollider.enabled = false;                                        //コンポーネントごとにチェックを外す

        hit = Physics2D.Linecast(start, end, blockingLayer);         //現在地の値を入れる＋blockingLayerに当たっているか

        boxCollider.enabled = true;

        if (!moving && hit.transform == null)
        {
            StartCoroutine(Movement(end));                                  //Coroutineは徐々に処理していく
            return true;
        }

        return false;
    }

    IEnumerator Movement(Vector3 end)                                       //コルーチンを使用するため
    {
        moving = true;
        float remainingDistance = (transform.position - end).sqrMagnitude;  //距離の2乗をしている

        while (remainingDistance > float.Epsilon)                           //float.Epsilonは0に限りなく近い
        {
            transform.position = Vector2.MoveTowards(transform.position, end, 1f / moveTime * Time.deltaTime);  //現在地からendまで動く

            remainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;                                              //コルーチンのリターンに使用
        }
        transform.position = end;                                           //現在地の更新

        moving = false;
    }

    public void OnCantMove(Player hit)
    {
        hit.EnemyAttack(attack);
        animator.SetTrigger("Enemy_Attack");
    }

    public void MoveEnemy()
    {
        if (!skipMove)
        {
            skipMove = true;
            int xdir = 0;
            int ydir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
            {
                ydir = target.position.y > transform.position.y ? 1 : -1;
            }
            else
            {
                xdir = target.position.x > transform.position.x ? 1 : -1;
            }
            ATMove(xdir, ydir);
        }
        else
        {
            skipMove = false;
            return;
        }
    }

    public void Death()
    {
        GameManager.instance.DestroyEnemy(this);
        gameObject.SetActive(false);
    }


}
