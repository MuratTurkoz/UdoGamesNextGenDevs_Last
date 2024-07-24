using System.Collections;
using System.Collections.Generic;
using MelonMath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UdoGames.NextGenDev
{
    public class DealManager : MonoBehaviour
    {
        public static DealManager Instance { get; private set; }

        private BaseInventory _inventory;

        [Header("UI")]
        [SerializeField] private TextMeshProUGUI _playerDealTMP;
        [SerializeField] private Button _upperDealBtn;
        [SerializeField] private Button _lowerDealBtn;
        [SerializeField] private Button _acceptOfferBtn;
        [SerializeField] private TextMeshProUGUI _btnDealPriceTMP;
        [SerializeField] private Button _rejectOfferBtn;
        [SerializeField] private Button _newOfferBtn;
        [SerializeField] private GameObject _dealOptions;
        [SerializeField] private GameObject _dealPanel;
        [SerializeField] private GameObject _playerOfferPanel;

        [Header("UI Customer")]
        [SerializeField] private TextMeshProUGUI _customerOfferTMP;
        [SerializeField] private TextMeshProUGUI _customerDialogTMP;

        [Header("UI Item")]
        [SerializeField] private TextMeshProUGUI _itemNameTMP;
        [SerializeField] private TextMeshProUGUI _itemEstimatedPriceTMP;
        [SerializeField] private Image _itemIcon;

        private Customer _currentCustomer;
        private ItemSO _currentItem;

        private void Awake()
        {
            Instance = this;
            _acceptOfferBtn.onClick.AddListener(AcceptOffer);
            _rejectOfferBtn.onClick.AddListener(RejectOfferEndDeal);
            _newOfferBtn.onClick.AddListener(GiveNewOffer);
            _upperDealBtn.onClick.AddListener(UpperDeal);
            _lowerDealBtn.onClick.AddListener(LowerDeal);
            _inventory = FindObjectOfType<BaseInventory>();
        }

        private void LowerDeal()
        {
            ChangeDeal(-1);
        }

        private void UpperDeal()
        {
            ChangeDeal(1);
        }

        private void ChangeDeal(int amount)
        {
            _playerOffer += amount;
            _playerDealTMP.SetText(NumberConverter.ConvertToString(_playerOffer));
        }

        public void CheckForCustomer()
        {
            if (_inventory.ItemCount() <= 0)
            {
                ShowEndDay();
                return;
            }
            else if (CustomerManager.Instance.CustomerCount() <= 0)
            {
                ShowEndDay();
                return;
            }

            CustomerManager.Instance.SendCustomer();
        }

        [SerializeField] private GameObject _dealItemPanel;

        public void StartDeal(Customer customer)
        {
            _dealPhase = 0;
            _currentCustomer = customer;
            _currentItem = _inventory.GetRandomItem();
            _playerOffer = _currentItem.EstimatedPrice;
            _dealItemPanel.SetActive(true);
            ShowDealPanel();
            _customerOffer = _currentCustomer.GivePrice(_currentItem.EstimatedPrice);
            ShowFirstPrice(_customerOffer);
            ShowDealOptions();
            OpenNewOffer();
        }

        private int _dealPhase;
        private int _customerOffer;
        private int _playerOffer;

        private void ShowDealPanel()
        {
            _customerOfferTMP.gameObject.SetActive(true);
            _playerDealTMP.SetText(NumberConverter.ConvertToString(_currentItem.EstimatedPrice));

            _itemEstimatedPriceTMP.SetText("Estimated Price: " + NumberConverter.ConvertToString(_currentItem.EstimatedPrice));
            _itemIcon.sprite = _currentItem.Icon;
            _itemNameTMP.SetText(_currentItem.ItemName);

            _dealOptions.SetActive(false);
            _dealPanel.SetActive(true);
        }

        private void CloseDealPanel()
        {
            _dealPanel.SetActive(false);
        }

        public void CloseOtherThanCustomer()
        {
            _dealItemPanel.SetActive(false);
            _dealOptions.SetActive(false);
            _playerOfferPanel.SetActive(false);
        }

        private void ShowFirstPrice(int givenPrice)
        {
            _customerOfferTMP.SetText("Customer Offers: " + NumberConverter.ConvertToString(givenPrice));
            _btnDealPriceTMP.SetText(NumberConverter.ConvertToString(givenPrice));
        }

        private void ShowDealOptions()
        {
            _dealOptions.SetActive(true);
        }

        private void CloseDealOptions()
        {
            _dealOptions.SetActive(false);
        }

        private void AcceptOffer()
        {
            _customerOfferTMP.gameObject.SetActive(false);
            CurrencyManager.Instance.AddGold(_customerOffer, "Sell " + _currentItem.name);
            _inventory.RemoveItem(_currentItem);
            //CloseDealPanel();
            CloseOtherThanCustomer();
            /* CustomerManager.Instance.RemoveCustomer(); */
            _customerDialogTMP.SetText("Anlaştık");
            Invoke(nameof(OnDealEnd), 2f);
        }

        private void OnDealEnd()
        {
            CustomerManager.Instance.RemoveCustomer();
            CloseDealPanel();
            if (_inventory.ItemCount() > 0 && CustomerManager.Instance.CustomerCount() > 0)
            {
                CustomerManager.Instance.SendCustomer();
            }
            else
            {
                ShowEndDay();
            }
        }

        public void AcceptOffer(int offer)
        {
            _customerOfferTMP.gameObject.SetActive(false);
            CurrencyManager.Instance.AddGold(offer, "Sell " + _currentItem.name);
            _inventory.RemoveItem(_currentItem);
            //CloseDealPanel();
            CloseOtherThanCustomer();
            _customerDialogTMP.SetText("Anlaştık he");
            Invoke(nameof(OnDealEnd), 2f);
        }

        private void GiveNewOffer()
        {
            CloseDealOptions();
            CloseNewOffer();
            _currentCustomer.GiveOffer(_playerOffer, _dealPhase);
            _dealPhase++;
        }

        private void OpenNewOffer()
        {
            _newOfferBtn.gameObject.SetActive(true);
            _playerOfferPanel.SetActive(true);
        }

        public void CloseNewOffer()
        {
            _newOfferBtn.gameObject.SetActive(false);
            _playerOfferPanel.SetActive(false);
        }

        private void RejectOfferEndDeal()
        {
            _customerOfferTMP.gameObject.SetActive(false);
            _currentCustomer.OnRejected();
        }

        public void SetCustomerDialogTMP(string text)
        {
            _customerDialogTMP.SetText(text);
        }

        public void OnCustomerLeaves()
        {
            OnEndByReject();
        }

        public void OnEndByReject()
        {
            _customerOfferTMP.gameObject.SetActive(false);
            CloseDealPanel();
            OnDealEnd();
        }

        public void CustomerOffers(int customerOffer)
        {
            _customerOffer = customerOffer;
            ShowFirstPrice(customerOffer);
            ShowDealOptions();
        }

        private void ShowEndDay()
        {
            CloseDealPanel();
            UIManager.Instance.ShowEndDayBtn();
            UIManager.Instance.ShowBtns();
        }
    }
}