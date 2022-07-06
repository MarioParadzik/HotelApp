import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Room } from 'src/model/room.model';

@Component({
  selector: 'app-room-form',
  templateUrl: './room-form.component.html',
  styleUrls: ['./room-form.component.css']
})
export class RoomFormComponent implements OnInit {
  validateForm!: FormGroup;
  room: Room | null = null;
  roomId!: number;
  constructor(private fb: FormBuilder) { }

  @Input() set roomData(roomInfo: Room){
    this.room = roomInfo;
    this.roomId = roomInfo.id;
  }
  
  @Output() myValueSubmited = new EventEmitter<Room>();

  submitForm(): void {
    if (this.validateForm.valid) {
      this.room = new Room;
      this.room.id = this.roomId,
      this.room.name = this.formsParameters['name'].value,
      this.room.numberOfBeds = this.formsParameters['numberOfBeds'].value,
      this.room.price = this.formsParameters['price'].value,
      this.myValueSubmited.emit(this.room);
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  get formsParameters() { return this.validateForm.controls; }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      name: [null, [Validators.required]],
      numberOfBeds: [null, [Validators.required, Validators.pattern("^[0-9]*$")]],
      price: [null, [Validators.required, Validators.pattern("([0-9]*[.,]?[0-9]*?)$")]]
    });
  }

}
