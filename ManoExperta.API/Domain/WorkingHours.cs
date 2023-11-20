namespace ManoExperta.API.Domain;

// public class Location 
// {
//     public string? Address { get; set; }
//     public string? City { get; set; }
//     public string? State { get; set; }
//     public string? Country { get; set; }
//     public string? ZipCode { get; set; }
// }

public class WorkingHours
{
    public WorkingHours(int day, int start, int end)
    {
        if(day < 1 || day > 7)
            throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 7");

        if(start < 0 || start > 23)
            throw new ArgumentOutOfRangeException(nameof(start), "Start time must be between 0 and 23");

        if(end < 0 || end > 23)
            throw new ArgumentOutOfRangeException(nameof(end), "End time must be between 0 and 23");

        Day = day;
        Start = start;
        End = end;
    }

    public Guid Id { get; set; }
    public int Day { get; init; }
    public int Start { get; init; }
    public int End { get; init; }
}
