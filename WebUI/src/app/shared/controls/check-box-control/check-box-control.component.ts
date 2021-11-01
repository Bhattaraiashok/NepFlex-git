import { Component, OnInit, forwardRef, Input, Output, EventEmitter } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { CheckBoxControlProperties } from "app/shared/ResourceModels/controls";

@Component({
  selector: 'app-check-box-control',
  templateUrl: './check-box-control.component.html',
  styleUrls: ['./check-box-control.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => CheckBoxControlComponent),
      multi: true
    }
  ]
})
export class CheckBoxControlComponent implements OnInit, ControlValueAccessor {
  @Input() _selectedValue: boolean;
  @Input() dataContent: CheckBoxControlProperties = new CheckBoxControlProperties();
  @Output() onChange: EventEmitter<CheckBoxControlProperties> = new EventEmitter<CheckBoxControlProperties>();

  get selectedValue() {
    return this._selectedValue;
  }

  set selectedValue(val) {
    this._selectedValue = val;
    this.propagateChange(this._selectedValue);
  }

  constructor() { }
  ngOnInit(): void { }

  valueChanged(event: { target; value: string }) {
    const val = event.target.checked;
    this.selectedValue = val;
    const cssPlay = document.getElementById(this.dataContent.id);
    if (this.selectedValue) {
      //asign positve value
      cssPlay.setAttribute('placeholder', this.dataContent.positveContent);
    } else {
      //asign negative value
      cssPlay.setAttribute('placeholder', this.dataContent.negativeContent);
    }
    this.dataContent.responseValue = this.selectedValue;
    this.onChange.emit(this.dataContent);
  }

  propagateChange = (_: any) => { };
  writeValue(obj: any): void { }
  registerOnChange(fn: any): void { }
  registerOnTouched(fn: any): void { }
  setDisabledState?(isDisabled: boolean): void { }

}
