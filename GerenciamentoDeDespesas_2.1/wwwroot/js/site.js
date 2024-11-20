
// codigo refrente ao uso dos filtros na pagina
$(document).ready(function () {
    // o que tiver dentro a function, vai ser rodado dentro da pagina junto ao JS.
    getDatatable('#TDespesas');
    getDatatable('#TableUsuarios');


    $('.btn-total-despesas').click(function () {
        var usuarioId = $(this).attr('usuario-id');

        $.ajax({
            type: 'GET',
            url: '/Usuario/ListarDespesasPorUsuarioId/' + usuarioId,
            success: function (result) {
                $("#listaDespesaUsuario").html(result);
                $('#modalDespesaUsuario').offcanvas('show');
                getDatatable('#TDespesas');
            }
        });
    });
})

// função de formatação de tabela, onde qualquer outra view, pode tá usando, apenas declaranco no comando acima.
function getDatatable(id) {
    $(id).DataTable({ // codogio onde é possivel está mudando a linguagem dos filtros ja estilizados.
        language: {
            "decimal": "",
            "emptyTable": "Nehum Registro encontrado na tabela",
            "info": "Mostrando _START_ registro de _END_ em um total de _TOTAL_ entradas",
            "infoEmpty": "Showing 0 to 0 of 0 entries",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Loading...",
            "processing": "",
            "search": "Procurar:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "first": "Primeiro",
                "last": "Ultimo",
                "next": "Proximo",
                "previous": "Anterior"
            },
            "aria": {
                "orderable": "Order by this column",
                "orderableReverse": "Reverse order this column"
            }
        }
    });
}


window.onload = function () {
    var messageModal = document.getElementById("messageModal");
    if (messageModal) {
        messageModal.style.display = "flex"; // Exibe o modal se ele existir
    }
};

function closeModal() {
    var messageModal = document.getElementById("messageModal");
    if (messageModal) {
        messageModal.style.display = "none"; // Oculta o modal
    }
}



function togglePassword(fieldId, iconId) {
    const passwordField = document.getElementById(fieldId);
    const eyeIcon = document.getElementById(iconId);

    if (passwordField.type === 'password') {
        passwordField.type = 'text';
        eyeIcon.innerHTML = '👁️'; 
    } else {
        passwordField.type = 'password';
        eyeIcon.innerHTML = '🙈'; 
    }
}

$(document).ready(function () {
    $('#form-novo-banco').submit(function (e) {
        e.preventDefault();
        var nomeBanco = $('#nome-banco').val();
        $.ajax({
            type: 'POST',
            url: '/Bancos/Cadastrar',
            data: { nome: nomeBanco },
            data: { nome: nomeBanco },
            success: function (data) {
                location.reload();
            }
        });
    });
});


