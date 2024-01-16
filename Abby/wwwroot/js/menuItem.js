var dataTable;
var webRootUrl = "https://localhost:44319/api/MenuItem/"
$(document).ready(function () {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/MenuItem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            { "data": "foodType.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group">
                                <a href="/Admin/MenuItems/upsert?id=${data}" 
                                 class="btn btn-success text-white mx-2">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onClick=Delete("${webRootUrl}"+${data}) 
                                 class="btn btn-danger text-white mx-2">
                                <i class="bi bi-trash-fill"></i>
                                </a>
                            </div>`
                },
                "width": "15%"
            },
        ],
        "width": "100%",
    });
});


function Delete(url) {
    console.log("delete url", url)
    Swal.fire({
        title: 'Are you Sure?',
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes I'm sure delete this bad boi"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //notification delete failed
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}