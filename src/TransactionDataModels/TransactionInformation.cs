using System;
using System.Collections;
using System.Collections.Generic;

namespace TransactionDataModels
{
    public class TransactionInformation
    {
        public bool ReturnStatus { get; set; }
        public IList<string> ReturnMessage { get; set; }
        public Hashtable ValidationErrors;
        public int TotalPages;
        public int TotalRows;
        public int PageSize;
        public bool IsAuthenicated;

        public TransactionInformation()
        {
            ReturnMessage = new List<string>();
            ReturnStatus = true;
            ValidationErrors = new Hashtable();
            TotalPages = 0;
            TotalPages = 0;
            PageSize = 0;
            IsAuthenicated = false;
        }

        public void AddExceptionMessage(Exception exception)
        {
            string errorMessage = exception.Message;
            ReturnStatus = false;
            ReturnMessage.Add(errorMessage);
        }
    }
}
