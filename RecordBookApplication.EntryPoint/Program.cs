using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordBookApplication.EntryPoint
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new();
            //Create Helper class for Sorting & Searching
            bool quit = false;
            do
            {
                book.Records.Clear();
                ReadDatabase(book);
                Console.WriteLine("1. Show all students\n2. Enter new student \n3. Exit");
                Int32.TryParse(Console.ReadLine(), out int menu);
                switch (menu)
                {
                    case 1: // Show all students
                        int index = 1;
                        foreach (var item in book.Records)
                        {
                            Console.WriteLine($"[{index}] {item.Student.Name}");
                            index++;
                        }
                        Console.WriteLine("Select student");
                        Int32.TryParse(Console.ReadLine(), out int choice);
                        if (choice > 0 && choice <= book.Records.Count)
                        {
                            book.Records[choice - 1].ComputeStatistics();
                        }
                        else
                        {
                            Console.WriteLine("Invalid input");
                        }
                        break;
                    case 2: // Add student
                        Console.Write("Enter a name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter 5 grades like (12,4 13,4 123,4 14,5 89,5");
                        string grades = "";
                        for (int i = 0; i < 5; i++)
                        {
                            Console.WriteLine("Enter a number");
                            while (true)
                            {
                                if (Double.TryParse(Console.ReadLine(), out double num))
                                {
                                    grades += num + " ";
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input");
                                }
                            }


                        }
                        string[] lines = { " ", $"Name: {name}", $"Grades: {grades}" };
                        SaveToDataBase(lines);


                        break;
                    case 3: // Quit
                        quit = true;
                        break;
                    default:
                        break;
                }
            } while (!quit);



            /*
             * Create a simple menu
             *  1. All Students:
             *      a. Student names ex. Ralph
             *          Name
             *          Statistics
             *  2. Enter New Student (CREATE: SaveToTextFile() )
             *          a. Enter a name
             *                &&
             *          b. Enter Grades
             *  3. Quit:
             *          
             * **/





        }

        private static void ReadDatabase(Book book)
        {
            var lines = File.ReadLines(Path.GetFullPath("grades.txt")).ToList();

            for (int i = 0; i < lines.Count - 1; i++)
            {
                if (lines[i].Contains("Name: "))
                {
                    var indexOfColon = lines[i].IndexOf(" ");
                    var name = lines[i].Substring(indexOfColon).Trim();
                    var colon = lines[i + 1].IndexOf(" ");
                    var grades = lines[i + 1].Substring(colon).Trim();

                    var listOfDoubles = ToDoubleList(grades);

                    book.AddRecord(new RecordBook() { Student = new Student() { Name = name, Grades = listOfDoubles } });
                }
            }
        }

        private static void SaveToDataBase(string[] lines)
        {
            File.AppendAllLines(Path.GetFullPath("grades.txt"), lines);
        }

        private static List<double> ToDoubleList(string grades)
        {
            var result = grades.Split(" ").ToList();
            var resultToDouble = result.ConvertAll(grade => Convert.ToDouble(grade.Replace(".", ",")));
            return resultToDouble;

        }

        private static void InsertionSort(List<double> records)
        {

        }

        private static int BinarySearch(List<double> records, double searchItem)
        {
            int min = 0;
            int max = records.Count - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchItem == records[mid])
                {
                    return records.IndexOf(++mid);
                }
                else if (searchItem < records[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
        public static async Task ExampleAsync(string inp)
        {
            using StreamWriter file = new(@"C:\Users\Fredr\Source\Repos\Bobbysonhjos\RecordBookApplication\RecordBookApplication.EntryPoint\TextFile1.txt", append: true);
            await file.WriteLineAsync(inp);
        }

        //private static void PrintRecordsList(List<double> records)
        //{
        //    foreach (var item in records)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}
    }
}
