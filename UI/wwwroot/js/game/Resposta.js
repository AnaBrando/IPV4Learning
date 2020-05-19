class Resposta  {
    constructor(resposta1, reposta2, reposta3, reposta4) {
        this.resposta1 = resposta1;
        this.reposta2 = reposta2;
        this.reposta3 = reposta3;
        this.reposta4 = reposta4;
    }
    set RespostaOcteto1(reposta1) {
        this.resposta1 = reposta1;
    }
    set RespostaOcteto2(reposta2) {
        this.reposta2 = reposta2;
    }
    set RespostaOcteto3(reposta3) {
        this.reposta3 = reposta3;
    }
    set RespostaOcteto4(reposta4) {
        this.reposta4 = reposta4;
    }
    get RespostaOcteto1() {
        return this.resposta1;
    }
    get RespostaOcteto2() {
        return this.reposta2;
    }
    get RespostaOcteto3() {
        return this.reposta3;
    }
    get RespostaOcteto4() {
        return this.reposta4;
    }

} module.exports = Resposta