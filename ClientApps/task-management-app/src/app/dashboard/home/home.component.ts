import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ToolbarComponent } from '../../shared/toolbar/toolbar.component';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [
    RouterLink,
    HttpClientModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    ToolbarComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
