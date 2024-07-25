using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gfxs;

        int estimatedPrice;
        int customerOffer;

        private void OnEnable()
        {
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

        [SerializeField] private string[] _firstCentences;

        public int GivePrice(int estimatedPrice)
        {
            this.estimatedPrice = estimatedPrice;
            float priceMultiplier = Random.Range(0.9f, 1.1f);
            customerOffer = (int)(estimatedPrice * priceMultiplier);
            string randomSentence = _firstCentences[Random.Range(0, _firstCentences.Length)];
            randomSentence = randomSentence.Replace("yyy", customerOffer.ToString() + "$");
            DealManager.Instance.SetCustomerDialogTMP(randomSentence);
            return customerOffer;
        }

        [SerializeField] private string[] _secondOfferSentences;
        [SerializeField] private string[] _angryOfferSentences;

        public void GiveOffer(int playerOffer, int phase)
        {
            if (phase == 0)
            {
                int min = (int)(customerOffer * 1.1f);
                int max = (int)(customerOffer * 1.5f);
                if (playerOffer <= min)
                {
                    DealManager.Instance.AcceptOffer(playerOffer);
                }
                else if (playerOffer <= max)
                {
                    bool acceptOffer = Random.Range(0, 2) == 0;
                    if (acceptOffer)
                    {
                        DealManager.Instance.AcceptOffer(playerOffer);
                    }
                    else
                    {
                        int customerNewOffer = (int)(playerOffer * 0.95f);
                        string randomSentence = _secondOfferSentences[Random.Range(0, _secondOfferSentences.Length)];
                        randomSentence = randomSentence.Replace("x$", customerNewOffer.ToString() + "$");
                        DealManager.Instance.SetCustomerDialogTMP(randomSentence);
                        DealManager.Instance.CustomerOffers(customerNewOffer);
                    }
                }
                else
                {
                    int randomness = Random.Range(0, 10);
                    if (randomness <= 1)
                    {
                        int customerNewOffer = (int)(estimatedPrice * 1.1f);
                        string randomSentence = _angryOfferSentences[Random.Range(0, _angryOfferSentences.Length)];
                        randomSentence = randomSentence.Replace("x$", customerNewOffer.ToString() + "$");
                        DealManager.Instance.SetCustomerDialogTMP("Bu abartı oldu, peki bu kadara ne dersin?");
                        DealManager.Instance.CustomerOffers(customerNewOffer);
                    }
                    else if (randomness <= 4)
                    {
                        DealManager.Instance.AcceptOffer(playerOffer);
                    }
                    else
                    {
                        RejectOfferFinishDeal();
                    }
                }
            }
            else if (phase == 1)
            {
                if (playerOffer <= (int)(customerOffer * 1.05f))
                {
                    bool acceptOffer = Random.Range(0, 10) > 2;
                    if (acceptOffer)
                    {
                        DealManager.Instance.AcceptOffer(playerOffer);
                    }
                    else
                    {
                        RejectOfferFinishDeal();
                    }
                }
            }
        }

        public void OnRejected()
        {
            DealManager.Instance.SetCustomerDialogTMP(_customerLeaveSentences[Random.Range(0, _customerLeaveSentences.Length)]);
            DealManager.Instance.CloseOtherThanCustomer();
            Invoke(nameof(Rejected), 2f);
        }

        private void Rejected()
        {
            gameObject.SetActive(false);
            /* CustomerManager.Instance.RemoveCustomer(); */
            DealManager.Instance.OnEndByReject();
        }

        [SerializeField] private string[] _customerLeaveSentences;

        private void RejectOfferFinishDeal()
        {
            DealManager.Instance.SetCustomerDialogTMP(_customerLeaveSentences[Random.Range(0, _customerLeaveSentences.Length)]);
            DealManager.Instance.CloseOtherThanCustomer();
            Invoke(nameof(Leave), 2f);
        }

        private void Leave()
        {
            /* CustomerManager.Instance.RemoveCustomer(); */
            gameObject.SetActive(false);
            DealManager.Instance.OnCustomerLeaves();
        }
    }
}