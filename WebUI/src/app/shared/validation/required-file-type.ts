import { FormControl } from "@angular/forms/forms";

export function requiredFileType(type: string[]) {
    return function (control: FormControl) {
        const file = control.value;
        if (file) {
            const extension = file.name !== undefined ? file.name.split('.')[1].toLowerCase() : file.split('.')[1].toLowerCase();
            type.forEach(element => {
                if (element.toLowerCase() !== extension.toLowerCase()) {
                    return {
                        requiredFileType: true
                    };
                }
            });
            return null;
        }

        return null;
    };
}