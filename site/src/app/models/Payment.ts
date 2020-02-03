import { CardType } from "./CardType";

export class Payment {
    methodId: string;
    cardType: CardType
    client: string;
    cardHolder: string;
    cardNumber: string;
    expirationYear: string;
    expirationMonth: string;
    cryptogram: string;
    constructor() {

    }

}