var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Departments/getall"
        },
        "columns": [
            {
                "data": "name", "width": "15%"
            }, {
                "data": "phoneNumber", "width": "15%"
            },
            {
                "data": "email", "width": "15%"
            },
          
            {
                "data": "imageUrl",
                "render": function (data) {
                    return `
                      <img class="rounded-circle" width="100px" height="50px" src="${data}"/>
                        `
                },
                "width": "15%"
            },
          

            {
                "data": "departmentId",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Departments/Edit?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="/Admin/Departments/Delete/${data}")
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }

        ]
    });
}

