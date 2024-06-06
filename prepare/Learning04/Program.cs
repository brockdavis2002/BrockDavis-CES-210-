using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a base Assignment object
        Assignment a1 = new Assignment("Bow wow", "Multiplication for Dogs");
        Console.WriteLine(a1.GetSummary());

        // Now create the derived class assignments
        MathAssignment a2 = new MathAssignment("Polly the parrot", "Bird Math", "7.3", "8-19");
        Console.WriteLine(a2.GetSummary());
        Console.WriteLine(a2.GetHomeworkList());

        WritingAssignment a3 = new WritingAssignment("Goldy the fish", "History of the Ocian", "The lockness monster");
        Console.WriteLine(a3.GetSummary());
        Console.WriteLine(a3.GetWritingInformation());
    }
}