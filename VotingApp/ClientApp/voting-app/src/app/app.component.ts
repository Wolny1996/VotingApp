import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { VotingComponent } from './voting/voting.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, VotingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'voting-app';
}
