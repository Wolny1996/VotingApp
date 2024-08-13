import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { Voter } from "./voting-vm.service";
import { ApiService } from "./services/api.service";
import { environment } from './../../environment';

@Injectable({
    providedIn: 'root'
  })
  export class VotersService {
    constructor(private apiService: ApiService) {
    }

    baseApiUrl = environment.baseApiUrl;
  
    addVoter(voterFullName: string): Observable<any> {
      return this.apiService.post(`${this.baseApiUrl}/Voters/add-voter`, {
        FullName: voterFullName,
      });
    }
  
    getVoters(): Observable<Voter[]> {
      return this.apiService.get(`${this.baseApiUrl}/Voters/get-voters`);
    }

    vote(candidateId: number, voterId: number): Observable<any> {
      return this.apiService.put(`${this.baseApiUrl}/Voters/voter-voted`, {
        CandidateId: candidateId,
        VoterId: voterId,
      });
    }
  }
