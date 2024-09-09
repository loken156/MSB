using bpac;
using Domain.Interfaces;
using Domain.Models.Label;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class LabelPrinterService : ILabelPrinterService
    {
        // Use Brother P-touch Editor to edit or create a label template, make sure that ObjectName has the same value as GetObject("ObjectName") in the code
        // Install the printer driver for the Brother printer
        // Configure the printer in Windows system settings to use the correct paper size and orientation
        // Run bPAC3CCISetup.msi on the client machine to be able to run printer service, for full SDK, use x86 version (x64 is not supported)
        // If the printer is returning a successful print but not printing, check that project is running on x86/Any cpu

        private readonly ILogger<LabelPrinterService> _logger;

        public LabelPrinterService(ILogger<LabelPrinterService> logger)
        {
            _logger = logger;
        }

        public Task PrintLabelAsync(LabelModel label)
        {
            return Task.Run(() => PrintLabel(label));
        }

        public void PrintLabel(LabelModel label)
        {
            Document doc = new Document();

            // Acquire the printer list
            object[] printers = null;
            try
            {
                printers = (object[])doc.Printer.GetInstalledPrinters();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting installed printers");
                throw;
            }

            // Acquire the printer name
            // This code will not work properly if there are multiple printers installed since it chooses the first printer in the list
            string printerName = printers?.Length > 0 ? printers[0].ToString() : null;

            if (printerName == null)
            {
                _logger.LogError("No printers installed");
                return;
            }

            // Check that the printer is online
            bool isPrinterOnline = false;
            try
            {
                isPrinterOnline = doc.Printer.IsPrinterOnline(printerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if printer is online");
                throw;
            }

            if (!isPrinterOnline)
            {
                _logger.LogError("Printer is offline");
                return;
            }

            // Replace with the path to your .lbx file
            string templatePath = @"C:\Users\latti\Documents\Skola\LiA2\Gitprj\Infrastructure\Resources\MagnificientStoreBuddies.lbx"; // Replace with correct path

            try
            {
                if (!doc.Open(templatePath))
                {
                    // Handle error
                    _logger.LogError("Error opening label template. Error code: {ErrorCode}", doc.ErrorCode);
                    return;
                }

                // Set the values of objects in the template
                // doc.GetObject("OrderNumber").Text = label.OrderNumber;
                // doc.GetObject("QRCode").Text = label.QRCode;
                // doc.GetObject("Barcode").Text = label.Barcode;
                string orderNumber = "1234123412"; // Replace with code above when in production
                string qrCode = "1234123412"; // Replace with code above when in production
                string barcode = "1234123412"; // Replace with code above when in production

                var orderNumberObject = doc.GetObject("OrderNumber");
                if (orderNumberObject != null)
                {
                    orderNumberObject.Text = orderNumber;
                }
                else
                {
                    _logger.LogError("OrderNumber object not found in template");
                }

                var qrCodeObject = doc.GetObject("QRCode");
                if (qrCodeObject != null)
                {
                    qrCodeObject.Text = qrCode;
                }
                else
                {
                    _logger.LogError("QRCode object not found in template");
                }

                var barcodeObject = doc.GetObject("Barcode");
                if (barcodeObject != null)
                {
                    barcodeObject.Text = barcode;
                }
                else
                {
                    _logger.LogError("Barcode object not found in template");
                }

                // Print the label
                doc.StartPrint("", PrintOptionConstants.bpoDefault);
                doc.PrintOut(1, PrintOptionConstants.bpoDefault);
                doc.EndPrint();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing label. Error code: {ErrorCode}", doc.ErrorCode);
                throw;
            }
            finally
            {
                doc.Close();
            }
        }
    }
}