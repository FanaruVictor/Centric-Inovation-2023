import { Injectable } from '@angular/core';
import { environment } from '../../environemnts/environments';
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { ChatService } from './chat.service';

@Injectable({
  providedIn: 'root'
})
export class ClientEventsService {
  private readonly reconnectIntervals: number[];
  private retryConnectionInterval: any;
  private retryConnectionIntervalIndex = 0;
  private retryIntervalMs: number = -1;
  private eventHubConnection!: HubConnection;
  private connectionUrl = environment.apiUrl.replace('api', 'hub');
  
  constructor(private chatApiService: ChatService) {
    if (!environment.production) {
      this.reconnectIntervals = [ 0, 5000, 10000];
      return;
    }

    this.reconnectIntervals = [0, 2000, 5000, 10000, 30000, 60000, 300000];
  }

  startConnection(accessToken: string) {
    if (
      this.eventHubConnection &&
      this.eventHubConnection.state === HubConnectionState.Connected
    ) {
      console.error('Tried to connect but already connected');
      return;
    }
    this.eventHubConnection = new HubConnectionBuilder()
      .withUrl(this.connectionUrl, {
        accessTokenFactory: () => `${accessToken}`,
      })
      .withAutomaticReconnect(this.reconnectIntervals)
      .configureLogging(LogLevel.Information)
      .build();

    this.eventHubConnection.serverTimeoutInMilliseconds = 2 * 60 * 60 * 1000;
    this.eventHubConnection.keepAliveIntervalInMilliseconds = 5000;

    this.establishConnection();
  };

  stopConnection() {
    if (
      this.eventHubConnection &&
      this.eventHubConnection.state === HubConnectionState.Connected
    ) {
      this.eventHubConnection
        .stop()
        .then(() => console.warn('Connection to client events Hub was closed'))
        .catch((reason) =>
          console.error(`Error while disconnecting from events hub: ${reason}`)
        );
    }
  }

  private initializeListener(): void {
    //type for data needs to be changed from any to somenthing else
    this.eventHubConnection.on('client-events', (data: any) => {
      //logic needs to be changed
      if(data) {
        this.chatApiService.addMessage(data);
      }
    });
  }

  private establishConnection() {
    this.eventHubConnection
      .start()
      .then(() => {
        this.initializeListener();
        this.onConnected(this.eventHubConnection.connectionId);
      })
      .catch((err) => this.onConnectionFailure(err));
  }

  private onConnected(connectionId: string | null): void {
    if (!connectionId) {
      console.error('ConnectionId is null');
      return;
    }
    this.retryConnectionIntervalIndex = 0;
  }

  private onConnectionFailure(error: any): void {
    if (!this.retryConnectionInterval) {
    }
    console.error('Error while starting connection with events hub: ', error);

    this.retryIntervalMs =
      this.reconnectIntervals[this.retryConnectionIntervalIndex];

    clearInterval(this.retryConnectionInterval);
    this.retryConnectionIntervalIndex++;

    if (this.retryIntervalMs === null) {
      return;
    }

    this.retryConnectionInterval = setInterval(
      () => this.establishConnection(),
      this.retryIntervalMs
    );
  }

}
