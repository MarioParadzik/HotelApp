import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  validateForm!: FormGroup;
  loading = false;
  submitted = false;
  
  constructor(private fb: FormBuilder, 
    private authService: AuthService,
    private alertService: AlertService) {
    }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      email: [null, [Validators.required, , Validators.email]],
      password: [null, [Validators.required]],
      remember: [true]
    });
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      this.submitted = true;
      this.loading = true;

      this.loading = true;
      this.authService.login(this.formParameter['email'].value, this.formParameter['password'].value)
        .pipe(first())
        .subscribe(
          data => {
            const { returnUrl } = window.history.state;
            window.location.replace(returnUrl || '/');
          },
          err => {
            this.alertService.error(err.error.error);
            this.loading = false;
          }
        );
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  get formParameter() { return this.validateForm.controls; }
}
