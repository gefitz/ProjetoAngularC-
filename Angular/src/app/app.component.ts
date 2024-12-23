import { Component } from '@angular/core';
import { NavPageComponent } from "./components/nav-page/nav-page.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
}
