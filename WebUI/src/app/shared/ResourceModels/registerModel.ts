export class RegisterRequest {
    Username: string;
    Firstname: string;
    Middlename: string;
    Lastname: string;
    Password: string;
    Email: string;
    IsUserSeller: boolean;
}

export class RegisterResponse {
    status: string;
}