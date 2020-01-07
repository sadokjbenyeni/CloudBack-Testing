import { CardType } from "./CardType";

export class Payment {
    methodId: string;
    cardType: CardType
    client: string;
    cardHolder: string;
    cardNumber: string;
    expirationDate: Date;
    cryptogram: string;

    constructor() {

    }

}