import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first, Observable } from 'rxjs';
import { AdministratorService } from 'src/app/services/administrator.service';
import { AlertService } from 'src/app/services/alert.service';
import { User } from 'src/model/user.model';

@Component({
  selector: 'app-admin-list',
  templateUrl: './admin-list.component.html',
  styleUrls: ['./admin-list.component.css']
})
export class AdminListComponent implements OnInit {
  adminList$!: Observable<User[]>;
  validateForm!: FormGroup;
  isVisible = false;
  constructor(private administratorService: AdministratorService,
              private fb: FormBuilder,
              private alertService: AlertService) { }

  ngOnInit(): void {
    this.adminList$ = this.administratorService.getAdmins();
    this.validateForm = this.fb.group({
      firstName: [null, [Validators.required]],
      lastName: [null, [Validators.required]],
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required, Validators.minLength(8), Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{7,}')]],
    });
  }

  get formParameter() { return this.validateForm.controls; }

  addAdministrator(){
    if (this.validateForm.valid) {
      this.administratorService.createAdmin(
        this.formParameter['firstName'].value,
          this.formParameter['lastName'].value,
          this.formParameter['email'].value,
          this.formParameter['password'].value,
      )
        .pipe(first())
        .subscribe(
          data => {
            this.alertService.success('Added Administrator!');
            this.isVisible = false;
            this.ngOnInit();
          },
          err => {
            this.alertService.error(err.error.error);
            this.isVisible = false;
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

  deleteadministrator(id: number){
    this.administratorService.deleteAdmin(id).subscribe(
      data => {
        this.alertService.success('Room deleted!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }

  showModal() {
    this.isVisible = true;
  }

  handleCancel(){
    this.isVisible = false;
  }

  handleOk(): void {
    this.addAdministrator()
    this.isVisible = false;
  }

}
