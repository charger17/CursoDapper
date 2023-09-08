var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $('#tblEtiquetas').DataTable({
        "ajax": {
            "url": "/admin/etiquetas/GetEtiquetas",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "idEtiqueta", "width": "10%" },
            { "data": "nombreEtiqueta", "width": "30%" },
            { "data": "fechaCreacion", "width": "30%" },
            {
                "data": "idEtiqueta",
                "render": function (data) {
                    return `

                        <div class="text-center">
                            <a href="/admin/etiquetas/editar/${data}" class="btn btn-success text-white cursor-modif">
                            <i class="bi bi-pencil-square"></i> Editar </a>
                            &nbsp;
                            <a onclick=Delete("/admin/etiquetas/BorrarEtiqueta/${data}")  class="btn btn-danger text-white cursor-modif"> 
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