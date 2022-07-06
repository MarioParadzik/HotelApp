import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Directive({
  selector: '[appInRole]'
})
export class InRoleDirective {
  role!: string | null;
  constructor(
    private authService: AuthService,
    private templateRef: TemplateRef<any>,
    private viewContainerRef: ViewContainerRef) {
      this.role = this.authService.getCurrentUserRole;
    }

    @Input() set appInRole(showForRole: string[]) {
      const showFor = showForRole || [];
      if(showFor.length > 0){
        this.roleChecker(showFor);
      } else {
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      }
    }

    roleChecker(showFor: string[]) {
      const anonymus = 'Anonymous';
      if(this.role !== null){
        let roles = this.role.split(",")
        
         const contains = showFor.some(r=> roles.includes(r));
        
        return contains ? this.viewContainerRef.createEmbeddedView(this.templateRef) :   this.viewContainerRef.clear();
      } else {
        if(showFor.includes(anonymus)){
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.viewContainerRef.clear();
        }
      }
    }
}
