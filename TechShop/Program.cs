using system;
class program
{
    static void main(string[] args)
    {
        #region price quantity calculation
        console.write("enetr the price of item: ");
        int price = convert.toint32(console.readline());
        console.write("enter the quantity: ");
        int quantity = convert.toint32(console.readline());
        int totalprice = price * quantity;
        int discount = 0;
        if (totalprice > 50000)
        {
            discount = totalprice / 10;
            totalprice = totalprice - discount;
        }
        else
        {
            console.write("no discount ");
        }
        int gst = (totalprice * 18) / 100;
        int finalamount = totalprice + gst;
        console.write($"final amount is::{finalamount}");
        #endregion
        #region discount if else
        console.writeline("enter loyaltypoints: ");
        int loyaltypoints = convert.toint32(console.readline());
        console.writeline("enetr totalpurchases: ");
        int totalpurchases = convert.toint32(console.readline()); ;
        if (loyaltypoints > 100 && totalpurchases > 1000)
        {
            console.writeline("eligible for special discount");
        }
        else
        {
            console.writeline("not eligible for special discount");
        }
        #endregion
        #region switch case stock managing
        int stock = 10;
        while (true)
        {
            console.writeline("--------choose an option------------");
            console.writeline("1. check available stock");
            console.writeline("2. buy product");
            console.writeline("3. return product");
            console.writeline("4. exit");
            console.write("enter your choice: ");
            int choice = convert.toint32(console.readline());
            switch (choice)
            {
                case 1:
                    console.writeline($"available stock: {stock}");
                    break;

                case 2:
                    console.write("enter the quantity to buy: ");
                    int quantitytobuy = convert.toint32(console.readline());
                    if (quantitytobuy < 1)
                    {
                        console.writeline("quantity must be at least 1.");
                    }
                    else if (quantitytobuy > stock)
                    {
                        console.writeline("insufficient stock available.");
                    }
                    else
                    {
                        stock -= quantitytobuy;
                        console.writeline($"success: you bought {quantitytobuy} units. remaining stock: {stock}");
                    }
                    break;
                case 3:
                    console.write("enter the quantity to return: ");
                    int quantitytoreturn = convert.toint32(console.readline());
                    if (quantitytoreturn < 1)
                    {
                        console.writeline("error: quantity must be at least 1.");
                    }
                    else
                    {
                        stock += quantitytoreturn;
                        console.writeline($"success: you returned {quantitytoreturn} units. updated stock: {stock}");
                    }
                    break;
                case 4:
                    console.writeline("exiting the program. goodbye!");
                    return;
                default:
                    console.writeline("invalid choice. please try again.");
                    break;


            }
        }
        #endregion
        #region order status checking array and switch
        int numberoforders = 5;
        int[] ordernumbers = new int[numberoforders];
        string[] orderstatuses = new string[numberoforders];
        ordernumbers[0] = 101; orderstatuses[0] = "shipped";
        ordernumbers[1] = 102; orderstatuses[1] = "in transit";
        ordernumbers[2] = 103; orderstatuses[2] = "delivered";
        ordernumbers[3] = 104; orderstatuses[3] = "pending";
        ordernumbers[4] = 105; orderstatuses[4] = "cancelled";
        while (true)
        {
            console.writeline("-------choose an option---------");
            console.writeline("1.check order status");
            console.writeline("2.exit");
            console.write("enter your choice: ");
            int choice = convert.toint32(console.readline());
            switch (choice)
            {
                case 1:
                    console.write("enter your order number: ");
                    int ordernumber = convert.toint32(console.readline());
                    bool orderfound = false;
                    for (int i = 0; i < numberoforders; i++)
                    {
                        if (ordernumbers[i] == ordernumber)
                        {
                            console.writeline($"order #{ordernumber} status: {orderstatuses[i]}");
                            orderfound = true;
                            break;
                        }
                    }
                    if (!orderfound)
                    {
                        console.writeline("invalid order number. please try again.");
                    }
                    break;
                case 2:
                    console.writeline("exiting the program. goodbye!");
                    return;
                default:
                    console.writeline("invalid choice. please try again.");
                    break;
            }
        }
        #endregion

    }
}

