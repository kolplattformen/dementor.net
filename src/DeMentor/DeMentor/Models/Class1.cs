using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeMentor.Models;

// CalendarItem myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
public class CalendarItem
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("o365EventId")]
    public object O365EventId { get; set; }

    [JsonPropertyName("googleEventId")]
    public object GoogleEventId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("calendarEntryTypeId")]
    public int CalendarEntryTypeId { get; set; }

    [JsonPropertyName("isAllDayEvent")]
    public bool IsAllDayEvent { get; set; }

    [JsonPropertyName("startDateFull")]
    public DateTime StartDateFull { get; set; }

    [JsonPropertyName("endDateFull")]
    public DateTime EndDateFull { get; set; }

    [JsonPropertyName("startDate")]
    public string StartDate { get; set; }

    [JsonPropertyName("endDate")]
    public string EndDate { get; set; }

    [JsonPropertyName("formattedStartDate")]
    public string FormattedStartDate { get; set; }

    [JsonPropertyName("formattedEndDate")]
    public string FormattedEndDate { get; set; }

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }

    [JsonPropertyName("hasAttachments")]
    public bool HasAttachments { get; set; }

    [JsonPropertyName("subjects")]
    public List<CalendarItemSubject> Subjects { get; set; }

    [JsonPropertyName("courses")]
    public List<object> Courses { get; set; }

    [JsonPropertyName("url")]
    public object Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class CalendarItemSubject
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

// TimeTableItemNotes myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
public class TimeTableItemNotes
{
    [JsonPropertyName("roomInfo")]
    public string RoomInfo { get; set; }

    [JsonPropertyName("timetableNotes")]
    public string TimetableNotes { get; set; }

    [JsonPropertyName("tutors")]
    public string Tutors { get; set; }
}

public class TimeTableItem
{
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }

    [JsonPropertyName("end")]
    public DateTime End { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }

    [JsonPropertyName("notes")]
    public TimeTableItemNotes Notes { get; set; }

    [JsonPropertyName("allDay")]
    public bool AllDay { get; set; }

    [JsonPropertyName("establishmentName")]
    public object EstablishmentName { get; set; }

    [JsonPropertyName("details")]
    public string Details { get; set; }
}

