namespace TechShop.Model
{
    public class OrderDetail
    {
        private int _orderDetailId;
        private Orders? _order; 
        private Products? _product; 
        private int _quantity;

        public int OrderDetailID
        {
            get { return _orderDetailId; }
            set { _orderDetailId = value; }
        }

        public Orders? Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public Products? Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public decimal Discount { get; internal set; }
        public decimal Price { get; internal set; }
        public int OrderID { get; internal set; }

        public OrderDetail() { }

        public OrderDetail(int orderDetailId, Orders? order, Products? product, int quantity)
        {
            _orderDetailId = orderDetailId;
            _order = order;
            _product = product;
            _quantity = quantity;
        }

        public override string ToString()
        {
            return $"Order Detail ID: {OrderDetailID}, " +
                   $"Order ID: {Order?.OrderID}, " +
                   $"Product: {Product?.ProductName}, " +
                   $"Quantity: {Quantity}, " +
                   $"Price: {Product?.Price:C}, " +
                   $"Total Price: {(Product?.Price ?? 0) * Quantity:C}";
        }
    }
}
