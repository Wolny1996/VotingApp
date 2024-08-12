import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { Candidate } from "./voting-vm.service";
import { ApiService } from "./api.service";

@Injectable({
    providedIn: 'root'
  })
  export class CandidatesService {
    constructor(private apiService: ApiService) {
    }
  
    addCandidate(voterFullName: string): Observable<any> {
      return this.apiService.post(`Candidates/add-candidate`, {
        FullName: voterFullName,
      });
    }
  
    getCandidates(): Observable<Candidate[]> {
      console.log("can");
      return this.apiService.get(`https://localhost:44346/Candidates/get-candidates`);
    }
  }