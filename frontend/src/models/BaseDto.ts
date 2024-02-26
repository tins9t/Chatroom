import { Message } from "./entities";

export class BaseDto<T>
{
  eventType: string;

  constructor(init?: Partial<T>)
  {
    this.eventType = this.constructor.name;
    Object.assign(this, init)
  }
}

export class ServerEchosClientDto extends BaseDto<ServerEchosClientDto>{
  echoValue?: string;
}

export class ClientBroadcastDto extends BaseDto<ClientBroadcastDto>{
  broadcastValue?: string;
}

export class ServerAddsClientToRoomDto extends BaseDto<ServerAddsClientToRoomDto>{
  messsage?: string;
}

export class ServerBroadcastsMessageWithUsernameDto extends BaseDto<ServerBroadcastsMessageWithUsernameDto>{
  message?: Message;
}

export class ServerSendsOlderMessagesToClientDto extends BaseDto<ServerSendsOlderMessagesToClientDto> {
  roomId?: number;
}
