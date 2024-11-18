using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExample  
{
    class Product  
    {
        public string Name { get; set; }  
        public double Price { get; set; } 
        public string Category { get; set; }  
    }

    class Program  
    {
        static void Main(string[] args)
        {
            // Create a list of products
            List<Product> products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 800, Category = "Electronics" },
                new Product { Name = "Shirt", Price = 25, Category = "Clothing" },
                new Product { Name = "Smartphone", Price = 600, Category = "Electronics" },
                new Product { Name = "Headphones", Price = 100, Category = "Electronics" },
                new Product { Name = "Jeans", Price = 40, Category = "Clothing" },
                new Product { Name = "Tablet", Price = 200, Category = "Electronics" }
            };

            // LINQ query to find products in the "Electronics" category and order by price (ascending)
            var electronics = from p in products  // Start LINQ query to work with the 'products' list
                              where p.Category == "Electronics"  // Filter products by the "Electronics" category
                              orderby p.Price  // Order the filtered products by price in ascending order
                              select p;  // Select the product objects that match the query criteria

            // Display the filtered and sorted products
            Console.WriteLine("Electronics sorted by price:");
            foreach (var product in electronics)
            {
                Console.WriteLine($"{product.Name}: ${product.Price}");  
            }


            // LINQ query to find products with a price greater than 50
            var expensiveProducts = from p in products  
                                    where p.Price > 50  // Filter products by price greater than 50
                                    select p;  // Select the product objects that match the query criteria

            // Display the expensive products
            Console.WriteLine("\nProducts priced above $50:");
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"{product.Name}: ${product.Price}");  
            }


            // LINQ query to find the average price of all products
            var averagePrice = products.Average(p => p.Price);  // Use LINQ's Average function to calculate the average price of all products
            Console.WriteLine($"\nThe average price of all products is: ${averagePrice}");


            // LINQ query to group products by category and count how many products are in each category
            var groupedByCategory = from p in products  
                                    group p by p.Category into productGroup  // Group the products by their Category
                                    select new  // Create a new anonymous object for each category group
                                    {
                                        Category = productGroup.Key,  // The key (category) of the group
                                        Count = productGroup.Count()  // The count of products in that category
                                    };

            // Display the groups and their product counts
            Console.WriteLine("\nProduct count by category:");
            foreach (var group in groupedByCategory)
            {
                Console.WriteLine($"{group.Category}: {group.Count} products");
            }
        }
    }
    /*
     
    Electronics sorted by price:
    Headphones: $100
    Tablet: $200
    Laptop: $800
    Smartphone: $600

    Products priced above $50:
    Laptop: $800
    Smartphone: $600
    Headphones: $100
    Tablet: $200

    The average price of all products is: $261.67

    Product count by category:
    Electronics: 4 products
    Clothing: 2 products

     */
}
