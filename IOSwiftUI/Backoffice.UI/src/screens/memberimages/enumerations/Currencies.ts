enum Currencies {

    USDollar = 0,
    EURO = 1,
    TRY = 2,

}

namespace Currencies {

    export function getCurrenciesName(type: Currencies): string {
        if (type === Currencies.USDollar) {
            return "USDollar";
        }

        if (type === Currencies.EURO) {
            return "EURO";
        }

        if (type === Currencies.TRY) {
            return "TRY";
        }

        return "TRY";
    }

}

export default Currencies;
