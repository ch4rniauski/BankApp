import {Component, inject} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {Router, RouterLink} from '@angular/router';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../data/services/auth.service';
import {HttpErrorResponse} from '@angular/common/http';
import {take, tap} from 'rxjs';

@Component({
  selector: 'app-login-page',
  imports: [
    Sidebar,
    RouterLink,
    ReactiveFormsModule
  ],
  templateUrl: './login-page.html',
  styleUrl: './login-page.scss',
})
export class LoginPage {
  private authService = inject(AuthService)
  private fb = inject(FormBuilder)
  private router = inject(Router)

  form = this.fb.nonNullable.group({
    email: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.email
    ]),
    password: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(8)
    ]),
  })

  onsubmit() {
    if (this.form.valid) {
      this.authService.loginClient(this.form.getRawValue())
        .subscribe({
          next: result => {
            localStorage.setItem('access_token', result.accessToken);
            localStorage.setItem('refresh_token', result.refreshToken);
            localStorage.setItem('client_id', result.clientId);

            this.router.navigate(['/']);
          },
          error: (error : HttpErrorResponse) => {
            console.error(error);
          }
        })
    } else {
      this.form.markAllAsTouched()
    }
  }
}
