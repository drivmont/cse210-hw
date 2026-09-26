public class Product
{
    private string _name;
    private double _price;

    private string _productId;

    private int _quantity;

    public Product(string name, double price, int quantity)
    {
        _name = name;
        _price = price;
        _productId = Guid.NewGuid().ToString();
        _quantity = quantity;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }

    public double GetTotalCost()
    {
        return _price * _quantity;
    }

}
    
