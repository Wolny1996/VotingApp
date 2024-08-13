import { Injectable } from "@angular/core";

import { Observable } from "rxjs";

import { Candidate } from "./voting-vm.service";
import { ApiService } from "./services/api.service";
import { environment } from "../../environment";

@Injectable({
  providedIn: 'root'
})
export class CandidatesService {
  constructor(private apiService: ApiService) {
  }

  baseApiUrl = environment.baseApiUrl;

  addCandidate(voterFullName: string): Observable<any> {
    return this.apiService.post(`${this.baseApiUrl}/Candidates/add-candidate`, {
      FullName: voterFullName,
    });
  }

  getCandidates(): Observable<Candidate[]> {
    return this.apiService.get(`${this.baseApiUrl}/Candidates/get-candidates`);
  }
}
