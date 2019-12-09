
export class StatementBankingIdentity {
    id: String;
    device: String;
    name: String;
    symbol: String;
    taux: Number;
    rib: Object;
    iban: Object;
    bic: Object;
    maxrib: Number;
    updated: Date;
}

export class StatementBankingIdentities {
    currencies: StatementBankingIdentity[];
}

