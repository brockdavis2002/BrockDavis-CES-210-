using System;

//makes the job file publick
public class Job
{
    //makes public stings
    public string _jobtTitle;
    public string _company;
    public int _startingYear;
    public int _endingYear;

    public void Display()
    {
        Console.WriteLine($"{_jobtTitle} ({_company}) {_startingYear}-{_endingYear}");
    }
}