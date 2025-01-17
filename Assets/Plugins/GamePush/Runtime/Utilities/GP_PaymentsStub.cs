using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePush
{
    [CreateAssetMenu(fileName = "GP_PaymentsStub", menuName = "GP_Settings/GP_PaymentsStub")]
    public class GP_PaymentsStub : ScriptableObject
    {
        [SerializeField] private List<FetchProducts> products = new List<FetchProducts>();
        [SerializeField] private List<FetchPlayerPurchases> purchases = new List<FetchPlayerPurchases>();

        public IReadOnlyList<FetchProducts> Products => products;
        public IReadOnlyList<FetchPlayerPurchases> Purchases => purchases;
    }
}