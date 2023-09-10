import { Component } from '@angular/core';
import { FaqService } from '../services/faq.service';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.scss']
})
export class FaqComponent {
  FAQS: string[] = [
    "What is InPosition?",
    "Give me all retailers",
    "What modules are active for Vomar?",
    "What config do I need for The Sting?",
    "Which params are needed in order to enable BascketCheck?",
    "Give mee Spar configuration"
  ];

  constructor(private faqService: FaqService){

  }

  sendFAQ(faq: string){
    this.faqService.sendMessage(faq);
  }
}
