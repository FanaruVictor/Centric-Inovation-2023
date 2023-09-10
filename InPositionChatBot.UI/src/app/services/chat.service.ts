import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { Message } from '../chat/models/message';
import { environment } from 'src/environemnts/environments';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private subject = new Subject<Message | undefined>();
  url = `${environment.apiUrl}/messages`;
  constructor(private httpClient: HttpClient) { }

  sendMessage(message: Message): Observable<boolean>{
    return this.httpClient.post<boolean>(this.url, message);
  }

  addMessage(message: Message): void {
    this.subject.next(message);
  }

  clearMessages() {
    this.subject.next(undefined);
  }

  getMessage(): Observable<Message | undefined> {
    return this.subject.asObservable();
  }
}
