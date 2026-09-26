using System;

class Program
{
    static void Main(string[] args)
    {
        Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
        Address address2 = new Address("456 Oak Ave", "Othertown", "ON", "Canada");
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Widget", 10.99, 2));
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Gadget", 15.99, 1));
        Order order3 = new Order(customer1);
        order3.AddProduct(new Product("Thingamajig", 5.99, 3));
        Order order4 = new Order(customer2);
        order4.AddProduct(new Product("Doohickey", 7.99, 4));
        Console.WriteLine($"Order Summary for {customer1.GetName()}:");
        Console.WriteLine($"Shipping cost: {order1.GetShippingCost()}");
        Console.WriteLine($"Total price: {order1.GetTotalPrice()}");
        Console.WriteLine($"Packing Label:\n{order1.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order1.GetShippingLabel()}");

        Console.WriteLine($"Order Summary for {customer2.GetName()}:");
        Console.WriteLine($"Shipping cost: {order2.GetShippingCost()}");
        Console.WriteLine($"Total price: {order2.GetTotalPrice()}");
        Console.WriteLine($"Packing Label:\n{order2.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order2.GetShippingLabel()}");

        Console.WriteLine($"Order Summary for {customer1.GetName()}:");
        Console.WriteLine($"Shipping cost: {order3.GetShippingCost()}");
        Console.WriteLine($"Total price: {order3.GetTotalPrice()}");
        Console.WriteLine($"Packing Label:\n{order3.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order3.GetShippingLabel()}");

        Console.WriteLine($"Order Summary for {customer2.GetName()}:");
        Console.WriteLine($"Shipping cost: {order4.GetShippingCost()}");
        Console.WriteLine($"Total price: {order4.GetTotalPrice()}");
        Console.WriteLine($"Packing Label:\n{order4.GetPackingLabel()}");
        Console.WriteLine($"Shipping Label:\n{order4.GetShippingLabel()}");

    }
}