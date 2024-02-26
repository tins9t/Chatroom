import { Component } from '@angular/core';
import {FormControl, ReactiveFormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {Router} from "@angular/router";
import {WebsocketService} from "../service/websocketservice.service";

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [ ReactiveFormsModule, CommonModule ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.less'
})
export class SignInComponent {
  username = new FormControl('')
  roomId = new FormControl('')

  eventType: string = "";

  constructor(private router: Router, private service: WebsocketService) {
    setTimeout(() => {
      this.service.ws.send("hey")

    }, 2000)
  }

  clickEnterRoom() {
    var object = {
      eventType: "ClientWantsToSignIn",
      username: this.username.value
    }
    this.service.ws.send(JSON.stringify(object))
    var object2 = {
      eventType: "ClientWantsToEnterRoom",
      roomId: this.roomId.value
    }
    this.service.ws.send(JSON.stringify(object2))
    this.router.navigate(['/room/' + this.roomId.value], {replaceUrl: true})
  }
}
