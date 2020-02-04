import { CardType } from "./CardType";

export class Payment {
    paymentMethodId:string;
    holder: string;
    numbers: string;
    client: string;
    network: CardType;
    cryptogram: string;
    expirationYear: string;
    expirationMonth: string;
    constructor() {

    }

}