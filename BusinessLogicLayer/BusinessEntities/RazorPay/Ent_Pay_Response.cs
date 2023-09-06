using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.RazorPay
{
    public class Ent_Pay_Response
    {
        private string transactionId { get; set; }
        private string status { get; set; }
        private string message { get; set; }
        private string errorMessage { get; set; }
        private string responceTransactionId { get; set; }
        private double amount { get; set; }
        private DateTime transactionTime { get; set; }
        private string orderId { get; set; }
        private int createdBy { get; set; }
        private string iPAddress { get; set; }
        private string userNo { get; set; }
        private string mACAddress { get; set; }

        public string UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public string ResponceTransactionId
        {
            get { return responceTransactionId; }
            set { responceTransactionId = value; }
        }

        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public DateTime TransactionTime
        {
            get { return transactionTime; }
            set { transactionTime = value; }
        }

        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public string IPAddress
        {
            get { return iPAddress; }
            set { iPAddress = value; }
        }

        public string MACAddress
        {
            get { return mACAddress; }
            set { mACAddress = value; }
        }
    }
}
