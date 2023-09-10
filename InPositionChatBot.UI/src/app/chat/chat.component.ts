import { Component, OnDestroy  } from '@angular/core';
import { UIMessage, State, Sender } from './models/message'
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { FaqService } from '../services/faq.service';
import { ChatService } from '../services/chat.service';
import { uid } from 'uid';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnDestroy {
  text: string = '';
  isWaitingForResponse = false;
  
  messages: UIMessage[] = [
    {id: uid(), sender: Sender.User, text: "Give me spar configuration", date: new Date(), state: State.success},
    {
      id: uid(),
      sender: Sender.AI, 
      text: `
        <?xml version="1.0" encoding="UTF-8"?>
        <root>
          <person>
            <name>John Doe</name>
            <age>30</age>
            <city>New York</city>
          </person>
          <person>
            <name>Jane Smith</name>
            <age>25</age>
            <city>Los Angeles</city>
          </person>
        </root>
      `, 
      date: new Date(),
      state: State.success
    },
    {id: uid(), sender: Sender.User, text: "Give me all retailers", date: new Date(), state: State.error},
    {id: uid(), sender: Sender.AI, text: "Give me all retailers", date: new Date(), state: State.noResponse},
  ]

  faqSubscription: Subscription = new Subscription();
  messagesSubscription: Subscription = new Subscription();

  constructor(
    private faqService: FaqService,
    private chatApiService: ChatService,
    private toastr: ToastrService){
      this.initiateChatMessages();
      this.initiateFAQ();
  }

  initiateFAQ(){
    this.faqSubscription = this.faqService.getMessage().subscribe(x => {
      if(!x){
        return;
      }
      
      this.sendMessage(x);
    })
  }

  initiateChatMessages(){
    this.messagesSubscription = this.chatApiService.getMessage().subscribe(x => {
      if(!x){
        return;
      }

      const uiMessage = {...x, state: State.success};
      this.messages.push(uiMessage);
    });
  }


  sendMessage(text: string){
    if(text !== '' && !this.isWaitingForResponse){
      const message = {
        id: uid(32),
        sender: Sender.User,
        text: text,
        date: new Date()
      };

      const uiMessage = {...message, state: State.noResponse};
      this.messages.push(uiMessage);

      this.chatApiService.sendMessage(message).subscribe(response => {
        if(response){
          this.handleSuccess(uiMessage);
        }
        else {
          this.handleError(uiMessage);
        }
      });
  
      this.text = '';
    }
    else{
      this.toastr.warning("You can not send an empty message", "Message not send");
    }
  }

  handleSuccess(uiMessage: UIMessage){
    this.isWaitingForResponse = true;
    
    const result = this.messages.find(x => x.id === uiMessage.id);

    if(result){
      result.state = State.success;
    }
    
    this.toastr.success('The message was send succesfully', 'Request completed');
  }

  handleError(uiMessage: UIMessage){
    const result = this.messages.find(x => x.id === uiMessage.id);

    if(result){
      result.state = State.error;
    }

    this.toastr.error('There was a problem while executing the request', 'Request failed');
  }

  restartConversation() {
    if(!this.isWaitingForResponse){
      this.messages = [];
      this.text = '';
    }
  }

  resendLastMessage(){
    if(this.isWaitingForResponse){
      return;
    }

    const humanMessages = this.messages.filter(x => x.sender === Sender.User);

    const lastMessage = {...humanMessages[humanMessages.length - 1]};
    lastMessage.date = new Date();

    this.messages.push(lastMessage);
  }

  ngOnDestroy(): void {
    this.faqSubscription.unsubscribe();
    this.messagesSubscription.unsubscribe();
  }
}
