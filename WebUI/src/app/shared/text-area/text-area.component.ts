import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  forwardRef
} from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-text-area',
  templateUrl: './text-area.component.html',
  styleUrls: ['./text-area.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextAreaComponent),
      multi: true
    }
  ]
})
export class TextAreaComponent implements OnInit, ControlValueAccessor {
  @Input() _selectedValue: string;
  @Input() textType: string;
  @Input() placeholder: string;
  @Output() newData: EventEmitter<any> = new EventEmitter();
  showClearButton: boolean = false;
  constructor() {}

  get selectedValue() {
    return this._selectedValue;
  }

  set selectedValue(val) {
    this._selectedValue = val;
    this.propagateChange(this._selectedValue);
  }
  ngOnInit() {}
  TextChanged(event: { target; value: string }) {
    this.showClearButton = true;
    const val = event.target.value;
    this.selectedValue = val;
    this.newData.emit(event);
  }
  clearInput(event: { target; value: string }) {
    event.target.value = '';
    this.selectedValue = event.target.value;
    this.newData.emit(event);
    this.showClearButton = false;
  }
  propagateChange = (_: any) => {};
  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}
  setDisabledState?(isDisabled: boolean): void {}
}
