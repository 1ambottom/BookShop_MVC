var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#productList').DataTable({
        "ajax": { "url": '/admin/product/getall' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'title', "width": "20%" },
            { data: 'author', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'isbn', "width": "20%" },
            { data: 'category.name', "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group text-center" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary col"><i class="fa-sharp fa-solid fa-pen pe-2"></i></a>
                            <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger col"><i class="fa-solid fa-trash pe-2"></i></a>
                        </div>
                        `
                },
                "width": "15%"
            },
        ]
    });
}


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}
