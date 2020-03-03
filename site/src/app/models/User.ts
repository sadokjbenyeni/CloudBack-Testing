export class User {
    id: number;
    firstname: string;
    lastname: string;
    email: string;
    password: string;
    job: string;
    companyName: string;
    companyType: string;
    website: string;
    address: string;
    postalCode: string;
    city: string;
    region: string;
    idCountry: string;
    
    country: string;
    cgu: string[] = [];
    phone: string;
    token: string;
    roleName: string[]; // Client, Administrator, Product, Compliance, Finance
}

export class Users {
    users: User[];
}
