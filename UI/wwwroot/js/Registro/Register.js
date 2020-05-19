//#region Introdução
var questionNum = 0;
const output = document.querySelector("#output");
var botao = document.querySelector("#start");
const introducao = [
    "Olá Bem Vindo ao Network Learning!", "Bora aprender ipv4?"
];
y();
function y() {
    introducao.forEach((name, i) => {
        setTimeout(() => {
            display(name);
        }, i * 1000);
    });
    const display = s => output.innerText = s;
};
//#endregion

//#region Registro

function start() {
    let botao1 = document.getElementById("start");
    botao1.style.display = 'none';
    let registro = document.getElementById("registro");
    registro.style.display = "block";
    let container = document.getElementById("container");
    container.style.display = "block";
    bot();
}

const cadastro = {};

async function bot() {
    var input = document.getElementById("input").value;
    var inputPassword = document.getElementById("inputPassword").value;
    switch (questionNum) {
        case 0:
            output.innerHTML = '<h1>Saudações</h1>';
            document.getElementById("input").value = "";
            question = '<h1>Qual seu Nome?</h1>';
            setTimeout(timedQuestion, 2000);
            questionNum++;
            break;

        case 1:
            let UserName = (input.replace(/ /g, "")).toUpperCase();
            if (UserName) {
                Object.freeze(UserName);
                cadastro.UserName = UserName;
                output.innerHTML = '<h1>Belo Nick!</h1>';
                document.getElementById("input").value = "";
                question = '<h1>Por favor,Digite seu Email:</h1>';
                setTimeout(timedQuestion, 2000);
                questionNum++;
                break;
            } else {
                questionNum = 0;
                output.innerHTML = '<h1>Nome Inválido !</h1>';
                setTimeout(bot, 2000);
                break;
            }

        case 2:
            let Email = input;
            //ValidaEmail(Email).then((valida) => { return valida});    
            let valida = await ValidaEmail(Email);
            valida = JSON.parse(valida);
            console.log(valida);
            if (valida !== true) {
                questionNum = 1;
                output.innerHTML = '<h1>Email Inválido !</h1>';
                setTimeout(bot, 1000);
                break;
            }
            else {
                Object.freeze(Email);
                cadastro.Email = Email;
                output.innerHTML = '<h1>Certo</h1>';
                trocaInput();
                question = '<h1>Digite sua senha, ps. isso fica entre eu e você.</h1>';
                setTimeout(timedQuestion, 2000);
                questionNum++;
                break;
            }
        case 3:
            let Password = inputPassword;
            Object.freeze(Password);
            cadastro.Password = Password;
            output.innerHTML = '<h1>Certo</h1>';
            document.getElementById("inputPassword").value = "";
            question = '<h1>Confirme sua senha</h1>';
            setTimeout(timedQuestion, 2000);
            questionNum++;
            break;
        case 4:
            let ConfirmPassword = inputPassword;
            Object.freeze(ConfirmPassword);
            cadastro.ConfirmPassword = ConfirmPassword;
            if (cadastro.Password !== cadastro.ConfirmPassword) {
                questionNum = 2;
                output.innerHTML = '<h1>Senhas não são iguais !</h1>';
                bot();
                break;
            }
            else {
                output.innerHTML = '<h1>Bem vindo !</h1>';
                question = '<h1>Bem vindo !</h1>';
                document.getElementById("inputPassword").style.display = 'none';
                setTimeout(timedQuestion, 2000);
                finale();
                break;
            }
        default:
    }
}

function timedQuestion() {
    output.innerHTML = question;
}
trocaInput = () => {
    document.getElementById("input").style.display = 'none';
    document.getElementById("inputPassword").style.display = 'block';
}
finale = () => {
    var finale = document.getElementById("finale");
    var professor = document.getElementById("professor");
    document.getElementById("inputPassword").style.display = 'none';
    finale.disabled = false;
    document.getElementById("registro").style.display = 'none';
    document.getElementById("finale").style.display = 'block';
    document.getElementById("professor").style.display = 'block';
    
}
//#endregion

//#region Validacoes

//function ValidaEmail(email) {
//    var validaEmail = JSON.stringify(email);
//    var res = $.ajax({
//        type: "POST",
//        url: '/Home/EmailisValid',
//        data: validaEmail,
//        dataType: "json",
//        contentType: "application/json",
//        success: function(data) { },
//        async: false,
//        headers: {
//            RequestVerificationToken:
//                $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        error: function(e) {
//            console.log(e);
//        },

//    }).responseJSON;
//    return res;
//}

async function ValidaEmail(email) {
    let promise = new Promise(resolve => {
        var string = JSON.stringify(email);
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", "/Home/EmailisValid", true);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.onreadystatechange = () => {
            if (xhttp.readyState === 4 && xhttp.status === 200)
                resolve(xhttp.response);
        };
        xhttp.send(string);
    });
    let retorno = await promise;
    return retorno;
}








//#endregion

//#region Serialização
serializar = () => {
    console.log(document.querySelector("#roleName"));
    cadastro.RoleName = document.querySelector("#roleName").innerText;
    cadastro.ProfessorId = document.querySelector("#professorId").value;
    var cadastroJson = JSON.stringify(cadastro);
    var request = $.ajax({
        type: "POST",
        url: '/Identity/Account/Register',
        data: cadastroJson,
        async: false,
        error: function(jqXHR, textStatus, errorThrown) {
            console.log(jqXHR.responseText);
            console.log(textStatus);
            alert("error: " + jqXHR + textStatus + errorThrown);
        },
        dataType: "json",
        contentType: "application/json",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    }).done(function(data) {
        //let url = (data.toString()).replace(/"/g, "");
        console.log(data.redirectUrl);
        window.location.href = data.redirectUrl;   
    });
};

//#endregion
