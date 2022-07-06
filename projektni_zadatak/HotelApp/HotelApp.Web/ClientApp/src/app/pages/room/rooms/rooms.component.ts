import { Component, OnInit,  } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from 'src/app/services/location.service';
import { RoomService } from 'src/app/services/room.service';
import { BehaviorSubject, combineLatest, mergeMap} from 'rxjs';
@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})

export class RoomsComponent implements OnInit {
  locations$ = this.locationService.getLocations();
  numberOfBeds$ = this.roomService.getNumberOfBeds();

  private bedNumberChangedSubject = new BehaviorSubject<number[]>([]);
  bedNumberChangedSubject$ = this.bedNumberChangedSubject.asObservable();

  private locationChangedSubject = new BehaviorSubject<number[]>([]);
  locationChangedSubject$ = this.locationChangedSubject.asObservable();

  private dateChangedSubject = new BehaviorSubject<Date[]>([]);
  dateChangedSubject$ = this.dateChangedSubject.asObservable();

  private sortChangedSubject = new BehaviorSubject<string>("asc");
  sortChangedSubject$ = this.sortChangedSubject.asObservable();

  private pageIndexChangedSubject = new BehaviorSubject<number>(1);
  pageIndexChangedSubject$ = this.pageIndexChangedSubject.asObservable();

  selectedBeds = [];
  selectedLocations = [];
  date: Date[] = [];
  selectedSort = "asc";

  room$ = combineLatest([
      this.pageIndexChangedSubject$,
      this.bedNumberChangedSubject$,
      this.locationChangedSubject$,
      this.dateChangedSubject$,
      this.sortChangedSubject$])
    .pipe(
    mergeMap(([pageNumber, beds, locations, date, sort]) => this.roomService.getRooms(pageNumber,beds, locations, date, sort)));


  totalPages!: number[];

  constructor(
    private roomService: RoomService,
    private locationService: LocationService,
    private router: Router
  ) { }

  ngOnInit(): void {
    
  }
  
  disabledStartDate = (startValue: Date): boolean => {
    if (!startValue) {
      return false;
    }
    return startValue.getTime() < new Date().getTime();
  };

  bedSelecotor(i:any){
    this.bedNumberChangedSubject.next(i);
  }

  locationSelecotor(i:any) {
    this.locationChangedSubject.next(i);
  }
  
  dateSelecotor(i: any) {
    if(i.length > 1){
      this.dateChangedSubject.next(i);
    }
  }
  onChange(i: any) {
    if(i.length == 0) this.dateChangedSubject.next([])
  }

  sortSelecotr(i: string){
    this.sortChangedSubject.next(i);
  }
  switchPage(i: number) {
    this.pageIndexChangedSubject.next(i);
  }


  RoomDetails(id:number) {

    let dateFrom: string | null =  null;
    let dateTo: string | null =  null;
    if(this.date.length > 1) {
      dateFrom = this.date[0].getFullYear() + "-" + (this.date[0].getMonth()+1) + "-" + this.date[0].getDate();
      dateTo = this.date[1].getFullYear() + "-" + (this.date[1].getMonth()+1) + "-" + this.date[1].getDate();
    }

    this.router.navigate(
      [`/rooms/${id}/reservation`], {
        queryParams: {
            dateFrom,
            dateTo
        }
      }
    );
  }
}