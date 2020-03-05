using System;
using System.Collections.Generic;
using System.Text;

namespace HiperInvoiceProcessor.Entity
{
    public class InvoiceRecord
    {
        public bool Valid;
        public string Name { get; set; }
        public string CEP { get; }
        public string Address { get; set; }
        public string Neighbourhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal Amount { get; set; }
        public int PagesNumber { get; }

        public InvoiceRecord(string cep, int pagesNumber)
        {
            CEP = cep;
            Valid = (cep.Length == 8 && cep != "00000000");
            PagesNumber = (pagesNumber % 2 == 0 ? pagesNumber : pagesNumber + 1);
        }

    }
}
