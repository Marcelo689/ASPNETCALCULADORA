

function Calcular() {
    var dados = $("#Visor");
    //console.log(dados.val());
    $.ajax({
        url: "Home/Calcular",
        dataType: "json",
        method:"GET",
        data: {"Visor": dados.val()},
        success: function (response) {
            console.log(response);
            var resultado = response.data;
            console.log(resultado);
            dados.val(resultado);
        }
    });
}

window.onload = function () {
    var caracterNaoSequencial = ["+", "-", "/", "*", ".",]
    var todosBotoes = $(".botao").find(".botao");
    if (!todosBotoes.prevObject)
        return;
    todosBotoes.prevObject.each((btn, campo) => {
        var digito = campo.value;
        campo.onclick = () => {
            var valorAntigo = $("#Visor").val();
            var comprimentoDigitado = valorAntigo.length - 1;
            var ultimoDigito = valorAntigo[comprimentoDigitado];
            var ultimoDigitoEhSinal = false;
            var digitoEhSinal = false;
            for (var i = 0; i < caracterNaoSequencial.length; i++) {
                var e = caracterNaoSequencial[i];
                if (!ultimoDigitoEhSinal)
                    ultimoDigitoEhSinal = e != ultimoDigito ? false : true;
                if (!digitoEhSinal)
                    digitoEhSinal = e != digito ? false : true;
            }
            if (ultimoDigitoEhSinal && digitoEhSinal)
                return;
            $("#Visor").val(valorAntigo + digito);
        }
       
    });

}