import { Component, Input, AfterViewInit, Output, EventEmitter } from '@angular/core';

import { Candidate, Voter } from '../voting-vm.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-voting-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './voting-view.component.html',
  styleUrl: './voting-view.component.scss'
})
export class VotingViewComponent implements AfterViewInit {
  @Input() candidates: Candidate[] = [];
  @Input() voters: Voter[] = [];

  @Output() addCandidate: EventEmitter<string> = new EventEmitter();
  @Output() addVoter: EventEmitter<string> = new EventEmitter();
  @Output() vote: EventEmitter<{candidateId: number, voterId: number}> = new EventEmitter();

  ngAfterViewInit(): void {
    console.log(this.candidates);
    console.log(this.voters);
  }

  createC(): void {
    this.addCandidate.emit("HeheC");
  }

  createV(): void {
    this.addVoter.emit("HeheV");
  }
}
