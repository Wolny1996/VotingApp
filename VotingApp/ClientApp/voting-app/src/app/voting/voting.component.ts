import { AfterViewInit, Component, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';

import { Observable, Subscription, tap } from 'rxjs';

import { VotingViewComponent } from './voting-view/voting-view.component';
import { VotingVM, VotingVMService } from './voting-vm.service';

@Component({
  selector: 'app-voting',
  standalone: true,
  imports: [VotingViewComponent, CommonModule],
  templateUrl: './voting.component.html',
  styleUrl: './voting.component.scss',
})
export class VotingComponent implements AfterViewInit, OnDestroy {
  vm$: Observable<VotingVM>;

  private subscriptions: Subscription = new Subscription();

  constructor(private votingVMService: VotingVMService) {
    this.vm$ = this.votingVMService.vm$.pipe(tap(x => console.log(x, "pipe")));
  }

  ngAfterViewInit(): void {
    console.log("parent");
    this.votingVMService.getDataForVoting();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
