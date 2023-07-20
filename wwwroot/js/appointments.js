$(document).ready(function () {
    $('#SelectedAppointment_StartTime').datetimepicker({
        format: 'YYYY-MM-DD HH:mm'
    });

    $('#SelectedAppointment_EndTime').datetimepicker({
        format: 'YYYY-MM-DD HH:mm'
    });

    function updateTimes(data) {
        $('#SelectedAppointment_StartTime').datetimepicker('date', moment(data.startTime));
        $('#SelectedAppointment_EndTime').datetimepicker('date', moment(data.endTime));
    }

    $('#calendar').fullCalendar({
        header: {
            left: 'prev, next, today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        eventClick: function (event, jsEvent, view) {
            // Pass the appointment ID as a parameter.
            var url = '/Appointments/Index?handler=Appointment&id=' + event.id;

            $.ajax({
                url: url,
                method: 'GET',
                success: function (data) {
                    console.log('Data received:', data);
                    $('#SelectedAppointment_Topic').val(data.topic);
                    $('#SelectedAppointment_PatientId').val(data.patientId);                 

                    $('#SelectedAppointment_StartTime').datetimepicker({
                        format: 'YYYY-MM-DD HH:mm',
                        defaultDate: moment(data.startTime)
                    });

                    $('#SelectedAppointment_EndTime').datetimepicker({
                        format: 'YYYY-MM-DD HH:mm',
                        defaultDate: moment(data.endTime)
                    });

                    $('#SelectedAppointment_Status').val(data.status);
                    $('#SelectedAppointment_Notes').val(data.notes);
                    console.log('Notes value set:', data.notes);
                    console.log('Status value set:', data.status);

                    $('#SelectedAppointmentId').val(event.id);

                    updateTimes(data);

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
