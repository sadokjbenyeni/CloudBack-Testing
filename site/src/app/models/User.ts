export class User {
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
    cgu: string[] = [];
    phone: String;
    token: String;
    roleName: String[]; // Client, Administrator, Product, Compliance, Finance
    role: String;
}

export class Users {
    users: User[];
}
