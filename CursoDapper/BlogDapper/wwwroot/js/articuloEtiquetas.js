var dataTable;

$(document).ready(function () {
    cargarDataTable();
});

function cargarDataTable() {
    dataTable = $('#tblArticuloEtiquetas').DataTable({
        "ajax": {
            "url": "/admin/etiquetas/getArticulosEtiquetas",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "idArticulo", "width": "30%" },
            { "data": "titulo", "width": "30%" },
            { "data": "etiqueta[0].nombreEtiqueta", "width": "30%" }
            
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