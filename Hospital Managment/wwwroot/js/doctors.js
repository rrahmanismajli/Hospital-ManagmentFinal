var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable () {
   dataTable= $('#tblData').DataTable({
        "ajax": {
           "url":"Doctors/GetAll"
        },
        "columns": [
            {
               "data": "firstName","width":"15%"
            }, 
            {
                "data": "specialty", "width": "15%"
            },
            {
                "data": "phoneNumber", "width": "15%"
            },
            {
                "data": "email", "width": "15%"
            },
            {
                "data": "address", "width": "15%"
            },
            {
                "data": "department.name", "width": "15%"
            },
            {
                "data": "imageUrl",
                "render": function (data) {
                    return `
                      <img width="100px" height="50px" src="${data}"/>
                        `
                },
                "width": "15%"
            }, {
                "data": "doctorId",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Doctors/Edit?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="/Admin/Doctors/Delete/${data}")
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }
         

        ]
    });
}

//{"data":[{"doctorId":6,"firstName":"Rrahman","lastName":"Ismajli","specialty":"Admin","phoneNumber":"03233544545","email":"kirurgji@gmail.com","address":"asgdg","departmentId":15,"department":null,"imageUrl":"\\images\\doctors\\886ab8b3-7534-4f8d-baf9-b27e1d2ac918.jpg","prescription":null,"appointments":null,"treatments":null,"doctorAppointments":null,"hospitalizations":null}]}