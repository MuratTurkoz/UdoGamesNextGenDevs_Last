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

        private void Awake()
        {
            Instance = this;
        }

        public void SendCustomer()
        {
            customer.MoveToPlayer(_dealStandingTransform.position);
        }
    }
}