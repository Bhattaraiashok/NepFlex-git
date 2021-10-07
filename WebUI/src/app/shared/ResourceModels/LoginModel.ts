export class LoginRequest {
    UserID: string;
    UserPSWD: string;
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
}