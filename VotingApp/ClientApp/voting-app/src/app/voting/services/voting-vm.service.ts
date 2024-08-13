import { Injectable } from "@angular/core";

import { catchError, EMPTY, forkJoin, map, merge, scan, Subject, switchMap, tap } from "rxjs";

import { VotersService } from "./voters.service";
import { CandidatesService } from "./candidates.service";

@Injectable({
    providedIn: 'root'
  })
  export class VotingVMService {
    constructor(
        private candidatesService: CandidatesService,
        private votersService: VotersService) {
    }
  
    /** Calls */
    addCandidate(fullName: string): void {
      this.addCandidateSubject.next(fullName);
    }

    addVoter(fullName: string): void {
      this.addVoterSubject.next(fullName);
    }

    getDataForVoting(): void {
      this.getDataForVotingSubject.next();
    }

    vote(vote: {candidateId: number, voterId: number}): void {
      this.voteSubject.next(vote);
    }
  
    /** Subjects */
    private addCandidateSubject = new Subject<string>();
    private addCandidateAction$ = this.addCandidateSubject.asObservable().pipe(
      switchMap((fullName: string) => {
        return this.candidatesService.addCandidate(fullName).pipe(
          catchError(() => {
            return EMPTY;
          })
        );
      }),
      switchMap(() => {
        return this.candidatesService.getCandidates().pipe(
          catchError(() => {
            return EMPTY;
          })
        );
      }),
      map((candidates) => (vm: VotingVM) => ({
        ...vm,
        Candidates: candidates,
      }))
    );

    private addVoterSubject = new Subject<string>();
    private addVoterAction$ = this.addVoterSubject.asObservable().pipe(
      switchMap((fullName: string) => {
        return this.votersService.addVoter(fullName).pipe(
          catchError(() => {
            return EMPTY;
          })
        );
      }),
      switchMap(() => {
        return this.votersService.getVoters().pipe(
          catchError(() => {
            return EMPTY;
          })
        );
      }),
      map((voters) => (vm: VotingVM) => ({
        ...vm,
        Voters: voters,
      }))
    );

    private getDataForVotingSubject = new Subject<void>();
    private getDataForVotingAction$ = this.getDataForVotingSubject.asObservable().pipe(
      switchMap(() => {
        return forkJoin([
            this.candidatesService.getCandidates(),
            this.votersService.getVoters(),
        ]).pipe(
          catchError(() => {
            return EMPTY;
          })
        );
      }),
      map(([candidates, voters]) => (vm: VotingVM) => ({
        ...vm,
        Candidates: candidates,
        Voters: voters,
      } as VotingVM))
    );

    private voteSubject = new Subject<{candidateId: number, voterId: number}>();
    private voteAction$ = this.voteSubject.asObservable().pipe(
      switchMap(({candidateId, voterId}) => {
        return this.votersService.vote(candidateId, voterId).pipe(
          tap(() => this.getDataForVoting),
          catchError(() => {
            return EMPTY;
          }),
        )
      }),
      tap(() => this.getDataForVoting()),
      map(() => (vm: VotingVM) => ({
        ...vm,
      }))
    );
  
    /** VM definition */
    vm$ = merge(
      this.addCandidateAction$,
      this.addVoterAction$,
      this.getDataForVotingAction$,
      this.voteAction$
    ).pipe(
      scan(
        (
          prevVm: VotingVM,
          mutationFn: (vm: VotingVM) => VotingVM
        ) => mutationFn(prevVm),
        {
            Candidates: [],
            Voters: [],
        } as VotingVM
        )
    )
  }
  
  export interface VotingVM {
    Candidates: Candidate[];
    Voters: Voter[];
  }

  export interface Person {
    Id: number;
    FullName: string;
  }

  export interface Candidate extends Person {
    NumberOfVotes: number;
  }

  export interface Voter extends Person {
    HasVoted: boolean;
  };

  export interface Vote {
    CandidateId: number;
    VoterId: number;
  }