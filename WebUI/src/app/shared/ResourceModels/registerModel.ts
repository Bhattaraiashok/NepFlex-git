// export class UserRegister {
//     UserDetail: UserRequest = new UserRequest();
//     FieldUpdateRequest: string;
// }

export class UserRegisterRequest {
    Email: string;
    Username: string;
    EnteredPassword: string;
    IsUserAgreementChecked: boolean;
}

export class UserUpdateRequest {
    Firstname: string;
    Middlename: string;
    Lastname: string;
    ProfileImage: string;
    Address: string;
    Country: string;
    PhoneNumber: string;
    ShowPhonenumber: boolean;
    IsUserSeller: boolean;
    FieldUpdateRequest: string;
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

