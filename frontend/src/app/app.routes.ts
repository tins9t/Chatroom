import { Routes } from '@angular/router';
import { RoomComponent } from '../room/room.component';
import { SignInComponent } from '../sign-in/sign-in.component';
export const routes: Routes = [
  {
    path: 'room/:roomId',
    component: RoomComponent
  },
  {
    path: 'signin',
    component: SignInComponent
  },
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  }];
