using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Calendar.Models;
using System.Globalization;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
namespace sample;
public partial class MainPageViewModel : ObservableObject
{
    ObservableCollection<CalendarEvent> eventCollection;
    public EventCollection Events { get; set; }
    public CultureInfo Culture => new CultureInfo("cs-CZ");

    [ObservableProperty]
    private bool isAddEventButtonVisible;

    [ObservableProperty]
    private DateTime? selectedDate;

    [ObservableProperty]
    private DateTime displayDate;
    public MainPageViewModel()
    {
        Events = new EventCollection { };
        SelectedDate = DateTime.Now;
        DisplayDate = DateTime.Now;
        IsAddEventButtonVisible = false;
        eventCollection = new ObservableCollection<CalendarEvent>
        {
            new CalendarEvent
            {
                diagnosisId = "D001",
                diagnosisName = "Annual Check-up",
                name = "Physical Examination",
                date = DateTime.Now.Date,
                location = "Main Hospital, Room 302",
                description = "Complete annual health assessment with Dr. Smith",
                color = "Blue"
            },
            new CalendarEvent
            {
                diagnosisId = "D002",
                diagnosisName = "Dental Care",
                name = "Teeth Cleaning",
                date = DateTime.Now.Date.AddDays(3),
                location = "Dental Clinic, 5th Floor",
                description = "Regular dental cleaning and check-up",
                color = "Green"
            },
            new CalendarEvent
            {
                diagnosisId = "D003",
                diagnosisName = "Follow-up",
                name = "Lab Results Review",
                date = DateTime.Now.Date.AddDays(-2),
                location = "Medical Center, Wing B",
                description = "Review blood test results with specialist",
                color = "Red"
            }
        };
    }

    public void OnPageAppearing()
    {
        SelectedDate = DateTime.Now;
        PopulateEvents();
    }

    public void PopulateEvents()
    {
        Events.Clear();

        foreach (CalendarEvent e in eventCollection)
        {
            if (e.date.Month != DisplayDate.Month || e.date.Year != DisplayDate.Year)
            {
                // Skip adding events that are not in the current month
                continue;
            }

            Color clr;
            switch (e.color)
            {
                case "Blue":
                    clr = Colors.Blue;
                    break;
                case "Red":
                    clr = Colors.Red;
                    break;
                case "Green":
                    clr = Colors.Green;
                    break;
                case "Yellow":
                    clr = Colors.Yellow;
                    break;
                case "Magenta":
                    clr = Colors.Magenta;
                    break;
                default:
                    clr = Colors.Blue;
                    break;
            }

            if (!Events.ContainsKey(e.date))
            {
                Events.Add(e.date, new DayEventCollection<CalendarEvent>(new List<CalendarEvent> { new CalendarEvent { diagnosisId = e.diagnosisId, diagnosisName = e.diagnosisName, name = e.name, date = e.date, location = e.location, description = e.description, color = e.color } })
                {
                    EventIndicatorColor = clr,
                    EventIndicatorSelectedColor = clr,
                    EventIndicatorSelectedTextColor = clr,
                    Colors = [Color.Parse(e.color)]
                });
            }
            else if (Events[e.date] is DayEventCollection<CalendarEvent> eventList)
            {
                eventList.Add(new CalendarEvent { diagnosisId = e.diagnosisId, diagnosisName = e.diagnosisName, name = e.name, date = e.date, location = e.location, description = e.description, color = e.color });
            }
        }
    }

    partial void OnSelectedDateChanged(DateTime? value)
    {
        if (value != null)
        {
            IsAddEventButtonVisible = true;
        }
        else
        {
            IsAddEventButtonVisible = false;
        }
    }

    [RelayCommand]
    async Task AddEvent()
    {
    }

    [RelayCommand]
    async Task EventTapped(CalendarEvent calendarEvent)
    {
        if (calendarEvent != null)
        {
        }
    }

    [RelayCommand]
    void PreviousMonth()
    {
        DisplayDate = DisplayDate.AddMonths(-1);
        PopulateEvents();
    }

    [RelayCommand]
    void NextMonth()
    {
        DisplayDate = DisplayDate.AddMonths(1);
        PopulateEvents();
    }
}
