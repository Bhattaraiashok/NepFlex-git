import { Injectable } from '@angular/core';
@Injectable()
export class ButtonProperties {
  buttonId: number;
  buttonLabel?: string;
  buttonRoute?: string;
  canRoute?: boolean;
  isDisable?:boolean;
  HasDropDown?: boolean;
  DropDownList?: DropDownList[];
  hasPopUp?: boolean;
  popUpName?: string;
  parentEmit?: boolean;
}

export class HeadersNavigation {
  headerId?: number;
  header?: string;
  headerRoute?: string;
  canRoute?: boolean;
  hasDropDown?: boolean;
  dropDownList?: DropDownList[];
}

export class DropDownList {
  id?: number;
  parentLabel?: string;
  displayName: string;
  disabled?: boolean;
  iconName?: string;
  route?: string;
  children?: DropDownList[];
}
