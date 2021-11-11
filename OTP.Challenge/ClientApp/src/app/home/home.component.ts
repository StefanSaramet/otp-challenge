import { Component, Inject, OnDestroy, ViewChild } from '@angular/core';
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
export class HomeComponent {
  userId: string = '';
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
    leftTime: 30,
  };

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string
  ) {
    this.baseUrl = baseUrl;
    this.userIdForm = this.formBuilder.group({
      userId: ['', Validators.required],
    });
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

    this.http
      .get<OtpModel>(this.baseUrl + `otp/generate/${this.userId}`)
      .subscribe((res) => {
        this.otp = res;
        this.config = { ...this.config, leftTime: this.otp.validFor };
        this.countdown?.begin();
      });
  }

  handleEvent(ev: CountdownEvent) {
    if (ev.action === 'done') {
      this.http
        .get<OtpModel>(this.baseUrl + `otp/generate/${this.userId}`)
        .subscribe((res) => {
          this.otp = res;
          this.config = { ...this.config, leftTime: this.otp.validFor };
          this.countdown?.begin();
        });
    }
  }
}
