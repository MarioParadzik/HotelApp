import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  validateForm!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
    private fb: FormBuilder,
    private alertService: AlertService,
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, [Validators.email, Validators.required]],
      password: [ null, [Validators.required, Validators.minLength(8), Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{7,}')]],
      confirmPassword: [null, [Validators.required, this.confirmationValidator]],
    });

  }

  get formParameter() { return this.validateForm.controls; }
  
  submitForm(): void {
    this.submitted = true;
    if (this.validateForm.valid) {
      this.loading = true;
      this.authService.register(
        this.formParameter['firstName'].value,
        this.formParameter['lastName'].value,
        this.formParameter['email'].value,
        this.formParameter['password'].value,
        this.formParameter['confirmPassword'].value
      )
        .pipe(first())
        .subscribe(
          data => {
            this.alertService.success('Registration successful');
            const { returnUrl } = window.history.state;
            window.location.replace(returnUrl || '/');
          },
          err => {
            this.alertService.error(err.error.error);
            this.loading = false;
          });
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  updateConfirmValidator(): void {
    Promise.resolve().then(() => this.validateForm.controls["confirmPassword"].updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls["password"].value) {
      return { confirm: true, error: true };
    }
    return {};
  };


}
