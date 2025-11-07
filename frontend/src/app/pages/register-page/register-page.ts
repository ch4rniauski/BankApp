import {Component, inject} from '@angular/core';
import {Sidebar} from "../../common-ui/sidebar/sidebar";
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {AuthService} from '../../data/services/auth.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-page',
  imports: [
    Sidebar,
    ReactiveFormsModule
  ],
  templateUrl: './register-page.html',
  styleUrl: './register-page.scss',
})
export class RegisterPage {
  private authService = inject(AuthService)
  private router = inject(Router)
  private fb = inject(FormBuilder)

  errorMessage : string | null = null

  form = this.fb.nonNullable.group({
    firstName: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50)
    ]),
    lastName: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(100)
    ]),
    phoneNumber: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.pattern(/^\+?[0-9]{9,15}$/)
    ]),
    email: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.email
    ]),
    password: this.fb.nonNullable.control('', [
      Validators.required,
      Validators.minLength(8)
    ])
  })

  onsubmit() {
    if (this.form.valid){
      this.authService.registerClient(this.form.getRawValue())
        .subscribe({
          next: _ => {
            this.router.navigate(['/'])
          },
          error: (error : HttpErrorResponse) => {
            this.errorMessage = error.error.detail;
          }
        })
    } else {
      this.form.markAllAsTouched();
    }
  }
}
