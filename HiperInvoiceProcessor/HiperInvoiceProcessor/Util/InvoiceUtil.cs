using HiperInvoiceProcessor.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HiperInvoiceProcessor.Util
{
    public static class InvoiceUtil
    {
        const int NAMEINDEX = 0;
        const int CEPINDEX = 1;
        const int ADDRESSINDEX = 2;
        const int HOODINDEX = 3;
        const int CITYINDEX = 4;
        const int STATEINDEX = 5;
        const int AMOUNTINDEX = 6;
        const int PAGENUMINDEX = 7;
        const char DELIMITER = ';';

        public static InvoiceRecord ParseLine(string line)
        {
            var splittedLine = Regex.Split(line, ";(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            var invoiceRecord = new InvoiceRecord(splittedLine[CEPINDEX].Trim(), Int32.Parse(splittedLine[PAGENUMINDEX].Trim().Replace("\"", string.Empty)));
            invoiceRecord.Name = splittedLine[NAMEINDEX].Trim().Replace("\"", string.Empty);
            invoiceRecord.Address = splittedLine[ADDRESSINDEX].Trim().Replace("\"", string.Empty);
            invoiceRecord.Neighbourhood = splittedLine[HOODINDEX].Trim().Replace("\"", string.Empty);
            invoiceRecord.City = splittedLine[CITYINDEX].Trim().Replace("\"", string.Empty);
            invoiceRecord.State = splittedLine[STATEINDEX].Trim().Replace("\"", string.Empty);
            invoiceRecord.Amount = decimal.Parse(splittedLine[AMOUNTINDEX].Trim().Replace("\"", string.Empty));
            return invoiceRecord;
        }

        public static string TransformRecordToCsvLine(InvoiceRecord invoiceRecord)
        {
            return $"\"{invoiceRecord.Name}\";\"{invoiceRecord.CEP}\";\"{invoiceRecord.Address}\";\"{invoiceRecord.Neighbourhood}\";\"{invoiceRecord.City}\";\"{invoiceRecord.State}\";\"{invoiceRecord.Amount}\";\"{invoiceRecord.PagesNumber}\"";
        }
    }
}
