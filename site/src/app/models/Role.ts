
export interface Role {
    name: String;
    slug: String;
    pages: Array<object>;
}

export interface Roles {
    roles: Role[];
}

export interface Pages {
    pages: Array<string>;
}
