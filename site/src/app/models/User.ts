export interface User {
    id: Number;
    firstname: String;
    lastname: String;
    email: String;
    password: String;
    job: String;
    companyName: String;
    companyType: String;
    website: String;
    address: String;
    postalCode: String;
    city: String;
    region: String;
    idCountry: String;
    country: String;
    cgv: Boolean;
    cgu: string[];
    commercial: Boolean;
    phone: String;
    sameAddress: Boolean;
    addressBilling: String;
    postalCodeBilling: String;
    cityBilling: String;
    idCountryBilling: String;
    countryBilling: String;
    paymentMethod: String;
    currencyBilling: String;
    vat: String;
    checkvat: Boolean;
    currency: String;
    payment: String;
    islogin: Boolean;
    token: String;
    nbSession: Number;
    roleName: String[]; // Client, Administrator, Product, Compliance, Finance
    role: String;
    state: Number; // 0 : non actif, 1 : actif, -1 : suspendu. Active after email validation
}

export interface UserResponse {
    user: User;
    message: string;
}

export interface Users {
    users: User[];
}
