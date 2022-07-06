import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first, Observable } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { ConfigurationService } from 'src/app/services/configuration.service';
import { ReservationService } from 'src/app/services/reservation.service';
import { Configuration } from 'src/model/configuration.model';

@Component({
  selector: 'app-configurations',
  templateUrl: './configurations.component.html',
  styleUrls: ['./configurations.component.css']
})
export class ConfigurationsComponent implements OnInit {
  configuration$!: Observable<Configuration[]>;
  editId!: number;
  valueChange!: string;
  configuration!: Configuration;
  isVisible = false;
  validateForm!: FormGroup;

  constructor(private configService: ConfigurationService,
    private reservationService: ReservationService,
    private alertService: AlertService,
    private fb: FormBuilder) {
      this.validateForm = this.fb.group({
        key: [null, [Validators.required]],
        value: [null, [Validators.required]],
        type: [null, [Validators.required]],
        description: [null, [Validators.required]]
      });
}

  ngOnInit(): void {
    this.configuration$ = this.configService.getConfigurations();
  }

  startEdit(id: number): void {
    this.editId = id;
  }

  stopEdit(value: string): void {
    this.configService.updateConfiguration(this.editId, value).subscribe(
      data => {
        this.alertService.success('Configuration updated!');
        this.ngOnInit();
      },
      err => {
        this.alertService.error(err.error.error);
      });
    this.editId = 0;
  }

  get formParameter() { return this.validateForm.controls; }

  showModal(): void {
    this.isVisible = true;
  }

  handleCancel(): void {
    this.isVisible = false;
  }

  sync(){
    this.reservationService.syncExternalServices().subscribe(
      data => {
        this.alertService.success('Reservation sync!');
      },
      err => {
        this.alertService.error(err.error.error);
      });
  }
}
