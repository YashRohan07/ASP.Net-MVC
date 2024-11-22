using System;
using System.Linq;

namespace LINQ_Student
{
    // Define the Student class
    public class Student
    {
        // Properties for student details
        public int Id { get; set; }
        public string Name { get; set; }
        public float Cgpa { get; set; }

        // Method to display student details
        public void Show()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}, CGPA: {Cgpa}");
        }
    }

    internal class Program
    {
        // Method to print the details of each student in an array
        static void printStudents(Student[] students)
        {
            // Loop through each student in the array
            foreach (var s in students)
            {
                s.Show();  // Call the Show method on the student object to display the student details
            }
        }

        // Method to create and return an array of Student objects
        static Student[] getAll()
        {
            // Declare an array of Student objects with a length of 5
            Student[] students = new Student[5];

            // Declare an array of float values representing CGPA scores of students
            float[] cgpas = new float[] { 3.45f, 2.34f, 3.76f, 3.78f, 3.10f };

            for (int i = 0; i < 5; i++)  // Fixed index to start from 0 for array indexing
            {
                // Create a new Student object for each iteration and assign it to the students array
                students[i] = new Student()
                {
                    Id = i + 1,  // Set the student's ID to the current index + 1
                    Name = "Student " + (i + 1),  // Name for student
                    Cgpa = cgpas[i],  // Assign CGPA value from the array
                };
            }

            // Return the array of students
            return students;
        }

        static void Main(string[] args)
        {
            // Call the getAll method to retrieve the array of student objects and store it in 'data'
            var data = getAll();

            // Use LINQ query to filter the students with CGPA greater than or equal to 3.75
            var filtered = (from s in data  // Declare a LINQ query to select students from the 'data' collection
                            where s.Cgpa >= 3.75
                            select s)  // Select the student object
                            .ToList();  // Convert the result into a List<Student> object

            // Print filtered students
            Console.WriteLine("\nFiltered Students (CGPA >= 3.75):");
            printStudents(filtered.ToArray());  // Print the filtered list of students
        }
    }
}

/*
 
Filtered Students (CGPA >= 3.75):
ID: 3, Name: Student 3, CGPA: 3.76
ID: 4, Name: Student 4, CGPA: 3.78

 */