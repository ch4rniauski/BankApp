import {Component, inject} from '@angular/core';
import {Sidebar} from '../../common-ui/sidebar/sidebar';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import {CreditCardService} from '../../data/services/credit-card.service';
import {HttpErrorResponse} from '@angular/common/http';
import {Router} from '@angular/router';

@Component({
  selector: 'app-add-credit-card-page',
  imports: [
    Sidebar,
    ReactiveFormsModule
  ],
  templateUrl: './add-credit-card-page.html',
  styleUrl: './add-credit-card-page.scss',
})
export class AddCreditCardPage {
  private creditCardService = inject(CreditCardService)
  private fb = inject(FormBuilder)
  private router = inject(Router)

  form = this.fb.nonNullable.group({
    cardType: this.fb.nonNullable.control('Visa', [Validators.required])
  })

  onsubmit() {
    if (this.form.valid){
      this.creditCardService.createCreditCard(this.form.controls['cardType'].value)
        .subscribe({
          next: _ => {
            this.router.navigate(['/'])
          },
          error: (error: HttpErrorResponse) => {
            console.error(error)
          }
        })
    } else {
      this.form.markAsTouched();
    }
  }
}
