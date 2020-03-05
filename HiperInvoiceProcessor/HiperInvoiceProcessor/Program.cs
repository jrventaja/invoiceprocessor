using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using HiperInvoiceProcessor.Util;
namespace HiperInvoiceProcessor
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var inputFilePath = Configuration.GetSection("AppConfiguration").GetSection("InputFilePath").Value;
            var outputDirectory = Configuration.GetSection("AppConfiguration").GetSection("OutputDirectory").Value;
            var lessOrEq6PagesFileName = Configuration.GetSection("AppConfiguration").GetSection("LessOrEq6PagesFileName").Value;
            var lessOrEq12PagesFileName = Configuration.GetSection("AppConfiguration").GetSection("LessOrEq12PagesFileName").Value;
            var moreThan12PagesFileName = Configuration.GetSection("AppConfiguration").GetSection("MoreThan12PagesFileName").Value;
            var zeroedAmountInvoicesFileName = Configuration.GetSection("AppConfiguration").GetSection("ZeroedAmountInvoicesFileName").Value;
            var inputStreamReader = new StreamReader(inputFilePath);
            var lessOrEq6PagesFileWriter = new StreamWriter(outputDirectory + lessOrEq6PagesFileName);
            var lessOrEq12PagesFileWriter = new StreamWriter(outputDirectory + lessOrEq12PagesFileName);
            var moreThan12PagesFileWriter = new StreamWriter(outputDirectory + moreThan12PagesFileName);
            var zeroedAmountFileWriter = new StreamWriter(outputDirectory + zeroedAmountInvoicesFileName);
            
            var headerLine = inputStreamReader.ReadLine();
            zeroedAmountFileWriter.WriteLine(headerLine);
            lessOrEq6PagesFileWriter.WriteLine(headerLine);
            lessOrEq12PagesFileWriter.WriteLine(headerLine);
            moreThan12PagesFileWriter.WriteLine(headerLine);

            while (!inputStreamReader.EndOfStream)
            {
                var line = inputStreamReader.ReadLine();

                var invoiceRecord = InvoiceUtil.ParseLine(line);
                if (invoiceRecord.Valid)
                {
                    if (invoiceRecord.Amount == 0.00m)
                    {
                        zeroedAmountFileWriter.WriteLine(InvoiceUtil.TransformRecordToCsvLine(invoiceRecord));
                    }
                    else
                    {
                        if (invoiceRecord.PagesNumber <= 6)
                        {
                            lessOrEq6PagesFileWriter.WriteLine(InvoiceUtil.TransformRecordToCsvLine(invoiceRecord));
                        }
                        if (invoiceRecord.PagesNumber <= 12)
                        {
                            lessOrEq12PagesFileWriter.WriteLine(InvoiceUtil.TransformRecordToCsvLine(invoiceRecord));

                        }
                        if (invoiceRecord.PagesNumber > 12)
                        {
                            moreThan12PagesFileWriter.WriteLine(InvoiceUtil.TransformRecordToCsvLine(invoiceRecord));
                        }
                    }
                }

            }
            zeroedAmountFileWriter.Close();
            lessOrEq6PagesFileWriter.Close();
            lessOrEq12PagesFileWriter.Close();
            moreThan12PagesFileWriter.Close();
        }
    }
}
