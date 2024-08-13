import { Component, Input, Output, EventEmitter, OnInit, inject } from '@angular/core';
import {MatSelectModule} from '@angular/material/select';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { AddModal } from '../components/add-modal/add-modal';
import { Candidate, Voter } from '../services/voting-vm.service';

@Component({
  selector: 'app-voting-view',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule,
    MatFormFieldModule, MatSelectModule, MatInputModule],
  templateUrl: './voting-view.component.html',
  styleUrl: './voting-view.component.scss'
})
export class VotingViewComponent implements OnInit {
  @Input() candidates: Candidate[] = [];
  @Input() voters: Voter[] = [];

  @Output() addCandidate: EventEmitter<string> = new EventEmitter();
  @Output() addVoter: EventEmitter<string> = new EventEmitter();
  @Output() vote: EventEmitter<{candidateId: number, voterId: number}> = new EventEmitter();

  form: FormGroup = new FormGroup({});
  readonly dialog = inject(MatDialog);
  readonly votingTypes = VotingType;

  ngOnInit(): void {
    this.form = new FormGroup({
      CandidateId: new FormControl(null, [Validators.required]),
      VoterId: new FormControl(null, [Validators.required]),
    });
  }

  get voterFormControl() : FormControl {
    return this.form.get("VoterId") as FormControl;
  }
 
  get candidateFormControl() : FormControl {
    return this.form.get("CandidateId") as FormControl;
  }

  openDialog(votingType: VotingType): void {
    const dialogRef = this.dialog.open(AddModal, {
      data: votingType,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        result.VotingType == VotingType.Candidate
        ? this.createCandidate(result.FullName)
        : this.createVoter(result.FullName);
      }
    });
  }

  createCandidate(fullName: string): void {
    this.addCandidate.emit(fullName);
  }

  createVoter(fullName: string): void {
    this.addVoter.emit(fullName);
  }

  castVote(): void {
    this.form.markAllAsTouched();

    if (this.form.valid) {
      let data = this.form.getRawValue();

      let model: {candidateId: number, voterId: number} = {
        candidateId: data.CandidateId,
        voterId: data.VoterId,
      }

      this.vote.emit(model);
      this.form.reset();
    }
  }
}

export enum VotingType {
  Candidate = 0,
  Voter = 1,
}
