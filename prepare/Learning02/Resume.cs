using System;

//makes the Resume file public for useing in other documents in directory
public class Resume
{
    //makes the Inputed_name string public
    public string Inputed_name;

    // initializes list
    public List<Job> _jobs = new List<Job>();

    //makes Display public 
    public void Display()
    {
        //wrights the name and _jobs into the consel
        Console.WriteLine($"Name: {Inputed_name}");
        Console.WriteLine("_jobs:");

        // prints out each job in the loop
        foreach (Job job in _jobs)
        {
            // Displays each Job in the loop
            job.Display();
        }
    }
}