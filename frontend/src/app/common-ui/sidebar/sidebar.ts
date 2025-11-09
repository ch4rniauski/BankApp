import {Component, inject} from '@angular/core';
import {RouterLink} from '@angular/router';
import {catchError, Observable, of, switchMap} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {AuthService} from '../../data/services/auth.service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-sidebar',
  imports: [
    RouterLink,
    AsyncPipe
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar {
  private authService = inject(AuthService)

  isAuth$ : Observable<boolean> = of(false)

  ngOnInit() {
    this.isAuth$ = this.authService.isAuth()
      .pipe(
        switchMap(isAuth => {
          if (isAuth) {
            return of(true)
          }

          return this.authService.updateAccessToken()
            .pipe(
              catchError((error : HttpErrorResponse) => {
                console.error(error)

                return of(false)
              })
            )
        })
      );
  }
}
