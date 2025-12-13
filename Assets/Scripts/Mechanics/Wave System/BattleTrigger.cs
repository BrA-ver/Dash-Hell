using System;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] WaveManager battleToTrigger;
    bool battleCompleted;

    private void Start()
    {
        battleToTrigger?.OnBattleStarted.AddListener(OnBattleComplete);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (battleCompleted) return;

        if (other.CompareTag("Player"))
        {
            battleToTrigger.StartBattle();
        }
    }

    private void OnBattleComplete()
    {
        battleCompleted = true;
    }
}
