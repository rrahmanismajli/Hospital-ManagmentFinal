var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("administrators")) {
        loadDataTable("administrators");
    } else {
        if (url.includes("customers")) {
            loadDataTable("customers");
        } else {
            loadDataTable();
        }
    }
});
function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "Patients/GetAllUsers?status="+status
        },
        "columns": [
            {
                "data": "name", "width": "15%"
            },
            {
                "data": "streetAdress", "width": "15%"
            },
            {
                "data": "city", "width": "15%"
            },
           
           
           
            {
                "data": "imageUrl",
                "render": function (data) {
                    return `
                       <img width="100px" height="50px" alt="user photo" src="${data}"/>
                      `
             },



               "width": "15%"
            },
            {
                "data": "role", "width": "15%"
            },
            {
                "data": "email", "width": "15%"
            },
            {
                "data": "phoneNumber", "width": "15%"
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

//{"data":[{"name":"posit","streetAdress":"okggfigk","city":"idjgig","state":"ijdigj","postalCode":"129",
//"imageUrl": null, "id": "108c1a1c-ffc2-47ea-a039-b06033c0ff88", "userName": "imigm@gmail.com",
  //  "normalizedUserName": "IMIGM@GMAIL.COM", "email": "imigm@gmail.com", "normalizedEmail": "IMIGM@GMAIL.COM",
 //   "emailConfirmed": false, "passwordHash": "AQAAAAEAACcQAAAAEFcQv/GXRrQXQFKsje4BJFrR7VJwqnkdZoHDrkkGOfN6AfoFa7Edv71lC9FbGrupew==",
 //       "securityStamp": "LQTUY34AOA4TWRPMQGC3YGXJ74KOIWD6", "concurrencyStamp": "5cf2143a-4848-41aa-8925-72f2e07d6bd4",
//"phoneNumber": "0350435", "phoneNumberConfirmed": false,
  //  "twoFactorEnabled": false, "lockoutEnd": null,
    //    "lockoutEnabled": true, "accessFailedCount": 0}