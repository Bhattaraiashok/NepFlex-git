export class UserRegister {
    UserDetail: UserRequest = new UserRequest();
    CompanyDetails: CompanyRequest = new CompanyRequest();
}

export class UserRequest {
    Username: string;
    Firstname: string;
    Middlename: string;
    Lastname: string;
    PSWDHASH: string;
    UserEmail: string;
    PhoneCountryCode: string;
    PhoneNumber: string;
    ShowPhonenumber: boolean;
    IsUserSeller: boolean;
    IsUserAgreementChecked:boolean;
}

export class ResponseObjects {
    isSuccess: boolean;
    strMesssage:string[];
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
