﻿using DumDumPay.DI;

namespace DumDumPay.TestInConsole
{
    class Program
    {
        private const string EndPoint = "https://dumdumpay.docs.apiary.io";
        // TODO: Use Guid
        private const string MerchantId = "6fc3aa31-7afd-4df1-825f-192e60950ca1";
        private const string SecretKey = "53cr3t";
        
        static void Main(string[] args)
        {
            Bootstrapper.Start(EndPoint, MerchantId, SecretKey);
            
            Bootstrapper.Stop();
        }
    }
}