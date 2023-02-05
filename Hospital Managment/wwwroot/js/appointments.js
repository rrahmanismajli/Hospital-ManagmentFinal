var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("active")) {
        loadDataTable("active");
    }
    else {
        if (url.includes("inprogress")) {
            loadDataTable("inprogress");
        } else {

if (url.includes("expired")) {
                loadDataTable("expired");
            }  else {
                    loadDataTable("all");
                }
            }
        }
    }

});
function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Appointments/GetAll?status=" + status
        },
        "columns": [
            {
                "data": "id", "width": "15%"
            },
            {
                "data": "fullName", "width": "15%"
            },
            {
                "data": "dateTimeOfAppointment", "width": "15%"
            },
            {
                "data": "email", "width": "15%"
            },
            {
                "data": "phoneNumber", "width": "15%"
            },
            {
                "data": "fullAdress", "width": "15%"
            },
            {
                "data": "reasonOfVisiting", "width": "15%"
            },
            {
                "data": "doctor.firstname", "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Costumer/Appointments/Details?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Details</a>

					</div>
                        `
                },
                "width": "15%"
            }


        ]
    });
}