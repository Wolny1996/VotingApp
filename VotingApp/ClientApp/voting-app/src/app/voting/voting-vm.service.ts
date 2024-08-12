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
    getDataForVoting(): void {
      console.log("next");
      this.getDataForVotingSubject.next();
    }
  
    /** Subjects */
    private getDataForVotingSubject = new Subject<void>();
    private getDataForVotingAction$ = this.getDataForVotingSubject.asObservable().pipe(
      switchMap(() => {
        console.log("sub");
        return forkJoin([
            this.candidatesService.getCandidates(),
            this.votersService.getVoters(),
        ]).pipe(
          tap(x => console.log(x, "sub")),
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
  
    /** VM definition */
    vm$ = merge(
      this.getDataForVotingAction$,
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