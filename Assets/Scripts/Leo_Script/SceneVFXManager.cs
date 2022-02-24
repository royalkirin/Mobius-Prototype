using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVFXManager : MonoBehaviour {

    [SerializeField] private GameObject PlayerCharacter;
    [SerializeField] private GameObject EnemyCharacter;



    public static SceneVFXManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }



    public void PlayerCharacterAttacking() {



    }



    // make battle animation 
    public void AttackAnimation(GameObject PlayerReceivingdamage) {

        if (PlayerReceivingdamage == PlayerCharacter) {

            EnemyCharacter.transform.DOMove(PlayerCharacter.transform.position, 1f).SetEase(Ease.OutCubic);//.OnCompelete(() => { shouldClose = true; });

        } else if (PlayerReceivingdamage == EnemyCharacter) {

            PlayerCharacter.transform.DOMove(EnemyCharacter.transform.position, 1f).SetEase(Ease.OutCubic);//.OnCompelete(() => { shouldClose = true; });

        }

    }










}
