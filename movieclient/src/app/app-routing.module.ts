import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {UserCollectionsComponent} from "./user-collections/user-collections.component";
import {MovieListComponent} from "./movie-list/movie-list.component";
import {LoginComponent} from "./login/login.component";

const routes: Routes = [
  { path: '', component: MovieListComponent, pathMatch: 'full' },
  { path: 'usercollections', component: UserCollectionsComponent },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
