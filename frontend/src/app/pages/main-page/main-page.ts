import {Component, inject} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {AuthService} from '../../data/services/auth.service';
import {AsyncPipe} from '@angular/common';
import {catchError, Observable, of, switchMap} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-main-page',
  imports: [
    Sidebar,
    AsyncPipe
  ],
  templateUrl: './main-page.html',
  styleUrl: './main-page.scss',
})
export class MainPage {
  private authService = inject(AuthService)
  isAuth$! : Observable<boolean>

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
