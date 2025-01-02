import { Component, inject, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';
import { filter, map, shareReplay } from 'rxjs/operators';
import { AsyncPipe, NgIf } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';
import {MatMenuModule} from '@angular/material/menu'

@Component({
  selector: 'app-nav-page',
  templateUrl: './nav-page.component.html',
  styleUrls: ['./nav-page.component.css'],
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    AsyncPipe,
    RouterLink,
    RouterOutlet,
    MatMenuModule
  ]
})
export class NavPageComponent implements OnInit {
  private breakpointObserver = inject(BreakpointObserver);
  titlePage:string = 'Produtos';
  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches),
    shareReplay()
  );
   constructor(private route: Router,private routeActive: ActivatedRoute){}
  ngOnInit(): void {
    this.route.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(()=>
      {
        const currentRoute = this.getPage(this.routeActive);
        this.titlePage = currentRoute.snapshot.data['title'];
      })
  }
    private getPage(page:ActivatedRoute): ActivatedRoute{
      while(page.firstChild){
        page = page.firstChild;
      }
      return page;
    }
}
