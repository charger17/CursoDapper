var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $('#tblCategorias').DataTable({
        "ajax": {
            "url": "/admin/categorias/GetCategorias",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "idCategoria", "width": "5%" },
            { "data": "nombre", "width": "40%" },
            { "data": "fechaCreacion", "width": "5%" },
            {
                "data": "idCategoria",
                "render": function (data) {
                    return `

                        <div class="text-center">
                            <a href="/admin/categorias/editar/${data}" class="btn btn-success text-white cursor-modif">
                            <i class="bi bi-pencil-square"></i> Editar </a>
                            &nbsp;
                            <a onclick=Delete("/admin/categorias/BorrarCategoria/${data}")  class="btn btn-danger text-white cursor-modif"> 
                            <i class="bi bi-x-square"></i> Borrar </a>
                        </div>

                    `;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de borrar?",
        text: "¡Este contendio no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, Borrar.",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: "DELETE",
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        })
    })
}