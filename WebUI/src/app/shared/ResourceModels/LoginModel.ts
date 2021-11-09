
export class LoginRequest {
    UserName: string;
    UserPSWD: string;
    IsRememberMe: boolean;
    IsUserSeller: boolean;
}

export class LoginResponse {
    email: string;
    firstname: string;
    middlename: string;
    lastname: string;
    profilePicture: string;
    dateJoined: string;
    userGuid: string;
    _Auth: string;
    isAuthenticated: boolean;// this needs to be true in order to be successfully login
    sessionID: string;
    timeStamp: Date;
    strMessage: string[];
    isSuccess: boolean;
    statusType: string;
}