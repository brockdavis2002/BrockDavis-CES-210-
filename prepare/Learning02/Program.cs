using System;

class Program
{
    static void Main(string[] args)
    {
        //lsit of _jobs and information reated to it
        Job job1 = new Job();
        job1._jobtTitle = "Dinosaur caretaker";
        job1._company = "Jarasic Park";
        job1._startingYear = 2029;
        job1._endingYear = 2039;

        Job job2 = new Job();
        job2._jobtTitle = "CEO";
        job2._company = "Jarasic World";
        job2._startingYear = 2039;
        job2._endingYear = 2044;

        Job job3 = new Job();
        job3._jobtTitle = "Astronot";
        job3._company = "Arospace Tech";
        job3._startingYear = 2044;
        job3._endingYear = 2080;


        Resume myResume = new Resume();
        myResume.Inputed_name = "Billy Bob";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);
        myResume._jobs.Add(job3);

        myResume.Display();
    }
}