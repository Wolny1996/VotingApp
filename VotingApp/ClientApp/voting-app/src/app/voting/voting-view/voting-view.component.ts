import { Component, Input, AfterViewInit, OnInit } from '@angular/core';

import { Candidate, Voter } from '../voting-vm.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-voting-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './voting-view.component.html',
  styleUrl: './voting-view.component.scss'
})
export class VotingViewComponent implements OnInit, AfterViewInit {
  @Input() candidates: Candidate[] = [];
  @Input() voters: Voter[] = [];

  hehe: string[] = ["a", "b"]; 

  ngOnInit(): void {
    console.log("child on init");
    console.log(this.candidates);
    console.log(this.voters);
  }

  ngAfterViewInit(): void {
    console.log("child after");
    console.log(this.candidates);
    console.log(this.voters);
  }
}
