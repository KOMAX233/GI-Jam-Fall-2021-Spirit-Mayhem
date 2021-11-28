using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyMove
{
    private List<EnemyMove> AttackTypes = new();
    public List<EnemyMove> EnemyAttacks = new();
    public EnemyMove MeleeAttack;
    public EnemyMove RangedAttack;
    void Start()
    {
        // Select attack types depending on enemy type
        if (enemy.type == 0) { EnemyAttacks.Add(MeleeAttack); }
        if (enemy.type == 1) { EnemyAttacks.Add(RangedAttack); }
        if (enemy.type == 2)
        {
            EnemyAttacks.Add(MeleeAttack);
            EnemyAttacks.Add(RangedAttack);
        }

        AttackTypes.Add(MeleeAttack);
        AttackTypes.Add(RangedAttack);

    }

    void Update()
    {       
        if (enemy.type == 0)
        {
            enemy.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
        else if (enemy.type == 1)
        {
            enemy.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        }
    }
}
