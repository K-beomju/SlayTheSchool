using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject targetSlot;
    [SerializeField] private int damage;
    private EnemyAnimation enemyAnim;
    private SpineSkillObject spineSkill;


    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimation>();
    }


    public void Attack()
    {
        StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(3f);
        transform.DOMoveX(transform.position.x - 2, 0.3f).OnComplete(() =>
        {
            transform.DOMoveX(transform.position.x + 2, 0.5f);
            PlayerHealth ph = FindObjectOfType<PlayerHealth>();
            ph.OnDamage(damage);
            CameraManager.ShakeCam(1, 0.3f);

            SoundManager.Instance.PlayFXSound("EnemyAttack");

            spineSkill = GameManager.GetBiteEffect();
            spineSkill.SetPositionData(new Vector3(ph.transform.position.x, ph.transform.position.y + 0.4f, 0), Utils.QI);
        });
        yield return new WaitForSeconds(2f);
        TurnManager.Instance.TurnStart();
    }



    void OnMouseEnter()
    {
        if (CardManager.Instance.isAttackCardArea())
            targetSlot.SetActive(true);
    } 

    void OnMouseExit()
    {
        if(targetSlot.activeSelf)
            targetSlot.SetActive(false);
    }


}
