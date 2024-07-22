using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gfxs;

        private void OnEnable() {
            SetRandomGFX();
        }

        private void SetRandomGFX()
        {
            int randomGfx = Random.Range(0, _gfxs.Length);
            for (int i = 0; i < _gfxs.Length; i++)
            {
                _gfxs[i].SetActive(i == randomGfx);
            }
        }

        public void MoveToPlayer(Vector3 targetPos)
        {
            transform.DOMove(targetPos, 1f).OnComplete(StartDeal);
        }

        private void StartDeal()
        {
            DealManager.Instance.StartDeal(this);
        }

        public int GivePrice(int estimatedPrice, int dealPhase)
        {
            switch (dealPhase)
            {
                case 0:
                    return estimatedPrice;
                default:
                    return estimatedPrice;
            }
        }
    }
}