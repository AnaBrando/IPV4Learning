class Octeto {
    constructor(id, valor) {
        this.id = id;
        this.valor = valor;
    }

    get Valor() {
        return this.valor;
    }

    set Correto(x) {
        if (valor === x) {
            return true;
        }
        return false;
    }
}