﻿using EFCoreCodeFirstDemo.Entities;
namespace EFCoreCodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new EFCoreDbContext())
            {
                //try
                //{
                //    Console.WriteLine("\nExplicit Loading Student Related Data\n");

                //    // Load a student (only student data is loaded initially)
                //    var student = context.Students.FirstOrDefault(s => s.StudentId == 1);

                //    // Display basic student information
                //    if (student != null)
                //    {
                //        Console.WriteLine($"\nStudent Id: {student.StudentId}, Name: {student.FirstName} {student.LastName}, Gender: {student.Gender} \n");

                //        // Explicitly load the Branch navigation property for the student
                //        context.Entry(student).Reference(s => s.Branch).Load();

                //        // Check if Branch is null before accessing its properties
                //        if (student.Branch != null)
                //        {
                //            Console.WriteLine($"\nBranch Location: {student.Branch.BranchLocation}, Email: {student.Branch.BranchEmail}, Phone: {student.Branch.BranchPhoneNumber} \n");
                //        }
                //        else
                //        {
                //            Console.WriteLine("\nBranch data not available.\n");
                //        }

                //        // Explicitly load the Branch navigation property for the student
                //        context.Entry(student).Reference(s => s.Branch).Load();

                //        // Check if Branch is null before accessing its properties
                //        if (student.Branch != null)
                //        {
                //            Console.WriteLine($"\nBranch Location: {student.Branch.BranchLocation}, Email: {student.Branch.BranchEmail}, Phone: {student.Branch.BranchPhoneNumber} \n");
                //        }
                //        else
                //        {
                //            Console.WriteLine("\nBranch data not available.\n");
                //        }
                //    }
                //    else
                //    {
                //        Console.WriteLine("Student data not found.");
                //    }
                //}
                //catch (Exception ex)
                //{
                //    // Handle any errors that occur during data retrieval
                //    Console.WriteLine($"An error occurred: {ex.Message}");
                //}
            }
        }
    }
}