$(document).ready(function () {
    $('#calendar').fullCalendar({
        header: {
            left: 'prev, next, today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        eventClick: function (event, jsEvent, view) {
            // Pass the appointment ID as a parameter.
            var url = '/Appointments/Index?handler=Appointment&id=' + event.id;

            // Make an AJAX request to retrieve the appointment details
            $.ajax({
                url: url,
                method: 'GET',
                success: function (data) {
                    $('#SelectedAppointment_Topic').val(data.topic);
                    $('#SelectedAppointment_PatientId').val(data.patientId);                 
                    $('#SelectedAppointment_StartTime').val(data.startTime);
                    $('#SelectedAppointment_EndTime').val(data.endTime);
                    $('#notes').val(data.notes);
                    $('#SelectedAppointmentId').val(event.id);

                    var modal = $("#appointment-edit");
                    modal.modal('show');
                },
                error: function () {
                    alert('Failed to retrieve appointment details.');
                }
            });
        },
        dayClick: function (date, jsEvent, view) {
            $('#appointment-add').modal('show');
        },
        firstDay: 1,
        events: '/Appointments/Index?handler=Events'
    });

    $('#closeBtn').click(function () {
        $('#appointment-add').modal('hide');
    });

    $('#closeSelectedBtn').click(function () {
        $('#appointment-edit').modal('hide');
    });

    $('#maincloseBtn').click(function () {
        $('#appointment-add').modal('hide');
    });

    $('#maincloseSelectedBtn').click(function () {
        $('#appointment-edit').modal('hide');
    });

    $('#appointment-add').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
    });

    $('#appointment-edit').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
    });

});
