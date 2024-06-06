public class WritingAssignment : Assignment
{
    private string _title;


    public WritingAssignment(string studentName, string topic, string title)
        : base(studentName, topic)
    {
        // variables specific to the WritingAssignment class
        _title = title;
    }

    public string GetWritingInformation()
    {
 
        string studentName = GetStudentName();

        return $"{_title} by {studentName}";
    }
}