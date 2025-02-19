﻿using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                try
                {
                    // Lazy Loading Example
                    Console.WriteLine("Lazy Loading Student and related data\n");

                    // Load a student (only student data is loaded initially)
                    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                    // Display basic student information
                    if (student != null)
                    {
                        Console.WriteLine($"\nStudent Id: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Gender: {student.Gender} \n");

                        //Disabling Lazy Loading Here
                        context.ChangeTracker.LazyLoadingEnabled = false;

                        // Check if Branch is null before accessing its properties
                        if (student.Branch != null)
                        {
                            Console.WriteLine($"\nBranch Location: {student.Branch.BranchLocation}, Email: {student.Branch.BranchEmail}, Phone: {student.Branch.BranchPhoneNumber} \n");
                        }
                        else
                        {
                            Console.WriteLine("\nBranch data not available.\n");
                        }

                        //Enabling Lazy Loading Here
                        context.ChangeTracker.LazyLoadingEnabled = true;
                        // Check if Address is null before accessing its properties
                        if (student.Address != null)
                        {
                            Console.WriteLine($"\nAddress: {student.Address.Street}, {student.Address.City}, {student.Address.State}, Pin: {student.Address.PostalCode} \n");
                        }
                        else
                        {
                            Console.WriteLine("\nAddress data not available.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Student data not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that occur during data retrieval
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            // Final Output
            Console.WriteLine("\nLazy loading of related entities completed.");
        }
    }
}