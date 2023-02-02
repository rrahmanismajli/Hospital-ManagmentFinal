var dataTable;

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "AppointmentFinal/GetAll?status=" + status
        },
        "columns": [
            {
                "data": "Id", "width": "15%"
            },
            {
                "data": "FullName", "width": "15%"
            },
            {
                "data": "PhoneNumber", "width": "15%"
            },
            {
                "data": "Email", "width": "15%"
            },
            {
                "data": "ReasonForVisit", "width": "15%"
            },
            {
                "data": "Notes", "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Costumer/AppointmentFinal/Details?appointmentId=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Details</a>

					</div>
                        `
                },
                "width": "15%"
            }


        ]
    });
}