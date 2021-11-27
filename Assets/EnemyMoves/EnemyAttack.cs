using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyMove
{
    public List<EnemyMove> AttackTypes = new();
    public EnemyMove MeleeAttack;
    public EnemyMove RangedAttack;

    void Start()
    {
        // Select attack types depending on enemy type
        if (enemy.type == 0) { AttackTypes.Add(MeleeAttack); }
        if (enemy.type == 1) { AttackTypes.Add(RangedAttack); }
        if (enemy.type == 2)
        {
            AttackTypes.Add(MeleeAttack);
            AttackTypes.Add(RangedAttack);
        }
    }

    void Update()
    {
        
    }
}
