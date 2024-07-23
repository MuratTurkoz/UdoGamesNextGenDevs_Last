using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private Transform _dealStandingTransform;
        public static CustomerManager Instance { get; private set; }

        [SerializeField] private Customer _customerPrefab;

        private Customer customer;

        private List<Customer> _customers;

        private void Awake()
        {
            Instance = this;
            _customers = new List<Customer>();
        }

        public void CreateCustomers()
        {
            int itemCount = FindObjectOfType<BaseInventory>().ItemCount();
            for (int i = 0; i < itemCount; i++)
            {
                _customers.Add(Instantiate(_customerPrefab));
                _customers[_customers.Count -1].transform.position = Vector3.zero;
                _customers[_customers.Count -1].gameObject.SetActive(false);
            }
        }

        public void RemoveCustomer()
        {
            _customers.Remove(customer);
            Destroy(customer.gameObject);
        }

        public void SendCustomer()
        {
            customer = _customers[0];
            customer.gameObject.SetActive(true);
            customer.MoveToPlayer(_dealStandingTransform.position);
        }

        public int CustomerCount()
        {
            return _customers.Count;
        }
    }
}