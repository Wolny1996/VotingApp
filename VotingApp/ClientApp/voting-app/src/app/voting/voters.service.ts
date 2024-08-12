import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { Voter } from "./voting-vm.service";
import { ApiService } from "./api.service";

@Injectable({
    providedIn: 'root'
  })
  export class VotersService {
    constructor(private apiService: ApiService) {
    }
  
    addVoter(voterFullName: string): Observable<any> {
      return this.apiService.post(`https://localhost:44346/Voters/add-voter`, {
        FullName: voterFullName,
      });
    }
  
    getVoters(): Observable<Voter[]> {
      return this.apiService.get(`https://localhost:44346/Voters/get-voters`);
    }

    vote(candidateId: number, voterId: number): Observable<any> {
      return this.apiService.put(`https://localhost:44346/Voters/voter-voted`, {
        CandidateId: candidateId,
        VoterId: voterId,
      });
    }
  }