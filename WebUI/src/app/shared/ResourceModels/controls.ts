export class CheckBoxControlProperties {
    id: string; //NOT NULLABLE
    label?: string;
    negativeContent: string; //NOT NULLABLE
    positveContent: string; //NOT NULLABLE
    parentEmit?: boolean;
    formControlName?: string;
    displayLabel?: boolean;
    responseValue: boolean; //reques:nullable but response : NOT NULLABLE
    displayValidation?: boolean;
    ValidationMessage?: string;
    event?:{ target; value: string };
}