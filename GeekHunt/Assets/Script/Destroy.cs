using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float wallHp = 3;
    public Sprite atkWall;

    public float enemyHp = 5;
    private Enemy enemy;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
    }

    public void AttackDamage(float attack)
    {
        if(gameObject.CompareTag("Wall"))
        {
            spriteRenderer.sprite = atkWall;

            wallHp -= attack;

            if (wallHp <= 0)
                gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            enemyHp -= attack;

            if (enemyHp <= 0)
                enemy.Death();
        }
    }
}
