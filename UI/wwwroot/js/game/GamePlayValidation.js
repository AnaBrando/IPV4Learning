//#region Init
let rede = document.getElementById("verificar");
rede.addEventListener("click", function (e){
    e.preventDefault;

    verificar();


});
//#endregion

//#region validarEnderecoRede
function verificar() {

    let respostaCerta = true;
    let obj = document.querySelector("#rangers");
    let rede = obj.innerText;
    let resposta = rede.substring(2, 37);

    let classeInputs = document.querySelectorAll(".binary");

    let reposta1 = resposta.substring(0, 8);
    let reposta2 = resposta.substring(9, 17);
    let reposta3 = resposta.substring(18, 26);
    let reposta4 = resposta.substring(27, 37);

    let octeto1 = document.querySelector("#octeto1").value;
    let octeto2 = document.querySelector("#octeto2").value;
    let octeto3 = document.querySelector("#octeto3").value;
    let octeto4 = document.querySelector("#octeto4").value;

    var processo;

    if (octeto1 !== reposta1) {
        setTimeout(octetoErrado(classeInputs[0]), 2000);
        respostaCerta = false;
        processo++;
    }
    if (octeto2 !== reposta2) {
        setTimeout(octetoErrado(classeInputs[1]), 2000);
        respostaCerta = false;
    }
    if (octeto3 !== reposta3) {
        setTimeout(octetoErrado(classeInputs[2]), 2000);
        respostaCerta = false;
        processo++;
    }
    if (octeto4 !== reposta4) {
        setTimeout(octetoErrado(classeInputs[3]), 2000);
        respostaCerta = false;
        processo++;
    }

    setTimeout(limpar, 1000);
    if (respostaCerta) {
        respostaCorreta(respostaCerta, processo);
    }
    return respostaCerta;
}

function octetoErrado(x) {
    x.classList.add("errado");
}
//#endregion

//#region limpar
function limpar() {
    let inputs = document.querySelectorAll(".binary");
    inputs.forEach(x => x.classList.remove("errado"));
}
//#endregion

//#region respostaCorreta
function respostaCorreta(respostaCerta,x) {
    if (respostaCerta) {
        var enderecoRede = document.querySelectorAll(".binary");
        if (enderecoRede.length == 4) {
            var rede = '';
            for (var i = 0; i < enderecoRede.length; i++) {
                rede = rede + enderecoRede[i].value.toString()+'.';
            }
            rede = rede.substring(0,35);
            return MostrarRede(rede);
        }
        
    }
}

//#endregion

//#region MostrarRede
function MostrarRede(rede) {
    let conteudo = document.getElementById("concluido");
    conteudo.classList.add("concluido");
    
    setTimeout(desaper(rede),3000);
    //conteudo.innerHTML(rede);
}

function desaper(rede) {
    let inputs = document.querySelectorAll(".binary");
    inputs.forEach(x => x.classList.add("redeDescoberta"))
    let x = document.querySelector('#sucesso');
    x.classList.add('sucesso');
    //x.innerHTML = rede;
    let decimais = document.querySelectorAll(".decimal");
    decimais.forEach(x => x.style.display = 'block');
    let broadcast = document.getElementById("broadcast");
    broadcast.disabled = false;
    let host = document.getElementById("host");
    host.disabled = false;
    console.log(broadcast);
    console.log(host);
   // x.classList("redeDescoberta");
}
function mostraAjuda() {
    let img = document.querySelector("#tabelaVerdade");
    img.style.display = 'block';
    img.classList.add("img");

}   


//#endregion


