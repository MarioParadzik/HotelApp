import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {path:"identity", loadChildren:() =>import('./identity/identity.module').then(x => x.IdentityModule)},
  {path:"", loadChildren:() =>import('./pages/pages.module').then(x => x.PagesModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
