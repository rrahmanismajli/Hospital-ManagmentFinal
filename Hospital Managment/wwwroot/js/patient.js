var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Patients/getall"
        },
        "columns": [
            {
                "data": "firstName", "width": "15%"
            },
            {
                "data": "lastName", "width": "15%"
            },
            {
                "data": "dateOfBirth", "width": "15%"
            },
            {
                "data": "gender", "width": "15%"
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
                "data": "insuranceProvider", "width": "15%"
            },
            {
                "data": "primaryCarePhysician", "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Admin/Patients/Edit?id=${data}"
                        class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a href="/Admin/Patients/Delete/${data}")
                        class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
					</div>
                        `
                },
                "width": "15%"
            }

        ]
    });
}


//{"data":[{"id":1,"firstName":"TEst","lastName":"tst","dateOfBirth":"2000-01-18T14:37:00","gender":"male","phoneNumber":"4333434333","email":"hter@gmail.com","address":"asfdg","insuranceProvider":"ASd","primaryCarePhysician":"test","appointments":null,"payment":null,"bills":null,"prescriptions":null,"testResults":null,"insuranceCompanies":null,"treatments":null,"hospitalizations":null,"patientInsuranceCompanies":null},{"id":2,"firstName":"TEst","lastName":"tst","dateOfBirth":"2000-01-18T14:37:00","gender":"male","phoneNumber":"4333434333","email":"hter@gmail.com","address":"asfdg","insuranceProvider":"ASd","primaryCarePhysician":"test","appointments":null,"payment":null,"bills":null,"prescriptions":null,"testResults":null,"insuranceCompanies":null,"treatments":null,"hospitalizations":null,"patientInsuranceCompanies":null}]}
  