<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventCalendar.aspx.cs" Inherits="ADMINISTRATION_EventCalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!DOCTYPE html>
<html lang='en'>
  <head>
   <meta charset='utf-8' />
      <style>
          #calendar {
    background-color: white; /* Change this to the color you want */
}
          .fc-header-toolbar {
   
    padding: 10px; /* Add padding to the header toolbar */
}

      </style>

    <script src='https://cdn.jsdelivr.net/npm/fullcalendar@6.1.11/index.global.min.js'></script>
   <script>
       document.addEventListener('DOMContentLoaded', function () {
           var calendarEl = document.getElementById('calendar');
           var calendar = new FullCalendar.Calendar(calendarEl, {
               initialView: 'dayGridMonth',
               displayEventTime: true, // Show event time on all dates
               eventDisplay: 'block', // Display events as blocks
               headerToolbar: {
                   left: 'prev,next today',
                   center: 'title',
                   right: 'dayGridMonth,timeGridWeek,timeGridDay'
               },
               events: function (fetchInfo, successCallback, failureCallback) {
                   // Make an AJAX request to fetch events from the server
                   $.ajax({
                       url: '<%= ResolveUrl("~/WebServices/EventCalendar.asmx/SpecialEvent") %>',
                       method: 'POST',
                       dataType: 'json',
                       contentType: "application/json; charset=utf-8",
                       success: function (response) {
                           var events = [];
                           var data = JSON.parse(response.d);
                           $.each(data, function (key, value) {
                               var startDate = new Date(value.STARTDATE);
                               var endDate = new Date(value.ENDDATE);
                              
                               var color = '#' + Math.floor(Math.random() * 16777215).toString(16);
                               events.push({
                                   title: value.EVENT_NAME,
                                   start: startDate,
                                   end: new Date(endDate.getTime() + (24 * 60 * 60 * 1000)), // Add one day to the end date
                                   allDay: true,
                                   color: color,
                                   extendedProps: {
                                       description: value.EVENT_DESCRIPTION,
                                   }
                               });
                           });
                           successCallback(events);
                       },
                       error: function (XMLHttpRequest, textStatus, errorThrown) {
                           alert("Status: " + textStatus);
                           alert("Error: " + XMLHttpRequest.responseText);
                           failureCallback(errorThrown);
                       }
                   });
               },
               eventClick: function (info) {
                   $('#EventTitle').html(info.event.title);
                   var startDate = new Date(info.event.start);
                   var formattedStartDate = startDate.getDate().toString().padStart(2, '0') + '-' + (startDate.getMonth() + 1).toString().padStart(2, '0') + '-' + startDate.getFullYear();
                   
                   var endDate = new Date(info.event.end);
                   endDate.setDate(endDate.getDate() - 1); // Subtracting one day
                   var formattedEndDate = endDate.getDate().toString().padStart(2, '0') + '-' + (endDate.getMonth() + 1).toString().padStart(2, '0') + '-' + endDate.getFullYear();

                   $('#EventStartDate').html(formattedStartDate);
                   $('#EventEndDate').html(formattedEndDate);
                   $('#EventDescription').html(info.event.extendedProps.description);
                   $('#EventModal').modal('show');
               }
        
           });
           calendar.render();

           var span = document.getElementsByClassName("close")[0];
           span.onclick = function () {
               $('#EventModal').modal('hide');
           };

           // Close modal when user clicks anywhere outside of the modal
           window.onclick = function (event) {
               var modal = document.getElementById('eventModal');
               if (event.target == modal) {
                   $('#EventModal').modal('hide');
               }
           };
       });

</script>
    

  </head>
  <body>
    <div id='calendar'></div>

      <!-- Modal -->
   <div class="modal fade" id="EventModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg" role="document"> <!-- Added modal-lg class for larger size -->
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title text-center" id="exampleModalLabel">Event Details</h5> <!-- Added text-center class to center title -->
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="modal-body">
                <!-- Display event details here -->
                <h4 class="text-center"><span id="EventTitle"></span></h4> <!-- Added text-center class to center title -->
                <p></p>
                <p><b>Event Start Date:</b> <span id="EventStartDate"></span></p>
                <p><b>Event End Date:</b> <span id="EventEndDate"></span></p>
                <p><b>Event Description:</b> <span id="EventDescription"></span></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>


  
  </body>
</html>
</asp:Content>


