import {BaseDto} from "./BaseDto";

export class ClientWantsToBroadcastToRoom extends BaseDto<ClientWantsToBroadcastToRoom> {
  roomId?: number;
  messageContent?: string;
}
