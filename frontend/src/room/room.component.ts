import { Component } from '@angular/core';
import {FormControl, ReactiveFormsModule} from "@angular/forms";
import { BaseDto, ClientBroadcastDto, ServerEchosClientDto, ServerAddsClientToRoomDto, ServerBroadcastsMessageWithUsernameDto, ServerSendsOlderMessagesToClientDto } from '../models/BaseDto';
import {CommonModule} from "@angular/common";
import {ActivatedRoute} from "@angular/router";
import {WebsocketService} from "../service/websocketservice.service";
import {ClientWantsToBroadcastToRoom} from "../models/clientWantsToBroadcastToRoom";
import { Message } from '../models/entities';
@Component({
  selector: 'app-room',
  standalone: true,
  imports: [ CommonModule, ReactiveFormsModule ],
  templateUrl: './room.component.html',
  styleUrl: './room.component.css'
})
export class RoomComponent {

  messages: string[] = [];
  eventType: string = "";
  //olderMessages: Message[] = [];

  messagesContent = new FormControl('')
  username: string = "";
  messageFromUser: string = "";

  constructor(private route: ActivatedRoute, public service: WebsocketService) {

    this.service.ws.onmessage = message => {
      const messageFromServer = JSON.parse(message.data) as BaseDto<any>
      try {
        // @ts-ignore
        this[messageFromServer.eventType]?.call(this, messageFromServer);
      } catch (error) {
        console.error('Error during method call:', error);
      }
    }
  }

  ServerEchosClient(dto: ServerEchosClientDto){
    this.messages.push(dto.echoValue!);
  }

  ClientBroadcast(dto: ClientBroadcastDto){
    this.messages.push(dto.broadcastValue!);
  }

  /*ServerBroadcastsMessageWithUsername(dto: ServerBroadcastsMessageWithUsernameDto){
    this.messageFromUser = ""+dto.message
    this.username = ""+dto.username
  }*/

  async ServerAddsClientToRoom(dto: ServerAddsClientToRoomDto){
    console.log("ServerAddsClientToRoom: "+dto.messsage);
  }

  async ServerSendsOlderMessagesToClient(dto: ServerSendsOlderMessagesToClientDto){
    //this.olderMessages = dto.messages ?? [];
  }

  clickEcho(){
    this.eventType = "ClientWantsToEchoServer"
  }

  clickBroadcast(){
    this.eventType = "ClientWantsToBroadcast"
  }

  getRoomId(){
    return this.route.snapshot.params['roomId'];
  }

  getMessages() {
    console.log("Messages: "+this.service.roomsWithMessages)
    return this.service.roomsWithMessages;
  }

  sendMessage(){
    this.service.sendDto(new ClientWantsToBroadcastToRoom({roomId: Number.parseInt(this.getRoomId()), messageContent: this.messagesContent.value!}))
  }
}
