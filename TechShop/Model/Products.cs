namespace TechShop.Model
{
    public class Products
    {
        private int _productId;
        private string? _productName;
        private string? _description;
        private decimal _price;
        private int _stockQuantity;

        // Properties
        public int ProductID
        {
            get { return _productId; }
            set { _productId = value; }
        }

        public string? ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int StockQuantity
        {
            get { return _stockQuantity; }
            set { _stockQuantity = value; }
        }

        public Products() { }
        public Products(int productId, string? productName, string? description, decimal price, int stockQuantity)
        {
            _productId = productId;
            _productName = productName;
            _description = description;
            _price = price;
            _stockQuantity = stockQuantity;
        }

        public override string ToString()
        {
            return $"Product ID: {ProductID}, Name: {ProductName}, Price: {Price:C}, Stock Quantity: {StockQuantity}";
        }
    }
}
