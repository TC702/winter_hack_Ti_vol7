using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float moveTime = 0.1f;
    public bool moving = false;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;

    public int attack = 1;
    private Animator animator;

    private int food;
    public Text foodText;
    private int foodpoint = 10;
    private int sodapoint = 20;

    private bool open = true;
    public BoardManager boardManager;
    public GameObject gameManager;
   


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        food = GameManager.instance.food;
        foodText.text = "Food:" + food;
        boardManager = gameManager.GetComponent<BoardManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerTurn)                   //GameManager.csのplayerTurnがfalseなら攻撃しない
            return;

        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");

        if(horizontal != 0)
        {
            vertical = 0;
            if (horizontal == 1)
                transform.localScale = new Vector3(1, 1, 1);
            else if (horizontal == -1)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        if (horizontal != 0 || vertical != 0)
            ATMove(horizontal, vertical);

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            
        }*/
    }

    

    public void ATMove(int horizontal, int vertical)
    {
        RaycastHit2D hit;
        food--;
        foodText.text = "Food:" + food;

        bool canMove = Move(horizontal, vertical, out hit);     //out <- 必ず値が入るという意味

        if(hit.transform == null)
        {
            GameManager.instance.playerTurn = false;
            return;
        }

        Destroy hitComponent = hit.transform.GetComponent<Destroy>();       //ぶつかった場所の位置情報を格納しDestroy.csの処理を行う

        if(!canMove && hitComponent != null)
            OnCantMove(hitComponent);
        
        GameManager.instance.playerTurn = false;
        CheckFood();
    }
    
    public void OnCantMove(Destroy hit)
    {
        hit.AttackDamage(attack);
        animator.SetTrigger("Attack");
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
        CheckFood();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            food += foodpoint;
            foodText.text = "Food:" + food;
            collision.gameObject.SetActive(false);
        }

        else if (collision.tag == "Soda")
        {
            food += sodapoint;
            foodText.text = "Food:" + food;
            collision.gameObject.SetActive(false);
        }

        else if (collision.tag == "Exit")
        {
            Invoke("ReStart", 0.2f);
            enabled = false;                            //プレイヤーの操作が何も効かなくなる
        }

        else if (collision.tag == "TresureBox" && open)
        {
            open = false;
            Vector2 pos = GameObject.FindGameObjectWithTag("TresureBox").transform.position;
            boardManager.TresureOpen(pos);
            collision.gameObject.SetActive(false);
        }
    }   

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnDisable()                             //enabledがfalseになったら動く
    {
        GameManager.instance.food = food;
    }

    private void CheckFood()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }

    public void EnemyAttack(int lose)
    {
        animator.SetTrigger("Hit");
        food -= lose;
        foodText.text = lose + "damages" + " 　Food:" + food;
        CheckFood();
    }
}
