import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-signup-request',
  templateUrl: './signup-request.component.html',
  styleUrl: './signup-request.component.scss'
})
export class SignupRequestComponent implements OnInit {
 
  selectedTab: string='Manager';
 
  ngOnInit(): void {
    console.log(this.selectedTab);

  }

  
  onTabChange(event: any): void {
    this.selectedTab = event.tab.textLabel;
    
    console.log(this.selectedTab);
  }

}
