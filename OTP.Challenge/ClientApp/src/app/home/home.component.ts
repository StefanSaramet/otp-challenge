import { Component, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OtpModel } from '../models/otp.model';
import { HttpClient } from '@angular/common/http';
import {
  CountdownComponent,
  CountdownConfig,
  CountdownEvent,
} from 'ngx-countdown';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  KEY = 'time';
  DEFAULT = 30;
  userId: string = '';
  selectedDate: string = '';

  showOtpWithTimer = false;

  otp: OtpModel = {
    otp: '',
    validFor: 0,
  };

  userIdForm: FormGroup;

  baseUrl: string;

  @ViewChild('cd', { static: false }) private countdown:
    | CountdownComponent
    | undefined;

  config: CountdownConfig = {
    leftTime: this.DEFAULT,
  };

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.userIdForm = this.formBuilder.group({
      userId: ['', Validators.required],
      selectedDate: [''],
    });
  }

  ngOnInit(): void {
    let value = +localStorage.getItem(this.KEY)!! ?? this.DEFAULT;
    if (value <= 0) value = this.DEFAULT;
    this.config = { ...this.config, leftTime: value };
  }

  startGeneration(): void {
    if (!this.userIdForm.valid) {
      Object.keys(this.userIdForm.controls).forEach((field) => {
        const control = this.userIdForm.get(field);
        control?.markAsTouched({ onlySelf: true });
      });
    }

    this.userId = this.userIdForm.value.userId;
    this.showOtpWithTimer = true;

    this.selectedDate = this.userIdForm.value.selectedDate;

    this.invokeOtp();
  }

  handleEvent(ev: CountdownEvent) {
    if (ev.action === 'done') {
      this.invokeOtp();
    }

    if (ev.action === 'notify') {
      // Save current value
      localStorage.setItem(this.KEY, `${ev.left / 1000}`);
    }
  }

  private invokeOtp() {
    if (this.selectedDate == '') {
      this.http
        .get<OtpModel>(this.baseUrl + `otp/generate/${this.userId}`)
        .subscribe((res) => {
          this.otp = res;
          this.config = { ...this.config, leftTime: this.otp.validFor };
          this.countdown?.begin();
        });
    } else {
      this.http
        .post<OtpModel>(this.baseUrl + `otp/generate/specific/date`, {
          userId: this.userId,
          requestedDate: this.selectedDate,
        })
        .subscribe((res) => {
          this.otp = res;
          this.config = { ...this.config, leftTime: this.otp.validFor };
          this.countdown?.begin();
        });
    }
  }
}
