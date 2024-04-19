using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DG.Tweening;
public class WeaponRotate : MonoBehaviour
{
    public GameObject[] weapon;
    public SpriteRenderer playerSprite;
    float angle;
    bool isAttacking;
    void Update()
    {
        if (isAttacking)
            return;

        Rotate();
        Attack();
    }

    void Rotate()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!playerSprite.flipX)
        {
            angle = Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x) * Mathf.Rad2Deg;
            weapon[0].SetActive(true);
            weapon[1].SetActive(false);
        }
        else
        {
            angle = Mathf.Atan2(transform.position.y - point.y, transform.position.x - point.x) * Mathf.Rad2Deg;
            weapon[0].SetActive(false);
            weapon[1].SetActive(true);
        }
        transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!playerSprite.flipX)
            {
                isAttacking = true;
                transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + 80), 0.2f, RotateMode.FastBeyond360).OnComplete(()=>
                {
                    transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - 130), 0.1f, RotateMode.Fast).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + 50), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic).OnComplete(() =>
                        {
                            isAttacking = false;
                        });
                    });
                });
            }
            else
            {
                isAttacking = true;
                transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - 80), 0.2f, RotateMode.FastBeyond360).OnComplete(() =>
                {
                    transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z + 130), 0.1f, RotateMode.Fast).OnComplete(() =>
                    {
                        transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - 50), 0.2f, RotateMode.Fast).SetEase(Ease.OutCubic).OnComplete(() =>
                        {
                            isAttacking = false;
                        });
                    });
                });
            }
        }
    }
}
