using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonUpgrade : MonoBehaviour
{
    [SerializeField] float cooldown = 20;
    [SerializeField] bool activateLv3 = false;

    // Start is called before the first frame update
    void Start()
    {
        MoonAttack moonAttack = GameObject.Find("Moon").GetComponent<MoonAttack>();
        moonAttack.cooldown = cooldown;
        moonAttack.isActivated = true;
        moonAttack.isLevel3 = activateLv3;

        Destroy(gameObject);
    }

}
