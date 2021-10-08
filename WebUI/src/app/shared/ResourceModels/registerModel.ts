export class UserRegister {
    UserDetail: RegisterRequest = new RegisterRequest();
    CompanyDetails: CompanyRequest = new CompanyRequest();
}

export class RegisterRequest {
    Username: string;
    Firstname: string;
    Middlename: string;
    Lastname: string;
    Password: string;
    Email: string;
    PhoneNumber: string;
    ShowPhonenumber: boolean;
    IsUserSeller: boolean;
}

export class RegisterResponse {
    status: string;
}

export class CompanyRequest {
    CompanyName: string;
    Address: string;
    PhoneCountryCode: string;
    PhoneNumber: string;
    ShowPhonenumber: boolean;
    IsGOVRegisteredCompany: boolean;
    CompanyEmailID: string;
}
