using System.Globalization;
using CsvHelper;
using EventbriteNET;
//using EventbriteNETStandardConsoleTest.Models;
using snowfroc.data.Models;

var eventbriteNET = new EventbriteContext("XXXXXXXXXXXXXX");

/*
var users = eventbriteNET.Get<User>();

Console.WriteLine(users.Count);
*/

/*
var user = users[0];

Console.WriteLine(user.Emails.Count);
Console.WriteLine(user.Name);
Console.WriteLine(user.FirstName);
Console.WriteLine(user.LastName);
*/
//var ebEvents = eventbriteNET.Get<Event>().ToList();

//var pagedEvents = eventbriteNET.GetOwnedEvents();

//var pagedEvents = eventbriteNET.GetOwnedEvents();

var sfevent = eventbriteNET.Get<Event>(487925687867);

eventbriteNET.EventId = sfevent.Id;

/*
Console.WriteLine($"Location {sfevent.VenueId} {sfevent.Venue.Address.Address1} {sfevent.Venue.Address.City}");
if (!sfevent.OnlineEvent)
{
    var venuue = eventbriteNET.Get<Venue>(sfevent.VenueId ?? 0);
    sfevent.Venue = venuue;
    Console.WriteLine(sfevent.Venue.ResourceUri);
    Console.WriteLine(string.Format("Location {0}", sfevent.Venue.Name));
}
*/

var tickets = eventbriteNET.Get<TicketClass>();

var attendeeList = new List<SFAttendee>();
var attendees = eventbriteNET.Get<Attendee>().ToList();
for (int i = 1; i <= eventbriteNET.Pagination.PageCount; i++)
{
    eventbriteNET.Page = i;
    attendees = eventbriteNET.Get<Attendee>().ToList();
    var attendee = eventbriteNET.Get<Attendee>(long.Parse(attendees[0].id));
    Console.WriteLine($"Attendee Pagination: ObjectCount {eventbriteNET.Pagination.ObjectCount} PageCount {eventbriteNET.Pagination.PageCount} PageNumber {eventbriteNET.Pagination.PageNumber} PageSize {eventbriteNET.Pagination.PageSize}");
    Console.WriteLine($"Attendees {attendees.Count}");
    attendees.ForEach(a => Console.WriteLine($"{a.profile.first_name},{a.profile.last_name},{a.profile.email},{a.ticket_class_id},{a.order_id},{a.barcodes.First().barcode}"));
    attendees.ForEach(a => attendeeList.Add(new SFAttendee { FirstName = a.profile.first_name, LastName = a.profile.last_name, Email = a.profile.email, TicketClassId = a.ticket_class_id, OrderId = a.order_id, Barcode = a.barcodes.First().barcode }));
}

using (var writer = new StreamWriter("SnowFROC2023.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(attendeeList);
}

var attendeeList2 = new List<SFAttendee>();
using (var reader = new StreamReader("SnowFROC2023.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    attendeeList2 = csv.GetRecords<SFAttendee>().ToList();
}

int x = 1;

/*
var pagedEvents = eventbriteNET.Search();

eventbriteNET.Pagination = pagedEvents.Pagination;

var ebEvents = pagedEvents.Events;

Console.WriteLine(string.Format("Pagination: ObjectCount {0} PageCount {1} PageNumber {2} PageSize {3}", eventbriteNET.Pagination.ObjectCount, eventbriteNET.Pagination.PageCount, eventbriteNET.Pagination.PageNumber, eventbriteNET.Pagination.PageSize));
ebEvents.ForEach(e =>
{
    Console.WriteLine(string.Format("{0} {1} {2}", e.Description.Text, e.Start.Local, e.End.Local));
    eventbriteNET.EventId = e.Id;

    Console.WriteLine(string.Format("Location {0}", e.OnlineEvent ? "Online Event" : string.Format("{0} {1} {2}", e.VenueId, e.Venue.Address.Address1, e.Venue.Address.City)));
    if (!e.OnlineEvent)
    {
        var venuue = eventbriteNET.Get<Venue>(e.VenueId ?? 0);
        e.Venue = venuue;
        Console.WriteLine(e.Venue.ResourceUri);
        Console.WriteLine(string.Format("Location {0}", e.Venue.Name));
    }





    var attendees = eventbriteNET.Get<Attendee>().ToList();

    Console.WriteLine(string.Format("Attendee Pagination: ObjectCount {0} PageCount {1} PageNumber {2} PageSize {3}", eventbriteNET.Pagination.ObjectCount, eventbriteNET.Pagination.PageCount, eventbriteNET.Pagination.PageNumber, eventbriteNET.Pagination.PageSize));

    Console.WriteLine(string.Format("Attendees {0}", attendees.Count));




    attendees.ForEach(a => Console.WriteLine(a.profile.email));

});
*/

//


//Console.ReadLine();
// eventbriteNET.Get<List<Event>>("")
// Console
