using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class DealManager : MonoBehaviour
    {
        public static DealManager Instance { get; private set; }

        [SerializeField] private BaseInventory _inventory;
        private Customer _currentCustomer;
        private ItemSO _currentItem;

        private void Awake()
        {
            Instance = this;
        }

        public void CheckForCustomer()
        {
            if (_inventory.ItemCount() <= 0)
            {
                ShowEndSelling();
                return;
            }

            CustomerManager.Instance.SendCustomer();
        }

        public void StartDeal(Customer customer)
        {
            _dealPhase = 0;
            _currentCustomer = customer;
            _currentItem = _inventory.GetRandomItem();
            ShowDealPanel();
            _customerOffer = _currentCustomer.GivePrice(_currentItem.EstimatedPrice, _dealPhase);
            ShowFirstPrice(_customerOffer);
            ShowDealOptions();
        }

        private int _dealPhase;
        private int _customerOffer;

        private void ShowDealPanel()
        {

        }

        private void CloseDealPanel()
        {

        }

        private void ShowFirstPrice(int givenPrice)
        {
            
        }

        private void ShowDealOptions()
        {

        }

        private void AcceptOffer()
        {
            CurrencyManager.Instance.AddGold(_customerOffer);
            _inventory.RemoveItem(_currentItem);
            CloseDealPanel();
            if (_inventory.ItemCount() > 0)
            {
                CustomerManager.Instance.SendCustomer();
            }
            else
            {
                ShowEndSelling();
            }
        }

        private void RejectOffer()
        {

        }

        private void ShowEndSelling()
        {
            
        }
    }
}