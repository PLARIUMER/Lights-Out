using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    int _hp;
    public int HP { get { return _hp; }
        set 
        {
            _hp = value;

            if(_hp > maxHP)
            {
                _hp = maxHP;
            }
        }
    }
    public int maxHP;

    [SerializeField] int _damage;
    public int Damage{ get { return _damage; } set { _damage = value; } }

    [SerializeField] float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    public float atkCool;

    public float seekRange = 5;

    public float atkRange;
    



    private void Start()
    {
        HP = maxHP;
        
    }
}
