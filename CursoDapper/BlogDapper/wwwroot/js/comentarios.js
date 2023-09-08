var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $('#tblComentarios').DataTable({
        "ajax": {
            "url": "/admin/comentarios/GetComentarios",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idComentario", "width": "5%" },
            { "data": "titulo", "width": "20%" },
            { "data": "mensaje", "width": "20%" },
            { "data": "articulo.titulo", "width": "40%" },
            { "data": "articulo.fechaCreacion", "width": "5%" },
            {
                "data": "idComentario",
                "render": function (data) {
                    return `

                            <a onclick=Delete("/admin/comentarios/BorrarComentario/${data}")  class="btn btn-danger text-white cursor-modif"> 
                            <i class="bi bi-x-square"></i> Borrar </a>
                        </div>

                    `;
                }, "width": "15%"
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