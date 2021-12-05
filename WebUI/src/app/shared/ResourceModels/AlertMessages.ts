export class AlertMessageProperties {
    //info,success,warning,danger,error -- all in lower
    alertType: string;
    alertMsg: string;
    alertBtnLabel?: string;
    showButton?: boolean;
}

export class CONSTList {
    error: string = 'error';
    failure: string = 'failure';
    success: string = 'success';
    info: string = 'info';
}

export class ContextKeys {
    searchText: string = 'USERLISTSEARCHED';
}