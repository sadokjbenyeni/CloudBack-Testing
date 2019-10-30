
export interface StatementBankingIdentity {
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

export interface StatementBankingIdentities {
    currencies: StatementBankingIdentity[];
}

