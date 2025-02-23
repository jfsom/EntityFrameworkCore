using Microsoft.EntityFrameworkCore;
using EFCoreCodeFirstDemo.Entities;
using System;
using System.Linq;

namespace EFCoreCodeFirstDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Adding categories and products to the database
                AddCategories();

                // Displaying all categories, subcategories, and products in level 3 categories
                DisplayCategories();
            }
            catch (DbUpdateException ex)
            {
                // Exception handling to catch database errors, showing the inner exception if available
                Console.WriteLine($"Database Error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                // Exception handling for any other errors
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Method to add categories and products to the database
        static void AddCategories()
        {
            using var context = new EFCoreDbContext();

            // Check if the database already has products to avoid duplication
            if (context.Products.Any())
            {
                Console.WriteLine("Products and Categories already exist in the database.\n");
                return;
            }

            // Creating categories (Level 1 → Level 2 → Level 3)
            var electronics = new Category { Name = "Electronics" }; // Level 1
            var computers = new Category { Name = "Computers", ParentCategory = electronics }; // Level 2
            var laptops = new Category { Name = "Laptops", ParentCategory = computers }; // Level 3

            var phones = new Category { Name = "Phones", ParentCategory = electronics }; // Level 2
            var smartPhones = new Category { Name = "Smartphones", ParentCategory = phones }; // Level 3

            // Creating categories (Level 1 → Level 2 → Level 3)
            var homeAppliances = new Category { Name = "Home Appliances" }; // Level 1
            var kitchen = new Category { Name = "Kitchen", ParentCategory = homeAppliances }; // Level 2
            var coffeeMachines = new Category { Name = "Coffee Machines", ParentCategory = kitchen }; // Level 3

            // Creating products for each category
            var product1 = new Product { Name = "Dell Laptop", Price = 899.99M, Category = laptops };
            var product2 = new Product { Name = "HP Laptop", Price = 799.99M, Category = laptops };
            var product3 = new Product { Name = "Espresso Machine", Price = 199.99M, Category = coffeeMachines };
            var product4 = new Product { Name = "iPhone 13", Price = 999.99M, Category = smartPhones };
            var product5 = new Product { Name = "Samsung Galaxy S21", Price = 899.99M, Category = smartPhones };

            // Adding categories and products to the database
            context.Categories.AddRange(electronics, computers, laptops, homeAppliances, kitchen, coffeeMachines, phones, smartPhones);
            context.Products.AddRange(product1, product2, product3, product4, product5);
            context.SaveChanges();

            // Confirmation message after seeding data
            Console.WriteLine("Categories and Products added successfully.\n");
        }

        // Method to fetch and display all categories, subcategories, and products for level 3 categories
        static void DisplayCategories()
        {
            using var context = new EFCoreDbContext();

            // Fetch top-level categories (ParentCategoryId is null) and include subcategories and products
            var categories = context.Categories
                .Include(c => c.Subcategories)
                    .ThenInclude(c => c.Subcategories)
                        .ThenInclude(c => c.Products)  // Include products for level 3 categories
                .Where(c => c.ParentCategoryId == null)  // Fetch only top-level categories
                .ToList();

            // Display all categories in a hierarchical format
            Console.WriteLine("All Categories, Subcategories, and Products (in level 3 categories):");
            foreach (var category in categories)
            {
                Console.WriteLine($"\nCategory: {category.Name}");  // Level 1
                foreach (var subcategory in category.Subcategories)
                {
                    Console.WriteLine($"  Subcategory: {subcategory.Name}");  // Level 2
                    foreach (var subSubcategory in subcategory.Subcategories)
                    {
                        Console.WriteLine($"    Sub-Subcategory: {subSubcategory.Name}");  // Level 3

                        // Display products under each Level 3 category
                        if (subSubcategory.Products.Any())
                        {
                            Console.WriteLine("         Products:");
                            foreach (var product in subSubcategory.Products)
                            {
                                Console.WriteLine($"            - {product.Name} (${product.Price})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("            No products in this category.");
                        }
                    }
                }
            }
            Console.WriteLine();  // New line for better output formatting
        }
    }
}