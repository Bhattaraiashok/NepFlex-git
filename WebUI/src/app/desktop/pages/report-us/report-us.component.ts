import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ReportService } from 'app/shared/services/report.service';
import { ReportGetData } from 'app/shared/ResourceModels/ReportGetData';
import { Router } from '@angular/router';

@Component({
  selector: 'app-report-us',
  templateUrl: './report-us.component.html',
  styleUrls: ['./report-us.component.scss']
})
export class ReportUsComponent implements OnInit {
  reportForm: FormGroup;
  reportedDataList: ReportGetData[] = new Array();
  infoPageLabel: string = 'No Report Found';
  showInfoPage: boolean = false;
  turnSubmitButtonDisable: boolean = false;
  turnLargeLoader: boolean = false;
  turnSmallLoader: boolean = false;

  constructor(
    private fb: FormBuilder,
    private reportService: ReportService
  ) {
    this.createReportForm();
    this.turnSmallLoader = true;
    this.getReportedData();
  }

  ngOnInit() {}
  createReportForm() {
    this.reportForm = this.fb.group({
      name: '',
      email: '',
      url: '',
      detail: ''
    });
  }
  populateReportForm(reportData: ReportGetData): void {
    if (this.reportForm) {
      this.reportForm.reset();
    }
    this.reportForm.setValue({
      id: reportData.id,
      name: reportData.name,
      email: reportData.email,
      url: reportData.url,
      detail: reportData.detail,
      dateAdded: reportData.dateAdded,
      userName: reportData.userName,
      ui: reportData.ui
    });
  }

  getReportedData() {
    this.reportService.getReports().subscribe(a => {
      this.reportedDataList = [];
      this.reportedDataList = a;
      if (this.reportedDataList.length > 0) {
      } else {
        this.showInfoPage = true;
      }
      this.turnSmallLoader = false;
    });
  }
  checkValidation(): boolean {
    if (!this.reportForm.get('name').value) {
      return false;
    }
    if (!this.reportForm.get('email').value) {
      return false;
    }
    if (!this.reportForm.get('url').value) {
      return false;
    }
    if (!this.reportForm.get('detail').value) {
      return false;
    }
    return true;
  }
  saveReport() {
    this.turnSubmitButtonDisable = true;
    if (!this.checkValidation()) {
      this.turnSubmitButtonDisable = false;
      return;
    } else {
      const para: ReportGetData = Object.assign(
        new ReportGetData(),
        this.reportForm.value
      );
      this.reportService.AddReports(para).subscribe(() => {
        this.turnSmallLoader = true;
        this.getReportedData();
        this.turnSubmitButtonDisable = false;
      });
    }
  }
}
