using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UdoGames.NextGenDev
{
    public class CustomerManager : MonoBehaviour
    {
        [SerializeField] private Transform _dealStandingTransform;
        public static CustomerManager Instance { get; private set; }

        [SerializeField] private Customer customer;

        private List<Customer> _customers;

        private void Awake()
        {
            Instance = this;
            _customers = new List<Customer>();
            _customers.Add(customer);
        }

        public void CreateCustomers()
        {
            int itemCount = FindObjectOfType<BaseInventory>().ItemCount();
            for (int i = 0; i < itemCount - 1; i++)
            {
                _customers.Add(Instantiate(customer));
                _customers[_customers.Count -1].transform.position = Vector3.zero;
                _customers[_customers.Count -1].gameObject.SetActive(false);
            }
        }

        public void RemoveCustomer()
        {
            customer.gameObject.SetActive(false);
            customer.transform.position = Vector3.zero;
            _customers.Remove(customer);
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