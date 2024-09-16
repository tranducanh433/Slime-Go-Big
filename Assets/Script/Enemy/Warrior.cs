using MyExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Enemy
{
    [SerializeField] List<GameObject> armors;
    [SerializeField] float force = 5;
    [SerializeField] float minAngle = 45;
    [SerializeField] float maxAngle = 135;


    protected override void StartFunc()
    {
        OnTakeDamage.AddListener(RemoveArmor);
    }


    public void RemoveArmor()
    {

        int removeArmorAtHP = 12 - ((6 - armors.Count) * 2);
        while(currentHP <= removeArmorAtHP)
        {
            if (armors.Count <= 0) 
                break;

            armors[0].transform.localScale = new Vector3(1, -1, 1);
            armors[0].transform.parent = null;
            Destroy(armors[0], 2);

            Rigidbody2D armorRB = armors[0].AddComponent<Rigidbody2D>();
            armorRB.gravityScale = 2;
            float randAngle = Random.Range(minAngle, maxAngle);
            Vector2 forceDir = randAngle.ToDirection2D() * force;
            armorRB.AddForce(forceDir);

            armors.RemoveAt(0);

            removeArmorAtHP = 12 - ((6 - armors.Count) * 2);
        }
    }
}
