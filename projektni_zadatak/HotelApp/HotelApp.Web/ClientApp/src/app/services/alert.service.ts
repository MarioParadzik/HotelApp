import { Injectable } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';


@Injectable({providedIn:'root'})
export class AlertService {


    constructor(private messageService: NzMessageService) {
    }

    success(message: string) {
        this.messageService.success(message,{
            nzDuration: 5000
        });
    }

    error(message: string) {
        this.messageService.error(message,{
            nzDuration: 5000
        });
    }

    info(message: string) {
        this.messageService.info(message,{
            nzDuration: 5000
        });
    }

    warn(message: string) {
        this.messageService.warning(message,{
            nzDuration: 5000
        });
    }
}