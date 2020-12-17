$(document).ready(function () {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/api/Calendar",
        success: function (data) {
            
            console.log(data);

            if (data.upcomingShiftsCount != 0) {
                $('#upcomingShiftsCount').text(data.upcomingShiftsCount);
            }
            if (data.openShiftsCount != 0) {
                $('#openShiftsCount').text(data.openShiftsCount);
            }
            if (data.pendingShiftsCount != 0) {
                $('#pendingShiftsCount').text(data.endingShiftsCount);
            }
            if (data.takenShifts != 0) {
                $('#takenShiftsCount').text(data.takenShifts);
            }


            $.each(data.allShiftsCalendar,
                function (i, v) {
                    var color = null;
                    if (v.type == 1) {
                        color = '#00bfff';
                    } else if (v.type == 2) {
                        color = '#FF0000';
                    } else if (v.type == 3) {
                        color = '#9932cc';
                    }
                    else if (v.type == 4) {
                        color = '#A9A9A9';
                    }
                    events.push({
                        id: v.id,
                        type: v.type,
                        groupName: v.groupName,
                        description: v.description,
                        start: moment(v.start),
                        end: v.end != null ? moment(v.end) : null,
                        color: color,
                        allDay: false,
                        eventDisplay: 'auto'
                    });
                });

            generateCalender(events);
        },
        error:
            function (error) {
                alert('failed');
            }
    });

    function generateCalender(events) {
        console.log(events);
        console.log("hello from the calendar");
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            contentHeight: 420,
            defaultDate: new Date(),
            timeFormat: 'HH:mm',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay'
            },
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                $('#myModal #groupName').text(calEvent.groupName); //#groupName
                var $description = $('<div />');
                $description.append($('<p />').html('<b>Start:</b> ' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>End:</b> ' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p />').html('<b>Description:</b>' + calEvent.description));
                $('#myModal #pDetails').empty().html($description);

                $('#myModal #shiftId').val(calEvent.id);

                sendShiftToCorrectRoute(calEvent.type);
                $('#myModal').modal();
            }
        });
    }

    function sendShiftToCorrectRoute(shiftType) {
        if (shiftType == 2) {
            $('#shiftActionForm').attr('action', '/ShiftApplication/Apply');
            $('#shiftActionForm > button').text('Apply');
        } else if (shiftType == 3) {
            $('#shiftActionForm').attr('action', '/ShiftChange/ShiftSwapRequests');
            $('#shiftActionForm > button').text('See requests');
        }
        else if (shiftType == 4) {
            $('#shiftActionForm').attr('action', '/ShiftChange/Apply');
            $('#shiftActionForm > button').text('Ask for swap');
        }
    }
})