import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Navbar } from "./navbar/navbar";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    Navbar
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  protected readonly title = signal('fitness-app-angular');
}
