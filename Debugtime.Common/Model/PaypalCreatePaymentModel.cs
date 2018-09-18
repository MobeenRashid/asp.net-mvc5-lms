namespace Debugtime.Common.Model
{
    public class PaypalCreatePaymentModel
    {
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Shiping { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public string TransactionDescription { get; set; }
        public string InvoiceNumber { get; set; }
    }
}